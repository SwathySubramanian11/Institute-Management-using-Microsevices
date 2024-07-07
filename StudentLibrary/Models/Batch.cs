using System;
using System.Collections.Generic;

namespace StudentLibrary.Models;

public partial class Batch
{
    public string BatchCode { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
