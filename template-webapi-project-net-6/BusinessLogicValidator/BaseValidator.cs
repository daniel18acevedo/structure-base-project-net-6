using BusinessLogicValidatorInterface;
using FluentValidation;
using FluentValidation.Results;

namespace BusinessLogicValidator;
//https://docs.fluentvalidation.net/en/latest/
public class BaseValidator<TEntity> : AbstractValidator<TEntity>, IBusinessValidator<TEntity> where TEntity : class
{
    public async Task CreationValidationAsync(TEntity entity)
    {
        await this.Validate(entity);

        await this.IncludeValidation(entity);

        await this.BusinessValidation(entity);
    }

    private async Task Validate(TEntity entity)
    {
        base.EnsureInstanceNotNull(entity);

        var result = await base.ValidateAsync(entity);
        var errorFormatted = FormatErrors(result.Errors);

        if (!result.IsValid) throw new ArgumentException(errorFormatted);
    }

    private string FormatErrors(IEnumerable<ValidationFailure> errors)
    {
        var errorFormatted = "";

        foreach (var error in errors)
        {
            if (string.IsNullOrEmpty(errorFormatted))
            {
                errorFormatted = error.ErrorMessage;
            }
            else
            {
                errorFormatted = $"{errorFormatted} \n {error.ErrorMessage}";
            }
        }

        return errorFormatted;
    }

    protected virtual Task BusinessValidation(TEntity entity) { return Task.CompletedTask; }

    protected virtual Task IncludeValidation(TEntity entity) { return Task.CompletedTask; }

    public void ValidateIdentifier(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Id can't be less or equal to 0");
        }

        this.BusinesIdentifierValidation(id);
    }

    protected virtual void BusinesIdentifierValidation(int id) { }
}