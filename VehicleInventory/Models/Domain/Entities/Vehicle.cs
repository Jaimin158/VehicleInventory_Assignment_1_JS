
using VehicleInventory.Models.Domain.Enums;
using VehicleInventory.Models.Domain.Exceptions;

namespace VehicleInventory.Models.Domain.Entities;



public class Vehicle
{
    private Vehicle() { }

    public Guid Id { get; private set; } = Guid.NewGuid();
    public string VehicleCode { get; private set; } = default!;
    public Guid LocationId { get; private set; }
    public string VehicleType { get; private set; } = default!;
    public VehicleStatus Status { get; private set; } = VehicleStatus.Available;

    public Vehicle(string vehicleCode, Guid locationId, string vehicleType)
    {
        if (string.IsNullOrWhiteSpace(vehicleCode))
            throw new DomainException("VehicleCode is required.");
        if (vehicleCode.Trim().Length > 20)
            throw new DomainException("VehicleCode must be 20 characters or less.");

        if (locationId == Guid.Empty)
            throw new DomainException("LocationId is required.");

        if (string.IsNullOrWhiteSpace(vehicleType))
            throw new DomainException("VehicleType is required.");
        if (vehicleType.Trim().Length > 50)
            throw new DomainException("VehicleType must be 50 characters or less.");

        VehicleCode = vehicleCode.Trim();
        LocationId = locationId;
        VehicleType = vehicleType.Trim();
        Status = VehicleStatus.Available;
    }

    public void MarkAvailable()
    {

        if (Status == VehicleStatus.Reserved)
            throw new DomainException("Reserved vehicle cannot be marked available without explicit release.");

        Status = VehicleStatus.Available;
    }

    public void MarkRented()
    {
        
        if (Status == VehicleStatus.Rented)
            throw new DomainException("Vehicle is already rented.");
        if (Status == VehicleStatus.Reserved)
            throw new DomainException("Vehicle is reserved and cannot be rented.");
        if (Status == VehicleStatus.Serviced)
            throw new DomainException("Vehicle is under service and cannot be rented.");

        Status = VehicleStatus.Rented;
    }

    public void MarkReserved()
    {
        if (Status == VehicleStatus.Reserved)
            throw new DomainException("Vehicle is already reserved.");
        if (Status == VehicleStatus.Rented)
            throw new DomainException("Vehicle is rented and cannot be reserved.");
        if (Status == VehicleStatus.Serviced)
            throw new DomainException("Vehicle is under service and cannot be reserved.");

        Status = VehicleStatus.Reserved;
    }

    public void MarkServiced()
    {
        if (Status == VehicleStatus.Serviced)
            throw new DomainException("Vehicle is already under service.");
        if (Status == VehicleStatus.Rented)
            throw new DomainException("Vehicle is rented and cannot be serviced.");
        if (Status == VehicleStatus.Reserved)
            throw new DomainException("Reserved vehicle cannot be serviced without explicit release.");

        Status = VehicleStatus.Serviced;
    }
}
