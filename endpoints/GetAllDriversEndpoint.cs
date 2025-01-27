using FastEndpoints;
using fleetsystem.repository;
using fleetsystem.dto;
namespace fleetsystem.endpoints;

public class GetAllDriversEndpoint : EndpointWithoutRequest<IEnumerable<DriverResponse>>
{
    private readonly IDriverRepository _repository;

    public GetAllDriversEndpoint(IDriverRepository repository)
    {
        _repository = repository;
    }

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("api/drivers");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var drivers = await _repository.GetAllDriversAsync();
        // var response = drivers.Select(d => new DriverResponse
        // {
        //     Id = d.Id,
        //     Name = d.Name,
        //     LicenseNumber = d.LicenseNumber,
        //     ContactInformation = d.ContactInformation
        // }).ToList();

        // await SendAsync(response);

        await SendAsync(drivers.Select(d => new DriverResponse
        {
            Id = d.Id,
            Name = d.Name,
            LicenseNumber = d.LicenseNumber,
            ContactInformation = d.ContactInformation
        }));
    }
}
