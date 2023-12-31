namespace Cafe.Contracts.Menu;

public record MenuResponse(
    string Id,
    string Name,
    string Description,
    List<MenuSectionResponse> Sections,
    string HostId,
    DateTime CreatedDateTime,
    DateTime UpdatedDateTime
    );

public record MenuSectionResponse(
    string Id,
    string Name,
    string Description,
    List<MenuItemResponse> Items);

public record MenuItemResponse(
    string Id,
    string Name,
    string Description,
    string Price,
    string Currency);
