namespace gosarajevovol3.Models;

public class Operator: User
{
    public List<Newsletter> Newsletters { get; set; } = new List<Newsletter>();
}