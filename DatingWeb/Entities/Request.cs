using System.ComponentModel.DataAnnotations;
using CommonFiles.Enums;

namespace DatingWeb.Entities;

public class Request
{
    [Key]
    public Guid RequestId { get; set; }

    public required Guid FromUser { get; set; }
    public required Guid ToUser { get; set; }

    public required string Text { get; set; }

    public ERequest Status { get; set; }
}