using CXChange.BuildingBlocks.Domain;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserAccess.Infrastructure.Decorators
{
    public class LoggingCommandHandlerDecorator<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
       where TRequest : IRequest<TResponse>
    {
        private IRequestHandler<TRequest, TResponse> _handler;
  
        private List<IDomainEvent> domainEvents;
        private ILogger _logger;
        public LoggingCommandHandlerDecorator(IRequestHandler<TRequest, TResponse> handler, IUnitOfWork unitOfWork, IDomainEventDispatcher domainEventDispatcher,ILogger logger)
        {
            _handler = handler;
            _logger = logger;
          
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Decorator A - Before handling the command");

            var response = await _handler.Handle(request, cancellationToken);
            Console.WriteLine("Decorator A - After handling the command");
            _logger.Error("executed");
            //await _unitOfWork.CommitChangesAsync();
            //await _domainEventDispatcher.Dispatch(domainEvents);
            return response;

        }
    }
}
