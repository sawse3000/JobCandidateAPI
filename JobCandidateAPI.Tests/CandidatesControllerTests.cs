using Microsoft.EntityFrameworkCore;
using Xunit;
using JobCandidateAPI.Controllers;
using JobCandidateAPI.Data;
using JobCandidateAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobCandidateAPI.Tests
{
    public class CandidatesControllerTests
    {
        private CandidatesController GetController()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var context = new ApplicationDbContext(options);
            return new CandidatesController(context);
        }

        [Fact]
        public async Task AddOrUpdateCandidate_CreatesNewCandidate()
        {
            var controller = GetController();
            var candidate = new Candidate
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                BestCallTime = "Morning",
                LinkedInUrl = "https://linkedin.com/in/johndoe",
                GitHubUrl = "https://github.com/johndoe",
                Comments = "Sample candidate"
            };

            var result = await controller.AddOrUpdateCandidate(candidate);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddOrUpdateCandidate_UpdatesExistingCandidate()
        {
            var controller = GetController();
            var candidate = new Candidate
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                BestCallTime = "Morning",
                LinkedInUrl = "https://linkedin.com/in/johndoe",
                GitHubUrl = "https://github.com/johndoe",
                Comments = "Sample candidate"
            };

            await controller.AddOrUpdateCandidate(candidate);

            candidate.FirstName = "Jane";
            var result = await controller.AddOrUpdateCandidate(candidate);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
