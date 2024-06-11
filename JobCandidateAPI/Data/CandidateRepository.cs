using Microsoft.EntityFrameworkCore;
using JobCandidateAPI.Models;
using System.Threading.Tasks;

namespace JobCandidateAPI.Data
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly ApplicationDbContext _context;

        public CandidateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Candidate> GetByEmailAsync(string email)
        {
            return await _context.Candidates.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task AddOrUpdateAsync(Candidate candidate)
        {
            var existingCandidate = await GetByEmailAsync(candidate.Email);
            if (existingCandidate != null)
            {
                existingCandidate.FirstName = candidate.FirstName;
                existingCandidate.LastName = candidate.LastName;
                existingCandidate.PhoneNumber = candidate.PhoneNumber;
                existingCandidate.BestCallTime = candidate.BestCallTime;
                existingCandidate.LinkedInUrl = candidate.LinkedInUrl;
                existingCandidate.GitHubUrl = candidate.GitHubUrl;
                existingCandidate.Comments = candidate.Comments;
            }
            else
            {
                _context.Candidates.Add(candidate);
            }

            await _context.SaveChangesAsync();
        }
    }
}
