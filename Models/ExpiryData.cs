namespace engine.Models;

public class ExpiryData
{
    public DateTime Date { get; set; }
    public string Data { get; set; }

    public override string ToString()
    {
        return $"[{Date}:{Data.Trim()}]";
    }
}