using Microsoft.AspNetCore.Mvc;
using NextPath.Application.DTOs;
using NextPath.Application.Interfaces;

namespace NextPath.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;
    private readonly LinkGenerator _linkGenerator;

    public CoursesController(ICourseService courseService, LinkGenerator linkGenerator)
    {
        _courseService = courseService;
        _linkGenerator = linkGenerator;
    }

    [HttpGet("search")]
    public async Task<ActionResult<PagedResult<CourseDto>>> Search(
        [FromQuery] string? term,
        [FromQuery] string? skillLevel,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? orderBy = null)
    {
        if (pageNumber <= 0 || pageSize <= 0)
        {
            return ValidationProblem("pageNumber and pageSize must be greater than zero.");
        }

        var result = await _courseService.SearchAsync(
            term,
            skillLevel,
            pageNumber,
            pageSize,
            orderBy,
            (id, dto) => AddLinks(id, dto));

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<CourseDto>>> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _courseService.SearchAsync(
            term: null,
            skillLevel: null,
            pageNumber,
            pageSize,
            orderBy: null,
            (id, dto) => AddLinks(id, dto));

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CourseDto>> GetById(Guid id)
    {
        var course = await _courseService.GetByIdAsync(id, (cid, dto) => AddLinks(cid, dto));
        if (course is null) return NotFound();

        return Ok(course);
    }

    [HttpPost]
    public async Task<ActionResult<CourseDto>> Create([FromBody] CourseCreateDto dto)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var created = await _courseService.CreateAsync(dto, (id, c) => AddLinks(id, c));

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CourseUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var updated = await _courseService.UpdateAsync(id, dto);
        if (!updated) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _courseService.DeleteAsync(id);
        if (!deleted) return NotFound();

        return NoContent();
    }

    private CourseDto AddLinks(Guid id, CourseDto dto)
    {
        var self = _linkGenerator.GetPathByAction(HttpContext,
            action: nameof(GetById),
            controller: "Courses",
            values: new { id });

        var update = _linkGenerator.GetPathByAction(HttpContext,
            action: nameof(Update),
            controller: "Courses",
            values: new { id });

        var delete = _linkGenerator.GetPathByAction(HttpContext,
            action: nameof(Delete),
            controller: "Courses",
            values: new { id });

        dto.Links.Clear();
        if (self is not null)
            dto.Links.Add(new LinkDto("self", self, "GET"));
        if (update is not null)
            dto.Links.Add(new LinkDto("update", update, "PUT"));
        if (delete is not null)
            dto.Links.Add(new LinkDto("delete", delete, "DELETE"));

        return dto;
    }
}
