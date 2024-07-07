using System;
using System.Collections.Generic;

namespace CourseLibrary.Models;

public partial class Course
{
    public string CourseCode { get; set; } = null!;

    public string CourseTitle { get; set; } = null!;

    public int? Duration { get; set; }

    public decimal? CourseFee { get; set; }
}
