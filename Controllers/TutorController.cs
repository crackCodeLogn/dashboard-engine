using AutoMapper;
using engine.Data;
using engine.Dto;
using engine.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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
        return Ok(200);
    }
}
