using System.ComponentModel.DataAnnotations;

namespace MigracijaNaBazu.Models
{
    public class Preferencija
    {
        [Key]
        public int PreferencijaId { get; set; }

        public int Historija { get; set; }
        public int TradicionalnaHrana { get; set; }
        public int Priroda { get; set; }
        public int Kultura { get; set; }
        public int NocniZivot { get; set; }

        public Preferencija() { }

        public void PostaviNivoInteresa(string tip, int nivo)
        {
            if (tip == "historija")
                Historija = nivo;
            else if (tip == "tradicionalnaHrana")
                TradicionalnaHrana = nivo;
            else if (tip == "priroda")
                Priroda = nivo;
            else if (tip == "kultura")
                Kultura = nivo;
            else if (tip == "nocniZivot")
                NocniZivot = nivo;
        }
    }
}