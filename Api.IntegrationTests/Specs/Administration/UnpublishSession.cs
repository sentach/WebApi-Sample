using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Api.IntegrationTests.Infrastructure;
using Api.IntegrationTests.Infrastructure.CollectionFixtures;
using Microsoft.Owin.Testing;
using Xunit;

namespace Api.IntegrationTests.Specs.Administration
{
    [Collection(Collections.Database)]
    public class UnpublishSession
    {
        private readonly DatabaseFixture _fixture;

        private readonly int _cinemaId;
        private readonly int _sessionId;

        public UnpublishSession(DatabaseFixture fixture)
        {
            _fixture = fixture;

            _cinemaId = _fixture.SeedData.Cinema.Id;
            _sessionId = _fixture.SeedData.Sessions.First().Id;
        }

        [Fact]
        public async Task PublishSession_Should_Publish_Session()
        {
            var endpoint = $"api/v1/cinemas/{_cinemaId}/sessions/{_sessionId}/publish";
            var response = await _fixture.Server.CreateRequest(endpoint)
                .WithIdentity(Identities.Administrator)
                .SendAsync(HttpMethod.Delete.Method);

            await response.IsSuccessStatusCodeOrTrow();
        }
    }
}