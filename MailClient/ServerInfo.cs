namespace MailClient
{
    public class ServerInfo
    {
        public string ImapServer { get; set; }
        public int ImapPort { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string LoginSuffix { get; set; }

        public ServerInfo(ServerPreset preset)
        {
            if (preset == ServerPreset.Gmail)
            {
                ImapServer = "imap.gmail.com";
                ImapPort = 993;
                SmtpServer = "smtp.gmail.com";
                SmtpPort = 465;
                LoginSuffix = "@gmail.com";
            }
            else
            {
                ImapServer = "imap.yandex.com";
                ImapPort = 993;
                SmtpServer = "smtp.yandex.com";
                SmtpPort = 465;
                LoginSuffix = "@yandex.com";
            }

            Preset = preset;
        }

        public ServerInfo(string imapServer, int imapPort, string smtpServer, int smtpPort)
        {
            ImapServer = imapServer;
            ImapPort = imapPort;
            SmtpServer = smtpServer;
            SmtpPort = smtpPort;
            Preset = ServerPreset.Custom;
            LoginSuffix = "@" + imapServer.Substring(imapServer.IndexOf(".") + 1);
        }
        public ServerInfo(string imapServer, int imapPort)
        {
            ImapServer = imapServer;
            ImapPort = imapPort;
            Preset = ServerPreset.Custom;
            LoginSuffix = "@" + imapServer.Substring(imapServer.IndexOf(".") + 1);
        }
        public ServerPreset Preset { get; set; }
        public enum ServerPreset { Gmail, Yandex, Custom };
    }
}