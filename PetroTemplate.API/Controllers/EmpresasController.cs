using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetroTemplate.Application.Services.EmpresaServices.AddEmpresaFilial;
using PetroTemplate.Application.Services.EmpresaServices.CreateEmpresa;
using PetroTemplate.Application.Services.EmpresaServices.GetEmpresa;
using PetroTemplate.Application.Services.EmpresaServices.ListEmpresas;
using PetroTemplate.Application.Services.EmpresaServices.RemoveEmpresa;
using PetroTemplate.Application.Services.EmpresaServices.RemoveEmpresaFilial;
using PetroTemplate.Application.Services.EmpresaServices.UpdateEmpresa;

namespace PetroTemplate.API.Controllers;

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
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

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
