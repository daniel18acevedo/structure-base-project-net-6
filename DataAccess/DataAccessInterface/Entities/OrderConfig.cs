namespace DataAccessInterface.Entities;
public class OrderConfig
{
    public ORDER OrderBy { get; set; } = ORDER.ASC;
    public string[] Properties { get; set; } = new string[0];
}