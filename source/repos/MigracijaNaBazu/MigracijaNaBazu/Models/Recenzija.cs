using System.ComponentModel.DataAnnotations;

namespace MigracijaNaBazu.Models
{
    public class Recenzija
    {
        [Key]
        public int RecenzijaId { get; set; }

        public string Tekst { get; set; }
        public double Ocjena { get; set; }
        public DateTime Datum { get; set; }

        public int RegistrovaniKorisnikId { get; set; }
        public RegistrovaniKorisnik RegistrovaniKorisnik { get; set; }

        public int? AtrakcijaId { get; set; }
        public Atrakcija Atrakcija { get; set; }

        public int? UgostiteljskiObjekatId { get; set; }
        public UgostiteljskiObjekat UgostiteljskiObjekat { get; set; }

        public Recenzija() { }
    }
}
