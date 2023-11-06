namespace engine.Dto;

public class ModeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }

    public override string ToString()
    {
        return $"[{Name}, {Color}]";
    }
}