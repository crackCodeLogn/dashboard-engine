using engine.Models;
using Microsoft.EntityFrameworkCore;

namespace engine.Data.Repo;

public class ModeRepository : IModeRepository
{
    public DataContext DataContext { get; }

    public ModeRepository(DataContext dataContext)
    {
        DataContext = dataContext;
    }

    public async Task<IEnumerable<Mode>> GetAllModesAsync()
    {
        return await DataContext.Modes.ToListAsync();
    }
}