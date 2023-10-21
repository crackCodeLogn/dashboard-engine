using engine.Data.Repo;

namespace engine.Data;

public class UnitOfWork : IUnitOfWork
{

    public DataContext DataContext { get; }

    public UnitOfWork(DataContext dataContext)
    {
        DataContext = dataContext;
    }

    IModeRepository IUnitOfWork.ModeRepository => new ModeRepository(DataContext);

    ISubjectRepository IUnitOfWork.SubjectRepository => new SubjectRepository(DataContext);
}