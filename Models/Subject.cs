namespace engine.Models;

public class Subject
{
    public int Id { get; set; }
    public string SessionSubject { get; set; }
    public Mode Mode { get; set; }

    public override string ToString()
    {
        return $"[{SessionSubject}, {Mode.SessionMode}]";
    }
}