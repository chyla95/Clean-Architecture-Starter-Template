namespace Sample.Architecture.Application.Mailing.Models;
public sealed record MailAttachmentModel(Stream FileStream, string Name);