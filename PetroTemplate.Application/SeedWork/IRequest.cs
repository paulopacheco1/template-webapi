using PetroTemplate.Domain.Repositories;

namespace PetroTemplate.Application.SeedWork;

public interface IRequest<TResponse> : MediatR.IRequest<TResponse>
{
}

public abstract class RequestHandler<TRequest, TResponse> : MediatR.IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    protected readonly IUnitOfWork _unitOfWork;

    protected RequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
