namespace Sample.Architecture.Extensions.Application.Common.DataSerializers;
public interface IDataSerializer
{
    Task<string> SerializeAsync<T>(T deserializedValue, CancellationToken cancellationToken = default)
        where T : class;

    Task<T> DeserializeAsync<T>(string serializedValue, CancellationToken cancellationToken = default)
        where T : class;
}
