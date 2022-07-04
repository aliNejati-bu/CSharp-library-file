namespace cliLearningManagment.Repositories.Exceptions;

public class DuplicateException : Exception
{
    public DuplicateException(string repositoryName, string fieldName) :
        base($"duplicate {fieldName} on {repositoryName}")
    {
    }
}