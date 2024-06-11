using Microsoft.AspNetCore.Mvc;
using JobCandidateAPI.Data;
using JobCandidateAPI.Models;
using System.Threading.Tasks;

namespace JobCandidateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateRepository _repository;

        public CandidatesController(ICandidateRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCandidate(Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddOrUpdateAsync(candidate);
                return Ok(candidate);
            }

            return BadRequest(ModelState);
        }
    }
}
