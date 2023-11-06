namespace engine.Models;

public class Mode
{
    public int Id { get; set; }
    public string SessionMode { get; set; }
    public string Color { get; set; }

    public override string ToString()
    {
        return $"[{SessionMode}, {Color}]";
    }
}