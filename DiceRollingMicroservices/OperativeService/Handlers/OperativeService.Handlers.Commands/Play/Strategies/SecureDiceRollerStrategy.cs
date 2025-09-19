namespace OperativeService.Handlers.Commands.Play.Strategies
{
    using System.Security.Cryptography;
    
    using OperativeService.Data.Models;

    public class SecureDiceRollerStrategy : IDiceRollerStrategy
    {
        public int Roll(DieType dieType) => RandomNumberGenerator.GetInt32(1, (int)dieType + 1);
    }
}
