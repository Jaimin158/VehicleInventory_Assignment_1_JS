using VehicleInventory.Models.Application.DTOs;
using VehicleInventory.Models.Application.Interfaces;
using VehicleInventory.Models.Domain.Entities;
using VehicleInventory.Models.Domain.Enums;
using VehicleInventory.Models.Domain.Exceptions;

namespace VehicleInventory.Models.Application.Services
{
    public class VehicleService
    {
        private readonly IVehicleRepository _repo;

        public VehicleService(IVehicleRepository repo)
        {
            _repo = repo;
        }

        public async Task<CreateVehicleResponse> CreateVehicle(CreateVehicleRequest req, CancellationToken ct)
        {
            // make vehicle 
            var code = req.VehicleCode.Trim();
            if (await _repo.VehicleCodeExistsAsync(code, ct))
                throw new DomainException("VehicleCode must be unique.");

            var vehicle = new Vehicle(req.VehicleCode, req.LocationId, req.VehicleType);

            await _repo.AddAsync(vehicle, ct);

            return new CreateVehicleResponse(vehicle.Id);
        }

        public async Task<VehicleDto> GetVehicleById(Guid id, CancellationToken ct)
        {
            var vehicle = await _repo.GetByIdAsync(id, ct);

            if (vehicle == null)
                throw new DomainException("Vehicle not found.");

            return MapToDto(vehicle);
        }

        public async Task<List<VehicleDto>> GetAllVehicles(CancellationToken ct)
        {
            var vehicles = await _repo.GetAllAsync(ct);
            return vehicles.Select(MapToDto).ToList();
        }

        public async Task UpdateVehicleStatus(Guid id, UpdateVehicleStatusRequest req, CancellationToken ct)
        {
            var vehicle = await _repo.GetByIdAsync(id, ct);

            if (vehicle == null)
                throw new DomainException("Vehicle not found.");

          
            switch (req.Status)
            {
                case VehicleStatus.Available:
                    vehicle.MarkAvailable();
                    break;

                case VehicleStatus.Rented:
                    vehicle.MarkRented();
                    break;

                case VehicleStatus.Reserved:
                    vehicle.MarkReserved();
                    break;

                case VehicleStatus.Serviced:
                    vehicle.MarkServiced();
                    break;

                default:
                    throw new DomainException("Invalid status.");
            }

            await _repo.UpdateAsync(vehicle, ct);
        }

        public async Task DeleteVehicle(Guid id, CancellationToken ct)
        {
            var vehicle = await _repo.GetByIdAsync(id, ct);

            if (vehicle == null)
                throw new DomainException("Vehicle not found.");

            await _repo.DeleteAsync(vehicle, ct);
        }

        private static VehicleDto MapToDto(Vehicle v)
        {
            return new VehicleDto(
                v.Id,
                v.VehicleCode,
                v.LocationId,
                v.VehicleType,
                v.Status
            );
        }
    }
}
