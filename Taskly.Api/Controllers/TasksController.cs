using Microsoft.AspNetCore.Mvc;
using Taskly.Application.DTOs;
using Taskly.Application.Services;


namespace Taskly.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskItemService _service;
    public TasksController(ITaskItemService service) => _service = service;


    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TaskItemDto>>> GetAll(CancellationToken ct)
    => Ok(await _service.GetAllAsync(ct));


    [HttpGet("{id:int}")]
    public async Task<ActionResult<TaskItemDto>> GetById(int id, CancellationToken ct)
    => (await _service.GetAsync(id, ct)) is { } dto ? Ok(dto) : NotFound();


    [HttpPost]
    public async Task<ActionResult<TaskItemDto>> Create([FromBody] CreateTaskItemRequest req, CancellationToken ct)
    {
        var created = await _service.CreateAsync(req, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }


    [HttpPut("{id:int}")]
    public async Task<ActionResult<TaskItemDto>> Update(int id, [FromBody] UpdateTaskItemRequest req, CancellationToken ct)
    => (await _service.UpdateAsync(id, req, ct)) is { } dto ? Ok(dto) : NotFound();


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    => (await _service.DeleteAsync(id, ct)) ? NoContent() : NotFound();


    [HttpPost("{id:int}/complete")]
    public async Task<IActionResult> Complete(int id, CancellationToken ct)
    => (await _service.CompleteAsync(id, ct)) ? NoContent() : NotFound();


    [HttpPost("{id:int}/reopen")]
    public async Task<IActionResult> Reopen(int id, CancellationToken ct)
    => (await _service.ReopenAsync(id, ct)) ? NoContent() : NotFound();
}