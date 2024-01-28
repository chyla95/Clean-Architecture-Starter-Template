namespace Sample.Architecture.Extensions.Application.Mailing.Models;
public sealed record MailAttachmentModel(
    Stream FileStream, 
    string Name
);