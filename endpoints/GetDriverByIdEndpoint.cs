using FastEndpoints;
using fleetsystem.repository;
using fleetsystem.dto;

public class GetDriverByIdEndpoint : EndpointWithoutRequest<DriverResponse> {

    private readonly IDriverRepository _repository;

    public GetDriverByIdEndpoint(IDriverRepository repository) {
        _repository = repository;
    }

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("api/drivers/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<int>("id"); // Get the route parameter
        var driver = await _repository.GetDriverByIdAsync(id);
        if (driver == null)
        {
            await SendNotFoundAsync();
            return;
        }

        var response = new DriverResponse
        {
            Id = driver.Id,
            Name = driver.Name,
            LicenseNumber = driver.LicenseNumber,
            ContactInformation = driver.ContactInformation
        };

        await SendAsync(response);
    }

}