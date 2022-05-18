namespace SecurityDomain;
public class SecurityUser
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }
}