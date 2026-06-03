using System.ComponentModel.DataAnnotations;

namespace gosarajevovol3.ViewModels
{
    public class SmartPlannerInputViewModel
    {
 
        [Required(ErrorMessage = "Datum dolaska je obavezan.")]
        [DataType(DataType.Date)]
        public DateTime ArrivalDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Datum odlaska je obavezan.")]
        [DataType(DataType.Date)]
        public DateTime DepartureDate { get; set; } = DateTime.Today.AddDays(1);

        [Range(1, 5)] public int HistoryQ1 { get; set; } = 3;
        [Range(1, 5)] public int HistoryQ2 { get; set; } = 3;

        [Range(1, 5)] public int NatureQ1 { get; set; } = 3;
        [Range(1, 5)] public int NatureQ2 { get; set; } = 3;

        [Range(1, 5)] public int CultureQ1 { get; set; } = 3;
        [Range(1, 5)] public int CultureQ2 { get; set; } = 3;

        [Range(1, 5)] public int FoodQ1 { get; set; } = 3;
        [Range(1, 5)] public int FoodQ2 { get; set; } = 3;

        [Range(1, 5)] public int NightlifeQ1 { get; set; } = 3;
        [Range(1, 5)] public int NightlifeQ2 { get; set; } = 3;

        public int HistoryScore => (int)Math.Round((HistoryQ1 + HistoryQ2) / 2.0);
        public int NatureScore => (int)Math.Round((NatureQ1 + NatureQ2) / 2.0);
        public int CultureScore => (int)Math.Round((CultureQ1 + CultureQ2) / 2.0);
        public int FoodScore => (int)Math.Round((FoodQ1 + FoodQ2) / 2.0);
        public int NightlifeScore => (int)Math.Round((NightlifeQ1 + NightlifeQ2) / 2.0);

        public bool DatesAreValid => DepartureDate > ArrivalDate;
        public int NumberOfDays => (DepartureDate - ArrivalDate).Days +1;
}
}
