using System;
using System.Collections.Generic;

namespace BatchLibrary.Models;

public partial class Course
{
    public string CourseCode { get; set; } = null!;

    public virtual ICollection<Batch> Batches { get; set; } = new List<Batch>();
}
