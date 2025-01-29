using fleetsystem.dto;

public class ValidationFailureException : Exception
{
    public ValidationFailure[] Failures {get;}

    public ValidationFailureException(ValidationFailure[] failures) 
    : base("Validation failed")
    {
        Failures = failures;
    }
}
