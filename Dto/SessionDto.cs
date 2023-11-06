using engine.Models;

namespace engine.Dto;

public class SessionDto
{
    public int ModeId { get; set; }
    public string Student { get; set; }
    public int SubjectId { get; set; }
    public DateTime SessionDate { get; set; }
    public int SessionStartTime { get; set; }
    public int SessionLengthInMinutes { get; set; }

    public override string ToString()
    {
        return $"[{ModeId} {Student} {SubjectId} => {SessionDate}:{SessionStartTime} {SessionLengthInMinutes}]";
    }
}