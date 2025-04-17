using System;
namespace SmartJobPreppAPI.DTOs
{
	public class JobDescriptionResponseDTO
	{
        public int JobDescriptionId { get; set; }

        public string Title { get; set; }

        public string DescriptionText { get; set; }

        public List<QuestionDTO> Questions { get; set; }
    }
}

