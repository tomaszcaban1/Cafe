namespace Cafe.Contracts.Common;

public record PagedResponse<T>(
    List<T> Items, 
    int TotalPages, 
    int ItemsFrom, 
    int ItemsTo, 
    int TotalItemsCount) where T : class;