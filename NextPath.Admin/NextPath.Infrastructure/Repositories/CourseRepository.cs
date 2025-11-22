using Microsoft.EntityFrameworkCore;
using NextPath.Domain.Entities;
using NextPath.Domain.Interfaces;
using NextPath.Infrastructure.Data;

namespace NextPath.Infrastructure.Repositories;

public class CourseRepository : Repository<Course>, ICourseRepository
{
    public CourseRepository(NextPathDbContext context) : base(context)
    {
    }

    public async Task<(IReadOnlyList<Course> Items, int TotalCount)> SearchPagedAsync(
        string? term,
        string? skillLevel,
        int pageNumber,
        int pageSize,
        string? orderBy)
    {
        var query = _dbSet.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(term))
        {
            var lower = term.ToLower();
            query = query.Where(c => c.Title.ToLower().Contains(lower) ||
                                     (c.Description != null && c.Description.ToLower().Contains(lower)));
        }

        if (!string.IsNullOrWhiteSpace(skillLevel))
        {
            var levelLower = skillLevel.ToLower();
            query = query.Where(c => c.SkillLevel.ToLower() == levelLower);
        }

        query = orderBy?.ToLower() switch
        {
            "title" => query.OrderBy(c => c.Title),
            "title_desc" => query.OrderByDescending(c => c.Title),
            "hours" => query.OrderBy(c => c.EstimatedHours),
            "hours_desc" => query.OrderByDescending(c => c.EstimatedHours),
            _ => query.OrderBy(c => c.Title)
        };

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }
}
