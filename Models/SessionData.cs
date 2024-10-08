namespace engine.Models;

public class SessionData
{
    public int Id { get; set; }
    public Mode Mode { get; set; }
    public string Student { get; set; }
    public Subject Subject { get; set; }
    public DateTime SessionDate { get; set; }
    public string Data { get; set; }

    public override string ToString()
    {
        return $"[{Mode.SessionMode} {Student} {Subject.SessionSubject} => {SessionDate}:{Data.Trim()}]";
    }
}