using engine.Models;

namespace engine.Data;

public interface IModeRepository
{
    Task<IEnumerable<Mode>> GetAllModesAsync();
}