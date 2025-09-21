namespace OperativeService.Handlers.Commands.Play.Strategies.RollDice
{
    using OperativeService.Data.Models;

    public class SimpleDiceRollerStrategy
    {
        public int Roll(DieType dieType) => Random.Shared.Next(1, (int)dieType + 1);
    }
}
