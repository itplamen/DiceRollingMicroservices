namespace OperativeService.Handlers.Commands.Play.Strategies
{
    using OperativeService.Data.Models;

    public interface IDiceRollerStrategy
    {
        int Roll(DieType dieType);
    }
}
