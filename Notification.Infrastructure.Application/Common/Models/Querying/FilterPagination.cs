namespace Notification.Infrastructure.Application.Common.Models.Querying;

public class FilterPagination
{
    public int PageSize { get; set; } = 10;
    public int PageToken { get; set; } = 1;
}
