namespace PaymentsService.Persistence.Entities;

using System.ComponentModel.DataAnnotations;

public class Account
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal Balance { get; set; }
    
    [Timestamp]
    public uint Version { get; set; }
}