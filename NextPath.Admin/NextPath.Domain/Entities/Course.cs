namespace NextPath.Domain.Entities;

public class Course : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string SkillLevel { get; set; } = "Beginner";
    public int EstimatedHours { get; set; }

    public ICollection<LearningPathCourse> LearningPathCourses { get; set; } = new List<LearningPathCourse>();
}
