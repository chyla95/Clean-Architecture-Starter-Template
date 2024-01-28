using Sample.Architecture.Extensions.Application.Common.DataSerializers;

namespace Sample.Architecture.Extensions.Application.Common.Factories;
public interface IDataSerializerFactory
{
    IJsonDataSerializer CreateJsonDataSerializer();
    IXmlDataSerializer CreateXmlDataSerializer();
}