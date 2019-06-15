namespace MailClient
{
    public static class ServerInfo
    {
        public static class Gmail
        {
            public const string ImapServer = "imap.gmail.com";
            public const int ImapPort = 993;
            public const string SmtpServer = "smtp.gmail.com";
            public const int SmtpPort = 465;
        }

        public static class Yandex
        {
            public const string ImapServer = "imap.yandex.com";
            public const int ImapPort = 993;
            public const string SmtpServer = "smtp.yandex.com";
            public const int SmtpPort = 465;
        }
    }
}