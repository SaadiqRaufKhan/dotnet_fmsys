using FastEndpoints;
using fleetsystem.dto;
using fleetsystem.entity;
using fleetsystem.repository;

namespace fleetsystem.endpoints
{
    public class AddTruckEndpoint : Endpoint<TruckRequest, TruckResponse>
    {
        private readonly ITruckRepository _repository;

        public AddTruckEndpoint(ITruckRepository repository)
        {
            _repository = repository;
        }

        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("/api/trucks");
            AllowAnonymous();
        }

        public override async Task HandleAsync(TruckRequest req, CancellationToken ct)
        {
            var truck = new Truck {
                Name = req.Name,
                Model = req.Model,
                Year = req.Year
            };

            var createdTruck = await _repository.AddTruckAsync(truck);

            var response = new TruckResponse
            {
                Id = createdTruck.Id,
                Model = createdTruck.Model,
                Name = createdTruck.Name,
                Year = createdTruck.Year
            };

            await SendAsync(response, 201);
        }
    }
}
