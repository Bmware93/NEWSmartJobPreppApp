using System;
using System.Collections.Generic;

namespace SmartJobPreppAPI.Entities;

public partial class Question
{
    public int Id { get; set; }

    public string? QuestionText { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public int? JobDescriptionId { get; set; }

    public virtual JobDescription? JobDescription { get; set; }
}
