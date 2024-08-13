using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Services.EmpresaServices.AddEmpresaFilial;
using Template.Application.Services.EmpresaServices.CreateEmpresa;
using Template.Application.Services.EmpresaServices.GetEmpresa;
using Template.Application.Services.EmpresaServices.ListEmpresas;
using Template.Application.Services.EmpresaServices.RemoveEmpresa;
using Template.Application.Services.EmpresaServices.RemoveEmpresaFilial;
using Template.Application.Services.EmpresaServices.UpdateEmpresa;

namespace Template.API.Controllers;

[ApiController]
[Route("api/empresas")]
public class EmpresasController : ControllerBase
{
    protected readonly IMediator Mediator;

    public EmpresasController(IMediator mediator)
    {
        Mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] ListEmpresasRequest request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var request = new GetEmpresaRequest();
        request.SetEmpresaId(id);

        var response = await Mediator.Send(request);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmpresaRequest request)
    {
        var empresaId = await Mediator.Send(request);
        return CreatedAtAction(nameof(Get), new { id = empresaId }, request);
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateEmpresaRequest request)
    {
        request.SetEmpresaId(id);

        var empresaId = await Mediator.Send(request);
        return Ok(empresaId);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var request = new RemoveEmpresaRequest();
        request.SetEmpresaId(id);

        await Mediator.Send(request);
        return NoContent();
    }

    [HttpPut("{id:Guid}/filiais")]
    public async Task<IActionResult> AddFilial([FromRoute] Guid id, [FromBody] AddEmpresaFilialRequest request)
    {
        request.SetEmpresaId(id);

        var filialId = await Mediator.Send(request);
        return Ok(filialId);
    }

    [HttpDelete("{id:Guid}/filiais/{filialId:Guid}")]
    public async Task<IActionResult> RemoveFilial([FromRoute] Guid id, [FromRoute] Guid filialId)
    {
        var request = new RemoveEmpresaFilialRequest();
        request.SetEmpresaId(id);
        request.SetFilialId(filialId);

        await Mediator.Send(request);
        return NoContent();
    }
}
