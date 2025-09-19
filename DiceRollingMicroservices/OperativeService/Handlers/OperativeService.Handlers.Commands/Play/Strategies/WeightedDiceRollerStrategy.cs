namespace OperativeService.Handlers.Commands.Play.Strategies
{
    using OperativeService.Data.Models;

    // Add weights for cheating
    public class WeightedDiceRollerStrategy : IDiceRollerStrategy
    {
        public int Roll(DieType dieType)
        {
            int sides = (int)dieType;
            int[] weights = new int[sides];

            for (int i = 0; i < sides; i++)
            {
                weights[i] = 1;
            }

            int weight = AddWeightToNumber(weights, sides);
            int roll = Random.Shared.Next(1, weight + 1);
            int cumulative = 0;

            for (int i = 0; i < sides; i++)
            {
                cumulative += weights[i];

                if (roll <= cumulative)
                {
                    return i + 1;
                }
            }

            return sides;
        }

        private int AddWeightToNumber(int[] weights, int sides)
        {
            int weightOnLastThreeNumbers = 3;

            for (int i = sides - weightOnLastThreeNumbers; i < sides; i++)
            {
                if (i >= 0)
                {
                    weights[i] += weightOnLastThreeNumbers;
                }
            }

            return weights.Sum();
        }
    }
}
