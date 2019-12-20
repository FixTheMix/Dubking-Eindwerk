using DubKing.Repositories.Contracts;
using System.Configuration;

namespace DubKing.Repositories
{
    public class ConnectionStringReader : IConnectionStringReader
    {
        public string GetConnectionString()
        {
            return ConfigurationManager.AppSettings["ConnectionString"];
        }
    }
}
