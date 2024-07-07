namespace InstituteMvcApp.Models
{
    public class Student
    {
        public string RollNo { get; set; } = null!;

        public string? BatchCode { get; set; }

        public string StudentName { get; set; } = null!;

        public string? StudentAddress { get; set; }
    }
}
