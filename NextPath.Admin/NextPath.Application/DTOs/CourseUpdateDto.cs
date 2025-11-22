using System.ComponentModel.DataAnnotations;

namespace NextPath.Application.DTOs;

public class CourseUpdateDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    [Required]
    [MaxLength(50)]
    public string SkillLevel { get; set; } = "Beginner";

    [Range(1, 500)]
    public int EstimatedHours { get; set; }
}
