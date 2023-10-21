namespace engine.Data;

public interface IUnitOfWork
{
    IModeRepository ModeRepository { get; }
    ISubjectRepository SubjectRepository { get; }
}