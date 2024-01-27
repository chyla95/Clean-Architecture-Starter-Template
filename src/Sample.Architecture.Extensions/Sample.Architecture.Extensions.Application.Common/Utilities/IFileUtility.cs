using System.Text;

namespace Sample.Architecture.Extensions.Application.Common.Abstractions.Utilities;
public interface IFileUtility
{
    string ReadAllText(string filePath);
    string ReadAllText(string filePath, Encoding? encoding);

    Task<string> ReadAllTextAsync(string filePath, CancellationToken cancellationToken = default);
    Task<string> ReadAllTextAsync(string filePath, Encoding encoding, CancellationToken cancellationToken = default);
}