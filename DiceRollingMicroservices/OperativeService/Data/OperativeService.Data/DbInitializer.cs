namespace OperativeService.Data
{
    using MongoDB.Driver;
    
    using OperativeService.Data.Models;

    public sealed class DbInitializer
    {
        private readonly OperativeServiceDbContext dbContext;

        public DbInitializer(OperativeServiceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Init()
        {
            IMongoCollection<Die> dice = InitCollection(Builders<Die>.IndexKeys.Ascending(x => x.Name), new CreateIndexOptions { Unique = true });
            IEnumerable<Die> diceSeed = GetDiceSeed();
            Seed(dice, diceSeed);

            InitCollection<DieRoll>();
            InitCollection(Builders<Game>.IndexKeys.Ascending(x => x.Name));
            InitCollection(Builders<Round>.IndexKeys.Ascending(x => x.GameId).Ascending(x => x.RoundNumber));
            InitCollection(Builders<RollResult>.IndexKeys.Ascending(x => x.UserId).Ascending(x => x.CreatedOn));
            InitCollection(Builders<User>.IndexKeys.Ascending(x => x.ExternalId), new CreateIndexOptions { Unique = true }); 
        }

        private IMongoCollection<TEntity> InitCollection<TEntity>(IndexKeysDefinition<TEntity> keys = null, CreateIndexOptions options = null)
            where TEntity : BaseModel
        {
            IMongoCollection<TEntity> collection = dbContext.GetCollection<TEntity>();

            if (keys != null)
            {
                CreateIndexModel<TEntity> indexModel = new CreateIndexModel<TEntity>(keys, options);
                collection.Indexes.CreateOne(indexModel);
            }
            
            return collection;
        }

        private void Seed<TEntity>(IMongoCollection<TEntity> collection, IEnumerable<TEntity> data)
            where TEntity : BaseModel
        {
            if (!collection.Find(x => true).Any())
            {
                collection.InsertMany(data);
            }
        }

        private IEnumerable<Die> GetDiceSeed()
        {
            var dice = new List<Die>()
            {
                new Die()
                {
                    Name = "Tetrahedron",
                    DieType = DieType.D4
                },
                new Die()
                {
                    Name = "Cube (standard six - sided die)",
                    DieType = DieType.D6
                },
                new Die()
                {
                    Name = "Octahedron",
                    DieType = DieType.D8
                },
                new Die()
                {
                    Name = "Pentagonal trapezohedron",
                    DieType = DieType.D10
                },
                new Die()
                {
                    Name = "Dodecahedron",
                    DieType = DieType.D12
                },
                new Die()
                {
                    Name = "Icosahedron",
                    DieType = DieType.D20
                }
            };

            dice.ForEach(d => d.CreatedOn = DateTime.UtcNow);

            return dice;
        }
    }
}
