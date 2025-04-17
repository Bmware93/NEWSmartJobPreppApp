using System;
namespace SmartJobPreppAPI.Entities
{
	public class InterviewAnswer
	{
		public int Id { get; set; }

		public int QuestionId { get; set; }

		public Question Question { get; set; }

		public string Answer { get; set; }

		public string Feedback { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}

