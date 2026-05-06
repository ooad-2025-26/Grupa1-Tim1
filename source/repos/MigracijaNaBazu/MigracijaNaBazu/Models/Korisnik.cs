using System.ComponentModel.DataAnnotations;

namespace MigracijaNaBazu.Models
{
    public class Korisnik
    {
        [Key]
        public int SessionId { get; set; }

        public Korisnik() { }

        public bool Prijava(string username, string password)
        {
            return true;
        }

        public void Odjava()
        {
        }

        protected bool ValidirajPodatke()
        {
            return true;
        }
    }
}