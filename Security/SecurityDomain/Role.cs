namespace SecurityDomain;
public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }

    public IEnumerable<RolePermission> Permissions { get; set; }
}
