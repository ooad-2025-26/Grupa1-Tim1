using System.ComponentModel.DataAnnotations;

namespace MigracijaNaBazu.Models
{
    public class RegistrovaniKorisnik : Korisnik
    {
        public int KorisnikId { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public int? KoordinateId { get; set; }
        public Koordinate Koordinate { get; set; }

        public List<SmartPlanner> Planovi { get; set; } = new List<SmartPlanner>();
        public List<Recenzija> ListaRecenzija { get; set; } = new List<Recenzija>();

        public RegistrovaniKorisnik() { }

        public void UrediProfil(string noviPodaci)
        {
        }

        public void DodajRecenziju(Recenzija recenzija)
        {
            ListaRecenzija.Add(recenzija);
        }
    }
}