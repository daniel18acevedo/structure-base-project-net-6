namespace SessionDomain;

public class SessionDomain
{
    public int Id { get; set; }
    public string Token { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public DateTime LastLoggedIn { get; set; }
}