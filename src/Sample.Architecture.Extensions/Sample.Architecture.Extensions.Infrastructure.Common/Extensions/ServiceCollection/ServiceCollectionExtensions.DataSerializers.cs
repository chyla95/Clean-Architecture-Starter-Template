using Microsoft.Extensions.DependencyInjection;
using Sample.Architecture.Extensions.Application.Common.DataSerializers;
using Sample.Architecture.Extensions.Application.Common.Factories;
using Sample.Architecture.Extensions.Infrastructure.Common.DataSerializers;
using Sample.Architecture.Extensions.Infrastructure.Common.Factories;

namespace Sample.Architecture.Extensions.Infrastructure.Common.Extensions.ServiceCollection;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommonDataSerializers(this IServiceCollection services)
    {
        services.AddTransient<IJsonDataSerializer, JsonDataSerializer>();
        services.AddTransient<IXmlDataSerializer, XmlDataSerializer>();

        services.AddTransient<IDataSerializerFactory, DataSerializerFactory>();

        return services;
    }
}
