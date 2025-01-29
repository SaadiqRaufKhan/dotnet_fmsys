using FastEndpoints;
using fleetsystem.dto;
using fleetsystem.entity;
using fleetsystem.repository;

namespace fleetsystem.endpoints
{
    public class AddDriverEndpoint : Endpoint<DriverRequest, DriverResponse>
    {
        private readonly IDriverRepository _repository;

        public AddDriverEndpoint(IDriverRepository repository)
        {
            _repository = repository;
        }

        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("/api/drivers");
            AllowAnonymous();
        }

        public override async Task HandleAsync(DriverRequest req, CancellationToken ct)
        {
            var driver = new Driver {
                Name = req.Name,
                LicenseNumber = req.LicenseNumber,
                ContactInformation = req.ContactInformation
            };

            var createdDriver = await _repository.AddDriverAsync(driver);

            var response = new DriverResponse
            {
                Id = createdDriver.Id,
                Name = createdDriver.Name,
                LicenseNumber = createdDriver.LicenseNumber,
                ContactInformation = createdDriver.ContactInformation
            };

            await SendAsync(response, 201);
        }
    }
}
