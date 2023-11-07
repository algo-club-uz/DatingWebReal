namespace CommonFiles.Models;

public class CreateRequestModel
{ 
    public required Guid ToUser { get; set; }
    public required string Text { get; set; }
}