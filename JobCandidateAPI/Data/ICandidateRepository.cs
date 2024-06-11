using JobCandidateAPI.Models;
using System.Threading.Tasks;

namespace JobCandidateAPI.Data
{
    public interface ICandidateRepository
    {
        Task<Candidate> GetByEmailAsync(string email);
        Task AddOrUpdateAsync(Candidate candidate);
    }
}
