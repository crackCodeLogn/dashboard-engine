using engine.Models;

namespace engine.Data;

public interface ISubjectRepository
{
    Task<IEnumerable<Subject>> GetAllSubjectsAsync();
}