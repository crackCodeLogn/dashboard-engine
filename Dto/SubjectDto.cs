using engine.Models;

namespace engine.Dto;

public class SubjectDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ModeId { get; set; }

    public override string ToString()
    {
        return $"[{Name}, {ModeId}]";
    }
}