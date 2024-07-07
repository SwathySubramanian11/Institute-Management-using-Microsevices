namespace InstituteMvcApp.Models
{
    public class Course
    {
        public string CourseCode { get; set; } = null!;

        public string CourseTitle { get; set; } = null!;

        public int? Duration { get; set; }

        public decimal? CourseFee { get; set; }
    }
}
