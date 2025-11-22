using NextPath.Application.DTOs;
using NextPath.Application.Interfaces;
using NextPath.Domain.Entities;
using NextPath.Domain.Interfaces;

namespace NextPath.Application.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;

    public CourseService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<PagedResult<CourseDto>> SearchAsync(
        string? term,
        string? skillLevel,
        int pageNumber,
        int pageSize,
        string? orderBy,
        Func<Guid, CourseDto, CourseDto> linksFactory)
    {
        var (items, totalCount) = await _courseRepository.SearchPagedAsync(
            term,
            skillLevel,
            pageNumber,
            pageSize,
            orderBy);

        var dtos = items.Select(c =>
        {
            var dto = new CourseDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                SkillLevel = c.SkillLevel,
                EstimatedHours = c.EstimatedHours
            };
            return linksFactory(c.Id, dto);
        }).ToList();

        return new PagedResult<CourseDto>
        {
            Items = dtos,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<CourseDto?> GetByIdAsync(Guid id, Func<Guid, CourseDto, CourseDto> linksFactory)
    {
        var course = await _courseRepository.GetByIdAsync(id);
        if (course is null) return null;

        var dto = new CourseDto
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description,
            SkillLevel = course.SkillLevel,
            EstimatedHours = course.EstimatedHours
        };

        return linksFactory(course.Id, dto);
    }

    public async Task<CourseDto> CreateAsync(CourseCreateDto dto, Func<Guid, CourseDto, CourseDto> linksFactory)
    {
        var entity = new Course
        {
            Title = dto.Title,
            Description = dto.Description,
            SkillLevel = dto.SkillLevel,
            EstimatedHours = dto.EstimatedHours
        };

        await _courseRepository.AddAsync(entity);

        var result = new CourseDto
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            SkillLevel = entity.SkillLevel,
            EstimatedHours = entity.EstimatedHours
        };

        return linksFactory(entity.Id, result);
    }

    public async Task<bool> UpdateAsync(Guid id, CourseUpdateDto dto)
    {
        var entity = await _courseRepository.GetByIdAsync(id);
        if (entity is null) return false;

        entity.Title = dto.Title;
        entity.Description = dto.Description;
        entity.SkillLevel = dto.SkillLevel;
        entity.EstimatedHours = dto.EstimatedHours;
        entity.UpdatedAt = DateTime.UtcNow;

        await _courseRepository.UpdateAsync(entity);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _courseRepository.GetByIdAsync(id);
        if (entity is null) return false;

        await _courseRepository.DeleteAsync(entity);
        return true;
    }
}
