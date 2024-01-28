using Sample.Architecture.Extensions.Application.Common.Abstractions.Utilities;
using System.Text;

namespace Sample.Architecture.Extensions.Infrastructure.Common.Utilities;
internal sealed class FileUtility : IFileUtility
{
    public string ReadAllText(string filePath)
    {
        return ReadAllText(filePath, Encoding.Default);
    }

    public string ReadAllText(string filePath, Encoding? encoding)
    {
        encoding ??= Encoding.Default;
        return File.ReadAllText(filePath, encoding);
    }

    public async Task<string> ReadAllTextAsync(string filePath, CancellationToken cancellationToken = default)
    {
        return await ReadAllTextAsync(filePath, Encoding.Default, cancellationToken);
    }

    public async Task<string> ReadAllTextAsync(string filePath, Encoding? encoding, CancellationToken cancellationToken = default)
    {
        encoding ??= Encoding.Default;
        return await File.ReadAllTextAsync(filePath, encoding, cancellationToken);
    }
}