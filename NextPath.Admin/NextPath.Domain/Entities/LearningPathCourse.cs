namespace NextPath.Domain.Entities;

public class LearningPathCourse
{
    public Guid LearningPathId { get; set; }
    public LearningPath LearningPath { get; set; } = null!;

    public Guid CourseId { get; set; }
    public Course Course { get; set; } = null!;

    public int Order { get; set; }
}
