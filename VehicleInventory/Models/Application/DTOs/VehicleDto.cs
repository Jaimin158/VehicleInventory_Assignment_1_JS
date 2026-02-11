using VehicleInventory.Models.Domain.Enums;

namespace VehicleInventory.Models.Application.DTOs;

public record VehicleDto(Guid Id, string VehicleCode, Guid LocationId, string VehicleType, VehicleStatus Status);
