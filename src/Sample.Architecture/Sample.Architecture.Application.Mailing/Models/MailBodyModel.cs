using Sample.Architecture.Application.Mailing.Enums;

namespace Sample.Architecture.Application.Mailing.Models;
public sealed record MailBodyModel(
    string Content, 
    MailBodyType MailBodyType
);
