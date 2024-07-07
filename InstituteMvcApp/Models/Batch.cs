namespace InstituteMvcApp.Models
{
    public class Batch
    {
        public string BatchCode { get; set; } = null!;

        public string? CourseCode { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
