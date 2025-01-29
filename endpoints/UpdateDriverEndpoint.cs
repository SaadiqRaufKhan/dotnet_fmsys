using FastEndpoints;
using fleetsystem.dto;
using fleetsystem.entity;
using fleetsystem.repository;

namespace YourNamespace.endpoints
{
    public class UpdateDriverEndpoint : Endpoint<DriverRequest, DriverResponse>
    {
        private readonly IDriverRepository _repository;

        public UpdateDriverEndpoint(IDriverRepository repository)
        {
            _repository = repository;
        }

        public override void Configure()
        {
            Verbs(Http.PUT);
            Routes("/api/drivers/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(DriverRequest request, CancellationToken ct)
        {
            // Fetch the Driver from the repository by ID
            // var existingDriver = await _repository.GetDriverByIdAsync(request.Id);
            var driverId = Route<int>("id"); // Extract id from route parameter
    
            // Fetch the truck from the repository by ID
            var existingDriver = await _repository.GetDriverByIdAsync(driverId);
    
            if (existingDriver == null)
            {
                await SendNotFoundAsync();
                return;
            }
    
            // Map TruckRequest to Truck entity
            existingDriver.Name = request.Name ?? existingDriver.Name;
            existingDriver.LicenseNumber = request.LicenseNumber;
            existingDriver.ContactInformation = request.ContactInformation;
    
            // Update the truck in the repository
            var updatedDriver = await _repository.UpdateDriverAsync(existingDriver);
    
            // Map the updated Driver entity to a response DTO
            var response = new DriverResponse
            {
                Id = updatedDriver.Id,
                Name = updatedDriver.Name,
                LicenseNumber = updatedDriver.LicenseNumber,
                ContactInformation = updatedDriver.ContactInformation
            };
    
            // Send the response back
            await SendAsync(response);
        }
    }
}