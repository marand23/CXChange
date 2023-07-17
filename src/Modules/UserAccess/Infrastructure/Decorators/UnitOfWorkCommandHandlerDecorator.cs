using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Domain.User;
using MassTransit.Mediator;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserAccess.Infrastructure.Decorators
{
    public class UnitOfWorkCommandHandlerDecorator<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private IRequestHandler<TRequest, TResponse> _handler;
        private IUnitOfWork _unitOfWork;

        public UnitOfWorkCommandHandlerDecorator(IRequestHandler<TRequest, TResponse> handler, IUnitOfWork unitOfWork, IDomainEventDispatcher domainEventDispatcher)
        {
            _handler = handler;
            _unitOfWork = unitOfWork;
         }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Decorator B - Before handling the command");
            var response=  await _handler.Handle(request, cancellationToken);
            
            Console.WriteLine("Decorator B - After handling the command");
            await _unitOfWork.CommitChangesAsync();
         
            return response;

        }
    }
   
}
