using FastEndpoints;
using fleetsystem.repository;

namespace fleetsystem.endpoints
{
    public class DeleteTruckEndpoint : Endpoint<int>
    {
        private readonly ITruckRepository _repository;

        public DeleteTruckEndpoint(ITruckRepository repository)
        {
            _repository = repository;
        }

        public override void Configure()
        {
            Verbs(Http.DELETE);
            Routes("/api/trucks/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(int id, CancellationToken ct)
        {
            var success = await _repository.DeleteTruckAsync(id);
            if (!success)
            {
                await SendNotFoundAsync();
                return;
            }

            await SendOkAsync();
        }
    }
}
