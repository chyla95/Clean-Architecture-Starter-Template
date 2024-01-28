using Sample.Architecture.Extensions.Application.Common.DataSerializers;
using System.Xml.Serialization;

namespace Sample.Architecture.Extensions.Infrastructure.Common.DataSerializers;
internal sealed class XmlDataSerializer : IXmlDataSerializer
{
    public async Task<string> SerializeAsync<T>(T deserializedValue, CancellationToken cancellationToken = default)
        where T : class
    {
        if (deserializedValue is null) throw new ArgumentException($"{nameof(deserializedValue)} value cannot be null", nameof(deserializedValue));

        XmlSerializer xmlSerializer = new(typeof(T));
        using StringWriter stringWriter = new();

        await Task.Run(() => { xmlSerializer.Serialize(stringWriter, deserializedValue); }, cancellationToken);

        string serializedValue = stringWriter.ToString();
        if (string.IsNullOrWhiteSpace(serializedValue)) throw new InvalidOperationException($"Serialization of value '{deserializedValue}' failed");

        return serializedValue;
    }

    public async Task<T> DeserializeAsync<T>(string serializedValue, CancellationToken cancellationToken = default)
        where T : class
    {
        if (string.IsNullOrWhiteSpace(serializedValue)) throw new ArgumentException($"{nameof(serializedValue)} value cannot be null or empty", nameof(serializedValue));

        XmlSerializer xmlSerializer = new(typeof(T));
        using StringReader stringReader = new(serializedValue);

        T? deserializedValue = await Task.Run(() => (T?)xmlSerializer.Deserialize(stringReader), cancellationToken);
        if (deserializedValue is null) throw new InvalidOperationException($"Deserialization of value '{serializedValue}' failed");

        return deserializedValue;
    }
}
