namespace NextPath.Domain.Entities;

public class LearningPath : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? TargetRole { get; set; }
    public string? Description { get; set; }

    public ICollection<LearningPathCourse> LearningPathCourses { get; set; } = new List<LearningPathCourse>();
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
