using System;
using System.Collections.Generic;

namespace StudentLibrary.Models;

public partial class Student
{
    public string RollNo { get; set; } = null!;

    public string? BatchCode { get; set; }

    public string StudentName { get; set; } = null!;

    public string? StudentAddress { get; set; }

    public virtual Batch? BatchCodeNavigation { get; set; }
}
