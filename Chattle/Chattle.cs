using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Serilog;

namespace Chattle
{
    public class Chattle
    {
        internal Chattle(IServiceProvider serviceProvider)
        {
            _mediator = serviceProvider.GetRequiredService<IMediator>();
            _logger = serviceProvider.GetRequiredService<ILogger>();

            _logger.Information("Application started");
        }

        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public Task<TResponse> SendRequest<TResponse>(IRequest<TResponse> request)
        {
            _logger.Verbose("Sending request {Request}", request);
            return _mediator.Send(request);
        }
    }
}