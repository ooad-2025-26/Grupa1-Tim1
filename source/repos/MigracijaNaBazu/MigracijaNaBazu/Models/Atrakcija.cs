using System.ComponentModel.DataAnnotations;

namespace MigracijaNaBazu.Models
{
    public class Atrakcija
    {
        [Key]
        public int AtrakcijaId { get; set; }

        public string NazivAtrakcije { get; set; }

        public int KoordinateId { get; set; }
        public Koordinate Koordinate { get; set; }

        public string SlikaUrl { get; set; }
        public string OpisAtrakcije { get; set; }
        public VrstaAtrakcije VrstaAtrakcije { get; set; }

        public List<Recenzija> Recenzije { get; set; } = new List<Recenzija>();

        public Atrakcija() { }

        public string GetDetaljneInformacije()
        {
            return NazivAtrakcije + " - " + OpisAtrakcije;
        }

        public double ProsjecnaOcjena()
        {
            if (Recenzije == null || Recenzije.Count == 0)
                return 0;

            return Recenzije.Average(r => r.Ocjena);
        }
    }
}