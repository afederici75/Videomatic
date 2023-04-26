namespace Company.Videomatic.Application.Model.Query;

public record FilterSettings(    
    IEnumerable<FilterItem> Items,
    FilterType FilterType = FilterType.Any);