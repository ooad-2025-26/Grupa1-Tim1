using System.ComponentModel.DataAnnotations;

namespace MigracijaNaBazu.Models
{
    public class UgostiteljskiObjekat
    {
        [Key]
        public int ObjekatId { get; set; }

        public string NazivObjekta { get; set; }

        public int KoordinateId { get; set; }
        public Koordinate Koordinate { get; set; }

        public string SlikaUrl { get; set; }
        public string OpisObjekta { get; set; }
        public VrstaObjekta VrstaObjekta { get; set; }

        public List<Recenzija> Recenzije { get; set; } = new List<Recenzija>();

        public UgostiteljskiObjekat() { }

        public string GetDetaljneInformacije()
        {
            return NazivObjekta + " - " + OpisObjekta;
        }

        public double ProsjecnaOcjena()
        {
            if (Recenzije == null || Recenzije.Count == 0)
                return 0;

            return Recenzije.Average(r => r.Ocjena);
        }
    }
}