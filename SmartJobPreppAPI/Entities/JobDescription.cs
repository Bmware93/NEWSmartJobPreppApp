﻿using System;
using System.Collections.Generic;

namespace SmartJobPreppAPI.Entities;

public partial class JobDescription
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Company { get; set; }

    public string? DescriptionText { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<Question> Questions { get; } = new List<Question>();
}
