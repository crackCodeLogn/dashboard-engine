using engine.Models;

namespace engine.Dto;

public class LibraryDataDto
{
    required public string BookName { get; set; }
    public DateTime? BorrowDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public DateTime? ReturnedDate { get; set; }

    public override string ToString()
    {
        return $"[{BookName.Trim()}:{BorrowDate},{ReturnDate},{ReturnedDate}]";
    }
}