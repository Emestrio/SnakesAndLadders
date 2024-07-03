public sealed class Supervisor(Board board, IEnumerable<Die> dies, double targetMeanChange)
{
    private double LastMean = 1;
    private double CurrentMean = 100;
    private readonly Dictionary<int, int> rollDistribution = [];

    public IOrderedEnumerable<KeyValuePair<int, int>> GetResults()
    {
        var numTests = 1_000;
        var loops = 0;
        while (Math.Abs(CurrentMean - LastMean) > targetMeanChange)
        {
            loops++;
            for (int i = 0; i < numTests; i++)
            {
                var rolls = 0;
                while (board.GetPlayerPosition() != board.EndPosition)
                {
                    var diceRollResult = (ulong)dies.Select(x => (int)x.Roll()).Sum();
                    board.MovePlayerForward(diceRollResult);
                    rolls++;
                }
                board.Reset();

                rollDistribution[rolls] = rollDistribution.TryGetValue(rolls, out int value) ? value + 1 : 1;
            }
            LastMean = CurrentMean;
            CurrentMean = (double)rollDistribution.Select(x => x.Key * x.Value).Sum() / (numTests * loops);
        }

        return rollDistribution.OrderBy(x => x.Key);
    }
}