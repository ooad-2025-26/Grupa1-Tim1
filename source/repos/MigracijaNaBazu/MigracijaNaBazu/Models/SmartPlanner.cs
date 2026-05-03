using System.ComponentModel.DataAnnotations;

namespace MigracijaNaBazu.Models
{
    public class SmartPlanner
    {
        [Key]
        public int PlanId { get; set; }

        public int RegistrovaniKorisnikId { get; set; }
        public RegistrovaniKorisnik RegistrovaniKorisnik { get; set; }

        public int PreferencijaId { get; set; }
        public Preferencija Preferencija { get; set; }

        public List<Atrakcija> ListaAtrakcija { get; set; } = new List<Atrakcija>();
        public List<UgostiteljskiObjekat> ListaObjekata { get; set; } = new List<UgostiteljskiObjekat>();
        public List<Event> ListaEvenata { get; set; } = new List<Event>();

        public DateTime DatumDolaska { get; set; }
        public DateTime DatumOdlaska { get; set; }

        public SmartPlanner() { }

        public List<string> GenerisiPlan()
        {
            return new List<string>();
        }

        public void SacuvajPlan()
        {
        }
    }
}