using System.ComponentModel.DataAnnotations;

namespace MigracijaNaBazu.Models
{
    public class Newsletter
    {
        [Key]
        public int NewsletterId { get; set; }

        public string Naslov { get; set; }
        public string Sadrzaj { get; set; }
        public DateTime DatumSlanja { get; set; }

        public string Tip { get; set; }
        public string Status { get; set; }

        public int OperaterId { get; set; }
        public Operater Operater { get; set; }

        public List<RegistrovaniKorisnik> Primaoci { get; set; } = new List<RegistrovaniKorisnik>();

        public Newsletter() { }

        public void PretplatiSe(string email)
        {
        }

        public void PosaljiNovosti()
        {
        }
    }
}