using System.Reflection;
using Twilio.Rest.Api.V2010.Account.Usage.Record;

namespace Notification.Infrastructure.Api.Configurations;

public static partial class HostConfiguration
{
    private static readonly ICollection<Assembly> Assemblies; 
    static HostConfiguration()
    {
        Assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToList();
        Assemblies.Add(Assembly.GetExecutingAssembly());
    }

    private static WebApplicationBuilder AddValidators(this WebApplicationBuilder builder) 
    {
        builder.Services.AddValidatorsFromAssemblies(Assemblies);

        return builder;
    }

    private static WebApplicationBuilder AddMappers(this WebApplicationBuilder builder) 
    {
        builder.Services.AddAutoMapper(Assemblies);

        return builder;
    }

    private static WebApplicationBuilder AddNotificationInfrastructure()
}
