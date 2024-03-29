﻿namespace Sample.Architecture.Extensions.Application.Mailing.Models;
public sealed record MailMessageModel
{
    public string? Subject { get; set; }
    public MailBodyModel? Body { get; set; }
    public ICollection<MailAttachmentModel> Attachments { get; set; } = [];

    public ICollection<MailAddressModel> Senders { get; init; } = [];
    public ICollection<MailAddressModel> Recipients { get; set; } = [];
    public ICollection<MailAddressModel> BccRecipients { get; set; } = [];
    public ICollection<MailAddressModel> CcRecipients { get; set; } = [];
}