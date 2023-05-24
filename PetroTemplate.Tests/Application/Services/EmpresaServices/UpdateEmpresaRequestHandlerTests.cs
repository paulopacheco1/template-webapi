using System.Net;
using System.Reflection;
using PetroTemplate.Application.Services.EmpresaServices.UpdateEmpresa;
using PetroTemplate.Domain.Aggregates.EmpresaAggregate;
using PetroTemplate.Domain.Exceptions;
using PetroTemplate.Domain.Repositories;
using PetroTemplate.Domain.Seedwork;

namespace PetroTemplate.Tests.Application.Services.EmpresaServices;

public class UpdateEmpresaRequestHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly UpdateEmpresaRequestHandler _handler;

    public UpdateEmpresaRequestHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new UpdateEmpresaRequestHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_EmpresaUpdatedAndCommitted()
    {
        // Arrange
        var empresaId = Guid.NewGuid();
        var request = new UpdateEmpresaRequest
        {
            Nome = "Nova Empresa",
            Email = "novaempresa@example.com"
        };
        request.SetEmpresaId(empresaId);

        var empresa = new Empresa("Empresa Antiga", "empresaantiga@example.com");
        typeof(Entity).GetProperty("Id", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)?.SetValue(empresa, empresaId);

        _unitOfWorkMock.Setup(uow => uow.Empresas.GetByIdAsync(empresaId))
            .ReturnsAsync(empresa);
        _unitOfWorkMock.Setup(uow => uow.Empresas.ListAsync(It.IsAny<EmpresaQueryFilter>()))
            .ReturnsAsync(new List<Empresa>());

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equal(empresaId, result);
        Assert.Equal(request.Nome, empresa.Nome);
        Assert.Equal(request.Email, empresa.Email);
        _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_UnknownEmpresaId_ThrowsAppExceptionWithNotFoundStatusCode()
    {
        // Arrange
        var empresaId = Guid.NewGuid();
        var request = new UpdateEmpresaRequest
        {
            Nome = "Nova Empresa",
            Email = "novaempresa@example.com"
        };        
        request.SetEmpresaId(empresaId);

        _unitOfWorkMock.Setup(uow => uow.Empresas.GetByIdAsync(empresaId))
            .ReturnsAsync((Empresa?)null);

        // Act
        async Task Execute() => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<AppException>(Execute);
        _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_DuplicateNome_ThrowsAppExceptionWithBadRequestStatusCode()
    {
        // Arrange
        var empresaId = Guid.NewGuid();
        var request = new UpdateEmpresaRequest
        {
            Nome = "Nova Empresa",
            Email = "novaempresa@example.com"
        };        
        request.SetEmpresaId(empresaId);

        var existingEmpresaId = Guid.NewGuid();
        var existingEmpresa = new Empresa("Empresa Existente", "empresaexistente@example.com");
        typeof(Entity).GetProperty("Id", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)?.SetValue(existingEmpresa, existingEmpresaId);

        _unitOfWorkMock.Setup(uow => uow.Empresas.GetByIdAsync(empresaId))
            .ReturnsAsync(existingEmpresa);
        _unitOfWorkMock.Setup(uow => uow.Empresas.ListAsync(It.IsAny<EmpresaQueryFilter>()))
            .ReturnsAsync(new List<Empresa>
            {
                new Empresa("Outra Empresa", "outraempresa@example.com"),
                existingEmpresa
            });

        // Act
        async Task Execute() => await _handler.Handle(request, CancellationToken.None);

        // Assert
        var exception = await Assert.ThrowsAsync<AppException>(Execute);
        Assert.Equal(HttpStatusCode.BadRequest, exception.HttpStatusCode);
        _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Never);
    }
}