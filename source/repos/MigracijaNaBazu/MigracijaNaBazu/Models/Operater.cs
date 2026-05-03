namespace MigracijaNaBazu.Models
{
    public class Operater : Korisnik
    {
        public int OperaterId { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public List<Newsletter> Newsletteri { get; set; } = new List<Newsletter>();

        public Operater() { }
    }
}