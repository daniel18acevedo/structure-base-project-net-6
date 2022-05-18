namespace SecurityDomain;

public class Permission
{
    public int Id { get; set; }
    public string Name { get; set; }

    public IEnumerable<RolePermission> Roles{get;set;}
}