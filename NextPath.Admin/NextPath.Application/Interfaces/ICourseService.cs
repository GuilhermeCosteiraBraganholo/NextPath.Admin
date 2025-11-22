using NextPath.Application.DTOs;

namespace NextPath.Application.Interfaces;

public interface ICourseService
{
    Task<PagedResult<CourseDto>> SearchAsync(
        string? term,
        string? skillLevel,
        int pageNumber,
        int pageSize,
        string? orderBy,
        Func<Guid, CourseDto, CourseDto> linksFactory);

    Task<CourseDto?> GetByIdAsync(Guid id, Func<Guid, CourseDto, CourseDto> linksFactory);
    Task<CourseDto> CreateAsync(CourseCreateDto dto, Func<Guid, CourseDto, CourseDto> linksFactory);
    Task<bool> UpdateAsync(Guid id, CourseUpdateDto dto);
    Task<bool> DeleteAsync(Guid id);
}
