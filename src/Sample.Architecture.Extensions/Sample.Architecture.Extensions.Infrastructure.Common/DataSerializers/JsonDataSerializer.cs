using Sample.Architecture.Extensions.Application.Common.DataSerializers;
using System.Text.Json;

namespace Sample.Architecture.Extensions.Infrastructure.Common.DataSerializers;

internal sealed class JsonDataSerializer : IJsonDataSerializer
{
    public async Task<string> SerializeAsync<T>(T deserializedValue, CancellationToken cancellationToken = default)
    where T : class
    {
        if (deserializedValue is null) throw new ArgumentException($"{nameof(deserializedValue)} value cannot be null", nameof(deserializedValue));

        string serializedValue = await Task.Run(() => JsonSerializer.Serialize(deserializedValue), cancellationToken);
        if (string.IsNullOrWhiteSpace(serializedValue)) throw new InvalidOperationException($"Serialization of value '{deserializedValue}' failed");

        return serializedValue;
    }

    public async Task<T> DeserializeAsync<T>(string serializedValue, CancellationToken cancellationToken = default)
        where T : class
    {
        if (string.IsNullOrWhiteSpace(serializedValue)) throw new ArgumentException($"{nameof(serializedValue)} value cannot be null or empty", nameof(serializedValue));

        T? deserializedValue = await Task.Run(() => JsonSerializer.Deserialize<T>(serializedValue), cancellationToken);
        if (deserializedValue is null) throw new InvalidOperationException($"Deserialization of value '{serializedValue}' failed");

        return deserializedValue;
    }
}
