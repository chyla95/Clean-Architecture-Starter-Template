using Sample.Architecture.Extensions.Application.Mailing.Enums;

namespace Sample.Architecture.Extensions.Application.Mailing.Models;
public sealed record MailBodyModel(
    string Content, 
    MailBodyType MailBodyType
);
