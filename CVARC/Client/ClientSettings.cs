using CVARC.Network;

namespace ClientBase
{
    public class ClientSettings
    {
        public string Ip { get { return "127.0.0.1"; }}
        public int Port { get { return 14000; } }
        public Side Side { get; set; }

        /// <summary>
        /// ��������� ����� ����� ��� ��������� �����. -1 - ��� ��������� �����.
        /// </summary>
        public int MapSeed { get; set; }
        
        /// <summary>
        /// ���������. None - ���� ��� ����������.
        /// </summary>
        public Bots BotName { get; set; }

        public string LevelName { get; set; }
    }
}