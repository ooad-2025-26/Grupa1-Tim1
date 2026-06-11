namespace gosarajevovol3.ViewModels;

public class ReviewFormViewModel
{
    public int? ReviewId { get; set; }
    public int HospitalityId { get; set; }
    public string HospitalityName { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
}