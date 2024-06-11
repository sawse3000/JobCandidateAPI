using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobCandidateAPI.Data;
using JobCandidateAPI.Models;

namespace JobCandidateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CandidatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCandidate(Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                var existingCandidate = await _context.Candidates
                    .FirstOrDefaultAsync(c => c.Email == candidate.Email);
                if (existingCandidate != null)
                {
                    existingCandidate.FirstName = candidate.FirstName;
                    existingCandidate.LastName = candidate.LastName;
                    existingCandidate.PhoneNumber = candidate.PhoneNumber;
                    existingCandidate.BestCallTime = candidate.BestCallTime;
                    existingCandidate.LinkedInUrl = candidate.LinkedInUrl;
                    existingCandidate.GitHubUrl = candidate.GitHubUrl;
                    existingCandidate.Comments = candidate.Comments;

                    _context.Candidates.Update(existingCandidate);
                }
                else
                {
                    _context.Candidates.Add(candidate);
                }

                await _context.SaveChangesAsync();
                return Ok(candidate);
            }

            return BadRequest(ModelState);
        }
    }
}
