using System.Data.SqlTypes;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace Company.Videomatic.Application.Behaviors;

[System.Diagnostics.DebuggerStepThrough]
public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : class
{
    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;
    public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        //Request
        var sw = Stopwatch.StartNew();
        _logger.LogInformation("Received {request}", request);
                
        var response = await next();

        //Response
        _logger.LogInformation("Returning {response} [{Elapsed}ms].", response, sw.ElapsedMilliseconds);

        return response;
    }

}