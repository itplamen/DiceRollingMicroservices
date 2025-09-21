namespace OperativeService.Handlers.Commands.Play.Strategies.RollDice
{
    using OperativeService.Data.Models;

    public interface IDiceRollerStrategy
    {
        int Roll(DieType dieType);
    }
}
