using System.ComponentModel.DataAnnotations;

namespace MigracijaNaBazu.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        public string NazivEventa { get; set; }
        public string OpisEventa { get; set; }
        public VrstaEventa VrstaEventa { get; set; }

        public DateTime VrijemePocetka { get; set; }
        public DateTime VrijemeZavrsetka { get; set; }

        public string SlikaUrl { get; set; }
        public string StranicaUrl { get; set; }

        public int KoordinateId { get; set; }
        public Koordinate Koordinate { get; set; }

        public Event() { }

        public bool ProvjeriDostupnost()
        {
            return true;
        }

        public string GetDetaljneInformacije()
        {
            return NazivEventa + " - " + OpisEventa;
        }

        public double ProsjecnaOcjena()
        {
            return 0;
        }
    }
}