using System.Text.Json;
using AutoMapper;
using engine.Data;
using engine.Dto;
using engine.Models;
using Microsoft.AspNetCore.Mvc;

namespace engine.Controllers;

[ApiController]
[Route("[controller]")]
public class TutorController : ControllerBase
{
    private readonly ILogger<TutorController> _logger;
    private ISubjectRepository SubjectRepository { get; }
    private IModeRepository ModeRepository { get; }
    private IMapper Mapper { get; }

    public TutorController(ILogger<TutorController> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        Mapper = mapper;
        SubjectRepository = unitOfWork.SubjectRepository;
        ModeRepository = unitOfWork.ModeRepository;
        _logger = logger;
    }

    [HttpGet("subjects")]
    public async Task<IActionResult> GetAllSubjects()
    {
        var list = await SubjectRepository.GetAllSubjectsAsync();
        _logger.LogInformation("list is -> {}", list);
        var dtos = Mapper.Map<IEnumerable<SubjectDto>>(list);
        _logger.LogInformation("dto list is -> {}", dtos);
        return Ok(dtos);
    }


    [HttpGet("modes")]
    public async Task<IActionResult> GetAllSessionModes()
    {
        var list = await ModeRepository.GetAllModesAsync();
        _logger.LogInformation("list is -> {}", list);
        var dtos = Mapper.Map<IEnumerable<ModeDto>>(list);
        _logger.LogInformation("dto list is -> {}", dtos);
        return Ok(dtos);
    }

    [HttpPost("session")]
    public async Task<IActionResult> PostSessionOnCalendarAsync([FromBody] SessionDto sessionDto)
    {
        _logger.LogInformation("Received session data from UI => {}", sessionDto);
        var session = new Session
        {
            Mode = (await ModeRepository.GetAllModesAsync()).First(p => p.Id == sessionDto.ModeId),
            Student = sessionDto.Student,
            Subject = (await SubjectRepository.GetAllSubjectsAsync()).First(p => p.Id == sessionDto.SubjectId),
            SessionDate = sessionDto.SessionDate,
            SessionStartTime = sessionDto.SessionStartTime,
            SessionLengthInMinutes = sessionDto.SessionLengthInMinutes
        };
        _logger.LogInformation("Converted session data => {}", session);

        string jsonData = JsonSerializer.Serialize(session);
        using (HttpClient client = new HttpClient())
        {
            StringContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("http://localhost:8089/cal/session", content);
            _logger.LogInformation("{}", response.StatusCode);
        }

        return Ok(200);
    }

    [HttpGet("sessionData")]
    public async Task<IActionResult> GetLatestSessionDataOfStudent([FromQuery] string student)
    {
        _logger.LogInformation("Received request to get latest session data of {}", student);

        var filePath = "";
        var fileName = "";
        var fileData = "";
        string baseFolder = "/home/v2/theTempest";
        if (Directory.Exists(baseFolder))
        {
            var list = Directory.GetFiles(baseFolder)
                                .Where(file => Path.GetFileName(file).Contains(student))
                                .Select(file => new FileInfo(file))
                                .OrderByDescending(file => file.LastWriteTime)
                                .Select(file => file.FullName)
                                .ToList();

            if (list.Count != 0) filePath = list.First();
        }
        _logger.LogInformation("Responding with information from file at => {}", filePath);
        if (filePath.Length > 0)
        {
            fileName = filePath.Substring(filePath.LastIndexOf('/') + 1);
            fileData = System.IO.File.ReadAllText(filePath);
        }

        return Ok(new { FileName = fileName, FileData = fileData });
    }

    [HttpPost("expiryData")]
    public async Task<IActionResult> PostExpiryDataOnCalendarAsync([FromBody] ExpiryDataDto expiryDataDto)
    {
        _logger.LogInformation("Received expiry data from UI => {}", expiryDataDto);
        var expiryData = new ExpiryData
        {
            Date = expiryDataDto.Date,
            Data = expiryDataDto.Data
        };
        _logger.LogInformation("Converted expiry data => {}", expiryData);

        string jsonData = JsonSerializer.Serialize(expiryData);
        using (HttpClient client = new())
        {
            StringContent content = new(jsonData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("http://localhost:8089/cal/expiry", content);
            _logger.LogInformation("{}", response.StatusCode);
        }

        return Ok(200);
    }

    [HttpPost("sessionData")]
    public async Task<IActionResult> CaptureSessionDataForDiskWrite([FromBody] SessionDataDto sessionDataDto)
    {
        _logger.LogInformation("Received session-data from UI => {}", sessionDataDto);
        var sessionData = new SessionData
        {
            Mode = (await ModeRepository.GetAllModesAsync()).First(p => p.Id == sessionDataDto.ModeId),
            Student = sessionDataDto.Student,
            Subject = (await SubjectRepository.GetAllSubjectsAsync()).First(p => p.Id == sessionDataDto.SubjectId),
            SessionDate = sessionDataDto.SessionDate,
            Data = sessionDataDto.Data,
        };
        _logger.LogInformation("Converted session-data => {}", sessionData);

        var fileTitle = GetFileTitle(sessionData);
        _logger.LogInformation("Generated file title => {}", fileTitle);

        string filePath = Path.Combine("/home/v2/theTempest", fileTitle);
        await System.IO.File.WriteAllTextAsync(filePath, sessionData.Data, System.Text.Encoding.UTF8);

        return Ok(200);
    }

    private static String GetFileTitle(SessionData sessionData)
    {
        return string.Join(".",
            sessionData.Mode.SessionMode.Contains("Varsity") ? "session" : "session-direct",
            sessionData.Student,
            sessionData.Subject.SessionSubject,
            sessionData.SessionDate.Date.ToString("yyyyMMdd"),
            "txt"
        );
    }
}
