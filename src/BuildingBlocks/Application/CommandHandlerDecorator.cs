using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection.Metadata;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using CXChange.BuildingBlocks.Domain;
using MassTransit;
using MassTransit.Mediator;
using static MassTransit.MessageHeaders;


namespace CXChange.BuildingBlocks.Application
{


    public class CommandHandlerDecorator<TRequest> : IConsumer<TRequest> where TRequest : class
    {
        private IConsumer<TRequest> _decorated;
        private IUnitOfWork _unitOfWork;
        public CommandHandlerDecorator(IConsumer<TRequest> decorated, IUnitOfWork unitOfWork)
        {
            _decorated = decorated;
            _unitOfWork = unitOfWork;   
        }

        public async Task Consume(ConsumeContext<TRequest> context)
        {
          await  _decorated.Consume(context);
          await  _unitOfWork.CommitChangesAsync();

        }
    }
}



