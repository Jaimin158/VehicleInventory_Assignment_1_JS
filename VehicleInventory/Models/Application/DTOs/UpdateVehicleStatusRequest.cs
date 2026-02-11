using VehicleInventory.Models.Domain.Enums;

namespace VehicleInventory.Models.Application.DTOs;

public record UpdateVehicleStatusRequest(VehicleStatus Status);
