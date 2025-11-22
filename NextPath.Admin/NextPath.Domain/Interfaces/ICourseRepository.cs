using NextPath.Domain.Entities;

namespace NextPath.Domain.Interfaces;

public interface ICourseRepository : IRepository<Course>
{
    Task<(IReadOnlyList<Course> Items, int TotalCount)> SearchPagedAsync(
        string? term,
        string? skillLevel,
        int pageNumber,
        int pageSize,
        string? orderBy);
}
