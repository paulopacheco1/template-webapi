namespace PetroTemplate.Domain.Exceptions;

public class RequestValidationException : AppException
{
    public IEnumerable<FieldValidationErrors> Fields { get; }

    public RequestValidationException(IEnumerable<FieldValidationErrors> fields) : base("Erro na validação dos dados.")
    {
        Fields = fields;
    }
}

public class FieldValidationErrors
{
    public string Field { get; }
    public IEnumerable<string> Errors { get; }

    public FieldValidationErrors(string field, IEnumerable<string> errors)
    {
        Field = field;
        Errors = errors;
    }
}