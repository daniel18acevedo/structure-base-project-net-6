using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Factory;
public class BaseFactory
{
    private readonly IServiceCollection _services;
    private readonly IConfiguration _configuration;

    public BaseFactory(IServiceCollection services, IConfiguration configuration)
    {
        this._services = services;
        this._configuration = configuration;
    }

    public void InjectDependencies()
    {
        this._services.InjectBusinessLogicsAdapter();
        this._services.InjectBusinessLogics();
        this._services.InjectDataAccess(this._configuration.GetConnectionString("MyDataBaseConnectionString"));
        this._services.InjectSession();
        this._services.InjectValidators();
    }
}