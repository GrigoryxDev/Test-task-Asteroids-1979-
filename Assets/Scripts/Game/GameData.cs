namespace Scripts.Game
{
    /// <summary>
    /// Class that stores game data
    /// </summary>
    public class GameData
    {
        public GameData(int waveNumber, int score)
        {
            WaveNumber = waveNumber;
            Score = score;
        }

        public int WaveNumber { get; set; }
        public int Score { get; set; }


    }
}