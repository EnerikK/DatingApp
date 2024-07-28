namespace DatingApp.Application.Identity.Dtos;

public class PhotoDto
{
    public int Id { get; set; }
    public required string Url { get; set; }
    public bool IsMain { get; set; }
}