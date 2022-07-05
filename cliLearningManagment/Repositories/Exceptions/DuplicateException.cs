namespace cliLearningManagment.Repositories.Exceptions;

public class DuplicateException : BaseDataException
{
    public DuplicateException(string repositoryName, string fieldName) :
        base($"duplicate {fieldName} on {repositoryName}")
    {
    }
}