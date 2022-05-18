using DataAccessInterface;
using SecurityDomain;

namespace SecurityLogic;
public class SecurityService
{
    private readonly IRepository<SecurityUser> _securityUserRepository;
    public SecurityService(IUnitOfWork unitOfWork)
    {
        this._securityUserRepository = unitOfWork.GetRepository<SecurityUser>();
    }

    public async Task<bool> CheckPermissionsAsync(int userId, IEnumerable<string> permissions)
    {
        permissions = permissions.Select(permission => permission.ToLower());

        var userHasPermissions = await this._securityUserRepository.ExistAsync(
            securityUser => securityUser.Id == userId && 
            securityUser.Role.Permissions.Select(rolePermission => rolePermission.Permission.Name.ToLower()).Any(userPermissions => permissions.Contains(userPermissions)));
        
        return userHasPermissions;
    }

    public async Task<bool> CheckRoleAsync(int userId, IEnumerable<string> roles)
    {
        roles = roles.Select(role => role.ToLower());

        var userHasRole = await this._securityUserRepository.ExistAsync(securityUser => securityUser.Id == userId && roles.Contains(securityUser.Role.Name.ToLower()));

        return userHasRole;
    }
}
