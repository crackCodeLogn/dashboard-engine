using engine.Models;
using Microsoft.EntityFrameworkCore;

namespace engine.Data.Repo;

public class SubjectRepository : ISubjectRepository
{
    public DataContext DataContext { get; }

    public SubjectRepository(DataContext dataContext)
    {
        DataContext = dataContext;
    }

    public async Task<IEnumerable<Subject>> GetAllSubjectsAsync()
    {
        return await DataContext.Subjects
        .Include(p => p.Mode)
        .ToListAsync();
    }
}