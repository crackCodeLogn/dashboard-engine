using AutoMapper;
using engine.Data;
using engine.Dto;
using Microsoft.AspNetCore.Mvc;

namespace engine.Controllers;

[ApiController]
[Route("tutor/[controller]")]
public class SubjectController : ControllerBase
{
    private readonly ILogger<SubjectController> _logger;
    private ISubjectRepository SubjectRepository { get; }
    private IMapper Mapper { get; }

    public SubjectController(ILogger<SubjectController> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        Mapper = mapper;
        SubjectRepository = unitOfWork.SubjectRepository;
        _logger = logger;
    }

    [HttpGet("tutor/modes")]
    public async Task<IActionResult> GetAllSessionModes()
    {
        var list = await SubjectRepository.GetAllSubjectsAsync();
        Console.WriteLine(list);
        var dtos = Mapper.Map<IEnumerable<SubjectDto>>(list);
        return Ok(list);
    }
}
