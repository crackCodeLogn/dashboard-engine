namespace engine.Models;

public class LibraryData
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