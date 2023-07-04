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
        _logger.LogInformation($"Handling {typeof(TRequest).Name}");
        Type myType = request.GetType();
        IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
        foreach (PropertyInfo prop in props)
        {
            object propValue = prop.GetValue(request, null)!;
            _logger.LogInformation("{Property} : {@Value}", prop.Name, propValue);
        }
        var response = await next();
        //Response
        _logger.LogInformation($"Handled {typeof(TResponse).Name}");
        return response;
    }

}