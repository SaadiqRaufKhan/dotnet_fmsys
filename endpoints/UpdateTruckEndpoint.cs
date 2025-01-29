using FastEndpoints;
using fleetsystem.dto;
using fleetsystem.entity;
using fleetsystem.repository;

namespace YourNamespace.endpoints
{
    public class UpdateTruckEndpoint : Endpoint<TruckRequest, TruckResponse>
    {
        private readonly ITruckRepository _repository;

        public UpdateTruckEndpoint(ITruckRepository repository)
        {
            _repository = repository;
        }

        public override void Configure()
        {
            Verbs(Http.PUT);
            Routes("/api/trucks/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(TruckRequest request, CancellationToken ct)
        {
            // Fetch the truck from the repository by ID
            // var existingTruck = await _repository.GetTruckByIdAsync(request.Id);
            var truckId = Route<int>("id"); // Extract id from route parameter
    
            // Fetch the truck from the repository by ID
            var existingTruck = await _repository.GetTruckByIdAsync(truckId);
    
            if (existingTruck == null)
            {
                await SendNotFoundAsync();
                return;
            }
    
            // Map TruckRequest to Truck entity
            existingTruck.Name = request.Name;
            existingTruck.Model = request.Model;
            existingTruck.Year = request.Year;

            
            // Validation example: Check if Year is valid
            if (request.Year < 1900 || request.Year > DateTime.Now.Year)
            {
                // Create validation failures
                var failures = new[] {
                    new ValidationFailure("Invalid year provided.", $"Year should be between 1900 and {DateTime.Now.Year}.")
                };

                // Throw a ValidationFailureException with the validation failures
                throw new ValidationFailureException(failures);
            }
    
            // Update the truck in the repository
            var updatedTruck = await _repository.UpdateTruckAsync(existingTruck);
    
            // Map the updated truck entity to a response DTO
            var response = new TruckResponse
            {
                Id = updatedTruck.Id,
                Name = updatedTruck.Name,
                Model = updatedTruck.Model,
                Year = updatedTruck.Year
            };
    
            // Send the response back
            await SendAsync(response);
        }
    }
}

/*

            var truck = new Truck
            {
                Name = req.Name,
                Model = req.Model,
                Year = req.Year
            };

            var updatedTruck = await _repository.UpdateTruckAsync(truck);
            if (updatedTruck == null)
            {
                await SendNotFoundAsync();
                return;
            }

            var response = new TruckResponse
            {
                Id = updatedTruck.Id,
                Name = updatedTruck.Name,
                Model = updatedTruck.Model,
                Year = updatedTruck.Year
            };

            await SendAsync(response);

*/

