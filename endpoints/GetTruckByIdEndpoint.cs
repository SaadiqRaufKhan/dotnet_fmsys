using FastEndpoints;
using fleetsystem.repository;
using fleetsystem.dto;

public class GetTruckByIdEndpoint : EndpointWithoutRequest<TruckResponse> {

    private readonly ITruckRepository _repository;

    public GetTruckByIdEndpoint(ITruckRepository repository) {
        _repository = repository;
    }

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("api/trucks/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<int>("id"); // Get the route parameter
        var truck = await _repository.GetTruckByIdAsync(id);
        if (truck == null)
        {
            await SendNotFoundAsync();
            return;
        }

        var response = new TruckResponse
        {
            Id = truck.Id,
            Model = truck.Model,
            Name = truck.Name,
            Year = truck.Year
        };

        await SendAsync(response);
    }

}