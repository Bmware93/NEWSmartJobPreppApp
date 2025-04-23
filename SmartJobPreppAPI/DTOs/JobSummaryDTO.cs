using System;
namespace SmartJobPreppAPI.DTOs
{
	public class JobSummaryDTO
	{
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Company { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}

