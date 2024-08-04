namespace eCommerceMicroservicesV2.BuildingBlocks.Pagination;

public record PaginationRequest(int PageIndex = 0, int PageSize = 10);
