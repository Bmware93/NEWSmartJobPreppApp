using System;
namespace SmartJobPreppAPI.DTOs
{
	public class AnswerFeedbackDTO
	{
		public string QuestionText { get; set; }

		public string Answer { get; set; }

		public string Feedback { get; set; }

		public DateTime SubmittedAt { get; set; }
	}
}

