using MediatR;

namespace VideomaticBlazor.Events;

public record SearchRequested(string SearchText) : INotification;