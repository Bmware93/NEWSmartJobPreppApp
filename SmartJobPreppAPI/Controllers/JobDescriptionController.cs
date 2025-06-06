using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartJobPreppAPI.Entities;
using SmartJobPreppAPI.DTOs;

namespace SmartJobPreppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobDescriptionController : ControllerBase
    {
        private readonly JobDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public JobDescriptionController(JobDbContext context, IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        // GET: api/JobDescription
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobDescription>>> GetJobDescriptions()
        {
          if (_context.JobDescriptions == null)
          {
              return NotFound();
          }
            return await _context.JobDescriptions.ToListAsync();
        }
        //GET: List all jobs
        [HttpGet("all-summaries")]
        public async Task<ActionResult<IEnumerable<JobSummaryDTO>>> GetAllJobs()
        {
            return await _context.JobDescriptions
                .OrderByDescending(j => j.CreatedAt)
                .Select(j => new JobSummaryDTO
                {
                    Id = j.Id,
                    Title = j.Title,
                    Company = j.Company,
                    CreatedAt = j.CreatedAt
                }).ToListAsync();
        }

        // GET: api/JobDescription/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobDescription>> GetJobDescription(int id)
        {
          if (_context.JobDescriptions == null)
          {
              return NotFound();
          }
            var jobDescription = await _context.JobDescriptions.FindAsync(id);

            if (jobDescription == null)
            {
                return NotFound();
            }

            return jobDescription;
        }

        [HttpGet("{id}/questions")]
        public async Task<ActionResult<IEnumerable<QuestionDTO>>> GetQuestionForJob(int id)
        {
            var job = await _context.JobDescriptions
                .Include(j => j.Questions)
                .FirstOrDefaultAsync(j => j.Id == id);

            if(job == null)
            {
                return NotFound();
            }

            return job.Questions
                .Select(q => new QuestionDTO
                {
                    Id = q.Id,
                    QuestionText = q.QuestionText
                }).ToList();
        }

        // PUT: api/JobDescription/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobDescription(int id, JobDescription jobDescription)
        {
            if (id != jobDescription.Id)
            {
                return BadRequest();
            }

            _context.Entry(jobDescription).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobDescriptionExists(id))
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

        // POST: api/JobDescription
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JobDescriptionResponseDTO>> PostJobDescription(JobDescription jobDescription)
        {
          if (_context.JobDescriptions == null)
          {
              return Problem("Entity set 'JobDbContext.JobDescriptions'  is null.");
          }
            _context.JobDescriptions.Add(jobDescription);
            await _context.SaveChangesAsync();

            var questions = await GenerateQuestionsFromOpenAI(jobDescription.Title, jobDescription.DescriptionText);

            // Saving the questions to DB
            foreach (var q in questions)
            {
                _context.Questions.Add(new Question
                {
                    JobDescriptionId = jobDescription.Id,
                    QuestionText = q
                });
            }

            await _context.SaveChangesAsync();

            var savedQuestions = await _context.Questions
                .Where(q => q.JobDescriptionId == jobDescription.Id)
                .Select(q => new QuestionDTO
                {
                    Id = q.Id,
                    QuestionText = q.QuestionText
                })
                .ToListAsync();

            
            var response = new JobDescriptionResponseDTO    
            {
                JobDescriptionId = jobDescription.Id,
                Title = jobDescription.Title,
                Company = jobDescription.Company,
                DescriptionText = jobDescription.DescriptionText,
                Questions = savedQuestions
            };

            return Ok(response);
        }

        // DELETE: api/JobDescription/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobDescription(int id)
        {
            if (_context.JobDescriptions == null)
            {
                return NotFound();
            }
            var jobDescription = await _context.JobDescriptions.FindAsync(id);
            if (jobDescription == null)
            {
                return NotFound();
            }

            _context.JobDescriptions.Remove(jobDescription);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobDescriptionExists(int id)
        {
            return (_context.JobDescriptions?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        //OpenAi caller method
        private async Task<List<string>> GenerateQuestionsFromOpenAI(string title, string description)
        {
            var client = _httpClientFactory.CreateClient();
            var apiKey = _config["OpenAI:ApiKey"];
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

            var body = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
            new { role = "system", content = "You are a technical interviewer." },
            new { role = "user", content = $"Generate 5 technical interview questions for a {title} job. Here is the description: {description}" }
        }
            };

            var json = JsonSerializer.Serialize(body);
            var response = await client.PostAsync("https://api.openai.com/v1/chat/completions",
                new StringContent(json, Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(responseString);
            var content = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return content.Split('\n')
                          .Where(line => !string.IsNullOrWhiteSpace(line))
                          .Select(line => line.Trim())
                          .ToList();
        }

    }
}
