namespace VehicleInventory.Models.Application.DTOs;

public record CreateVehicleRequest(string VehicleCode, Guid LocationId, string VehicleType);
public record CreateVehicleResponse(Guid Id);
