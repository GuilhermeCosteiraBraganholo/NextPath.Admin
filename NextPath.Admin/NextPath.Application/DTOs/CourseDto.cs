namespace NextPath.Application.DTOs;

public class CourseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string SkillLevel { get; set; } = string.Empty;
    public int EstimatedHours { get; set; }
    public List<LinkDto> Links { get; set; } = new();
}
