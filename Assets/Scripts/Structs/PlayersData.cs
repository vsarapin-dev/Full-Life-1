namespace Structs
{
    public struct PlayersData
    {
        public int NetworkId;
        public string PlayerName;
        public bool IsReady;

        public PlayersData(int networkId, string playerName, bool isReady)
        {
            NetworkId = networkId;
            PlayerName = playerName;
            IsReady = isReady;
        }
    }
}