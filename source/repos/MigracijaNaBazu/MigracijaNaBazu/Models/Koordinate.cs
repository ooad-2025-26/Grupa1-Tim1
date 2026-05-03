using System.ComponentModel.DataAnnotations;

namespace MigracijaNaBazu.Models
{
    public class Koordinate
    {
        [Key]
        public int KoordinateId { get; set; }

        public double Latituda { get; set; }
        public double Longituda { get; set; }

        public Koordinate() { }
    }
}