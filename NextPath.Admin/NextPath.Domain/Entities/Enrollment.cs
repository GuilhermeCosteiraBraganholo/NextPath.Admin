namespace NextPath.Domain.Entities;

public class Enrollment : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;

    public Guid LearningPathId { get; set; }
    public LearningPath LearningPath { get; set; } = null!;

    public double ProgressPercentage { get; set; }
    public DateTime? LastAccessAt { get; set; }
}
