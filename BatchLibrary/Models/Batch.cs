using System;
using System.Collections.Generic;

namespace BatchLibrary.Models;

public partial class Batch
{
    public string BatchCode { get; set; } = null!;

    public string? CourseCode { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual Course? CourseCodeNavigation { get; set; }
}
