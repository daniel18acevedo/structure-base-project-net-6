namespace SecurityDomain;
public class RolePermission
{
    public int Id { get; set; }
    public int RolId { get; set; }
    public Role Role { get; set; }
    public int PermissionId { get; set; }
    public Permission Permission { get; set; }
}