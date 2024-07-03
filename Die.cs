public sealed class Die(byte maxRoll)
{
    public byte Roll()
    {
        return (byte)((Random.Shared.Next() % maxRoll) + 1);
    }
}