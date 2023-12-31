namespace Cafe.Contracts.Common;

public record PagedRequest(
    string? Filter,
    string? SortBy,
    bool SortByDesc,
    int PageNumber,
    int PageSize);
