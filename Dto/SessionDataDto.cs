using engine.Models;

namespace engine.Dto;

public class SessionDataDto
{
    public int ModeId { get; set; }
    public string Student { get; set; }
    public int SubjectId { get; set; }
    public DateTime SessionDate { get; set; }
    public string Data { get; set; }

    public override string ToString()
    {
        return $"[{ModeId} {Student} {SubjectId} => {SessionDate}:{Data.Trim()}]";
    }
}