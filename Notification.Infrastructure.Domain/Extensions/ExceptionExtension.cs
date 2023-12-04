using Notification.Infrastructure.Domain.Common.Exceptions;
using System.Reflection.Metadata.Ecma335;

namespace Notification.Infrastructure.Domain.Extensions;

public static  class ExceptionExtension
{
    public static async ValueTask<FuncResult<T>> GetValueAsync<T>(this Func<Task<T>> func) where T : struct
    {
        FuncResult<T> result;
        try 
        {
            result = new FuncResult<T>(await func());
        }
        catch (Exception ex) 
        {
            result = new FuncResult<T>(ex);
        }

        return result;
    }

    public static async ValueTask<FuncResult<T>> GetValueAsync<T>(this Func<ValueTask<T>> func) where T : struct
    {
        FuncResult<T> result;
        try
        {
            result = new FuncResult<T>(await func());
        }
        catch (Exception ex)
        {
            result = new FuncResult<T>(ex);
        }

        return result;
    }
}
