public sealed class Board
{
    private readonly Dictionary<ulong, ulong> squareMovement = [];
    private ulong playerPos = 1;

    public ulong EndPosition { get; private set; }

    public Board(uint width, uint height, IEnumerable<Snake> snakes, IEnumerable<Ladder> ladders)
    {
        EndPosition = width * height;

        for (ulong i = 1; i <= EndPosition; i++)
        {
            squareMovement[i] = i;
        }
        _ = snakes.Select(x => squareMovement[x.From] = x.To);
        _ = ladders.Select(x => squareMovement[x.From] = x.To);
    }

    internal ulong GetPlayerPosition()
    {
        return playerPos;
    }

    internal void MovePlayerForward(ulong diceRollResult)
    {
        var newPosition = Math.Min(playerPos + diceRollResult, EndPosition);
        playerPos = squareMovement[newPosition];
    }

    internal void Reset()
    {
        playerPos = 1;
    }
}