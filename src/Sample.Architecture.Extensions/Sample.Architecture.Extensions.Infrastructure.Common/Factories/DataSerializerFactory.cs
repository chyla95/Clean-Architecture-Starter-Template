using Sample.Architecture.Extensions.Application.Common.DataSerializers;
using Sample.Architecture.Extensions.Application.Common.Factories;

namespace Sample.Architecture.Extensions.Infrastructure.Common.Factories;
internal sealed class DataSerializerFactory(IJsonDataSerializer jsonDataSerializer, IXmlDataSerializer xmlDataSerializer) : IDataSerializerFactory
{
    public IJsonDataSerializer CreateJsonDataSerializer() => jsonDataSerializer;
    public IXmlDataSerializer CreateXmlDataSerializer() => xmlDataSerializer;
}
