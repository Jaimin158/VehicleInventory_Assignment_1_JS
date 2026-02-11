using VehicleInventory.Models.Domain.Entities;

namespace VehicleInventory.Models.Application.Interfaces
{
    public interface IVehicleRepository
    {
        Task AddAsync(Vehicle vehicle, CancellationToken ct);
        Task<Vehicle?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<List<Vehicle>> GetAllAsync(CancellationToken ct);
        Task UpdateAsync(Vehicle vehicle, CancellationToken ct);
        Task DeleteAsync(Vehicle vehicle, CancellationToken ct);
        Task<bool> VehicleCodeExistsAsync(string vehicleCode, CancellationToken ct);
    }
}
