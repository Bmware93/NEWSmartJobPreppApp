using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartJobPreppAPI.DTOs;
using SmartJobPreppAPI.Entities;

namespace SmartJobPreppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewController : ControllerBase
    {
        private readonly JobDbContext _context;
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;

        public InterviewController(JobDbContext context, IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _config = config;
            _httpClientFactory = httpClientFactory;
        }

        // GET: api/Interview
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InterviewAnswer>>> GetInterviewAnswers()
        {
          if (_context.InterviewAnswers == null)
          {
              return NotFound();
          }
            return await _context.InterviewAnswers.ToListAsync();
        }

        // GET: api/Interview/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InterviewAnswer>> GetInterviewAnswer(int id)
        {
          if (_context.InterviewAnswers == null)
          {
              return NotFound();
          }
            var interviewAnswer = await _context.InterviewAnswers.FindAsync(id);

            if (interviewAnswer == null)
            {
                return NotFound();
            }

            return interviewAnswer;
        }

        [HttpGet("job/{jobId}/answers")]
        public async Task<ActionResult<IEnumerable<AnswerFeedbackDTO>>> GetAnswersForJob(int jobId)
        {
            var answers = await _context.InterviewAnswers
                .Include(a => a.Question)
                .Where(a => a.Question.JobDescriptionId == jobId)
                .Select(a => new AnswerFeedbackDTO
                {
                    QuestionText = a.Question.QuestionText,
                    Answer = a.Answer,
                    Feedback = a.Feedback,
                    SubmittedAt = a.CreatedAt

                }).ToListAsync();

            return Ok(answers);
        }


        // PUT: api/Interview/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInterviewAnswer(int id, InterviewAnswer interviewAnswer)
        {
            if (id != interviewAnswer.Id)
            {
                return BadRequest();
            }

            _context.Entry(interviewAnswer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterviewAnswerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Interview
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("feedback")]
        public async Task<ActionResult<string>> GetAnswerFeedback([FromBody] InterviewAnswerRequestDTO dto)
        {
            var question = await _context.Questions.FindAsync(dto.QuestionId);

            if(question == null)
            {
                return ("Question not found");
            }

            var feedback = await GenerateFeedbackFromOpenAI(question.QuestionText, dto.Answer);

            var record = new InterviewAnswer
            {   
                QuestionId = question.Id,
                Answer = dto.Answer,
                Feedback = feedback
            };

            _context.InterviewAnswers.Add(record);
            await _context.SaveChangesAsync();

            return Ok(new { feedback });
        }

        // DELETE: api/Interview/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInterviewAnswer(int id)
        {
            if (_context.InterviewAnswers == null)
            {
                return NotFound();
            }
            var interviewAnswer = await _context.InterviewAnswers.FindAsync(id);
            if (interviewAnswer == null)
            {
                return NotFound();
            }

            _context.InterviewAnswers.Remove(interviewAnswer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InterviewAnswerExists(int id)
        {
            return (_context.InterviewAnswers?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async Task<string> GenerateFeedbackFromOpenAI(string question, string answer)
        {
            var client = _httpClientFactory.CreateClient();
            var apiKey = _config["OpenAI:ApiKey"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var body = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "system", content = "You are a technical interview coach. Provide kind but constructive feedback on coding interview answers." },

                    new { role = "user", content = $"Question: {question}\nAnswer: {answer}\n\nHow did I do?" }
                }
            };

            var json = JsonSerializer.Serialize(body);
            var response = await client.PostAsync("https://api.openai.com/v1/chat/completions",
                new StringContent(json, Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(responseString);
            return doc.RootElement
                      .GetProperty("choices")[0]
                      .GetProperty("message")
                      .GetProperty("content")
                      .GetString();
        }

    }
}
