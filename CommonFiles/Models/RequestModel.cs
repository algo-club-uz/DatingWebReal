namespace CommonFiles.Models;

public class RequestModel
{
    public Guid RequestId { get; set; }

    public required string FromUser { get; set; }
    public required string ToUser { get; set; }

    public required string Text { get; set; }

    public required string Status { get; set; }
}