namespace OperativeService.Data
{
    using Microsoft.Extensions.Configuration;
    using MongoDB.Driver;
    
    using OperativeService.Data.Models;

    public class OperativeServiceDbContext
    {
        private readonly IMongoDatabase database;

        public OperativeServiceDbContext(IConfiguration configuration)
        {
            string dbHost = configuration["MongoDb:Host"];
            string dbName = configuration["MongoDb:DatabaseName"];
            database = GetDataBase(dbHost, dbName);
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>() 
            where TEntity : BaseModel
        {
            return database.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        private IMongoDatabase GetDataBase(string host, string dbName)
        {
            IMongoClient mongoClient = new MongoClient(host);
            IMongoDatabase database = mongoClient.GetDatabase(dbName);

            return database;
        }
    }
}
