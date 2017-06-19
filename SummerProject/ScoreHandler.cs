namespace SummerProject
{
    public static class ScoreHandler
    {
        public static int Score { get; set; }
        public static int HighScore { get; private set; }

        public static void AddScore(int worthScore)
        {
            Score += worthScore;
        }
        private static void UpdateHighScore()
        {
            if (Score > HighScore)
                HighScore = Score;
        }
        public static void Reset()
        {
            UpdateHighScore();
            Score = 0;
        }
    }
}
