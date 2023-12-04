using Notification.Infrastructure.Application.Common.Models.Querying;

namespace Notification.Infrastructure.Application.Common.Querying.Extensions;

public static class LinqExtension
{
    public static IQueryable<TSourse> ApplyPagination<TSourse>
        (
        this IQueryable<TSourse> sourse,
        FilterPagination filterPagination
        )
    => sourse.Skip((filterPagination.PageToken - 1) * filterPagination.PageSize).Take(filterPagination.PageSize);

    public static IEnumerable<TSourse> ApplyPagination<TSourse>
        (
        this IEnumerable<TSourse> sourse,
        FilterPagination filterPagination
        )
    => sourse.Skip((filterPagination.PageToken - 1) * filterPagination.PageSize).Take(filterPagination.PageSize);
}
