namespace BusinessLogicValidatorInterface;
public interface IBusinessValidator<TEntity> where TEntity : class
{
    Task CreationValidationAsync(TEntity entity);
    void ValidateIdentifier(int id);
}
