using FastEndpoints;
using fleetsystem.repository;

namespace fleetsystem.endpoints
{
    public class DeleteDriverEndpoint : EndpointWithoutRequest<bool> 
    {
        private readonly IDriverRepository _repository;

        public DeleteDriverEndpoint(IDriverRepository repository)
        {
            _repository = repository;
        }

        public override void Configure()
        {
            Verbs(Http.DELETE);
            Routes("/api/drivers/{id:int}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var id = Route<int>("id"); // Get the route parameter
            var result = await _repository.DeleteDriverAsync(id);
            if (!result)
            {
                await SendNotFoundAsync();
                return;
            }

            await SendNoContentAsync();
        }
    }
}
