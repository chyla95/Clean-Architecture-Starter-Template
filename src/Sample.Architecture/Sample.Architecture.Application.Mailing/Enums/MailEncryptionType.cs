namespace Sample.Architecture.Application.Mailing.Enums;
public enum MailEncryptionType
{
    /// <summary>
    /// No connection encryption.
    /// </summary>
    None,

    /// <summary>
    /// If a server is compatible and no errors occur, the secured TLS or SSL connection will be established.
    /// If anything fails in the process, a plain-text transmission will be established.
    /// </summary>
    OpportunisticTls,

    /// <summary>
    /// If succeeded, a secure connection will be set up.
    /// If a server is not compatible or a connection times out, a transmission will be abandoned.
    /// </summary>
    ForcedTls
}
