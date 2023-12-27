using System.Text;

namespace Sample.Architecture.Application.Utilities;
internal interface IFileUtility
{
    string ReadAllText(string filePath);
    string ReadAllText(string filePath, Encoding? encoding);

    Task<string> ReadAllTextAsync(string filePath, CancellationToken cancellationToken = default);
    Task<string> ReadAllTextAsync(string filePath, Encoding encoding, CancellationToken cancellationToken = default);
}