IEnumerable<Die> dice = [new Die(6)];
var board = new Board(10, 10, [], []);

var supervisor = new Supervisor(board, dice, 0.00000005);

var results = supervisor.GetResults();

foreach (var result in results)
{
    Console.WriteLine($"Roll: {result.Key} Quantity: {result.Value}");
}