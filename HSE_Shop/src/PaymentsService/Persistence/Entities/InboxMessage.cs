using System.ComponentModel.DataAnnotations;

namespace PaymentsService.Persistence.Entities;

public class InboxMessage
{
    [Key]
    public Guid MessageId { get; set; }
    public DateTime ProcessedOn { get; set; }
}