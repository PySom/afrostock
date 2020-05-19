using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using MimeKit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AfrroStock.Services
{
	// This class is used by the application to send email for account confirmation and password reset.
	// For more details see https://go.microsoft.com/fwlink/?LinkID=532713
	public class EmailSender : IEmailSender
	{
		private readonly ILogger<EmailSender> _logger;
		static readonly string senderEmail = "cnwisu@infomall.ng";
		static readonly string senderName = "Afro Stock Studios";
		static readonly string password = "Philomena01";
		static readonly string smtp = "infomall.ng";
		static readonly int port = 25;

		public EmailSender(ILogger<EmailSender> logger)
		{
			_logger = logger;
		}
		public async Task SendEmailAsync(string email, string subject, string message)
		{
			var mimeMessage = new MimeMessage();
			mimeMessage.From.Add(new MailboxAddress(senderName, senderEmail));
			mimeMessage.Subject = !string.IsNullOrEmpty(subject) ? subject : "AfroStock";
			mimeMessage.To.Add(new MailboxAddress(email));
            BodyBuilder builder = new BodyBuilder
            {
                HtmlBody = message
            };
            mimeMessage.Body = builder.ToMessageBody() ?? new TextPart("plain");

			using var client = new SmtpClient();
			client.Connect(smtp, port, SecureSocketOptions.None);
			client.AuthenticationMechanisms.Remove("XOAUTH2");
			client.Authenticate(senderEmail, password);
			await client.SendAsync(mimeMessage);
			_logger.LogInformation("message sent successfully...");
			await client.DisconnectAsync(true);

		}

		public async Task SendEmailToAllAsync(IEnumerable<string> emails, string subject, string message)
		{
			var mimeMessage = new MimeMessage();
			mimeMessage.From.Add(new MailboxAddress(senderName, senderEmail));
			foreach (string email in emails)
			{
				mimeMessage.Bcc.Add(new MailboxAddress(email));
			}
			mimeMessage.Subject = !string.IsNullOrEmpty(subject) ? subject : "AfroStock";
            BodyBuilder builder = new BodyBuilder
            {
                HtmlBody = message
            };
            mimeMessage.Body = builder.ToMessageBody() ?? new TextPart("plain");

			using var client = new SmtpClient();
			client.Connect(smtp, port, SecureSocketOptions.None);
			client.AuthenticationMechanisms.Remove("XOAUTH2");
			client.Authenticate(senderEmail, password);
			await client.SendAsync(mimeMessage);
			_logger.LogInformation("message sent successfully...");
			await client.DisconnectAsync(true);
		}
	}
}
