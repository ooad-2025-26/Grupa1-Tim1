namespace MigracijaNaBazu.Models
{
    public class Admin : Korisnik
    {
        public int AdminId { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public Admin() { }

        public void ObrisiKorisnika()
        {
        }
    }
}