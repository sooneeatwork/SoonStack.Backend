
using EMS.Shared.Persistance;
using System.Data;
using Dapper;
using SqlKata;
using EMS.Core.TicketMgmt;
using EMS.UseCases.TicketMgmt.Application.TicketModule.RepositoryInterfaces;

namespace EMS.Repository.TicketData
{
    public class TicketPurchaseHistoryRepository : BaseRepository, ITicketPurchaseHistoryRepository
    {
        public TicketPurchaseHistoryRepository(IDbConnection dbConnection) : base(dbConnection) { }

        public async Task<int> GetCustomerPurchasedTicketCount(long customerId)
        {
            var countQuery = new Query(nameof(TicketPurchaseHistory))
                                    .Where(nameof(customerId), customerId)
                                    .AsCount();

            var compiledQuery = _compiler.Compile(countQuery);
            var result = await _dbConnection.QueryFirstOrDefaultAsync<int>(compiledQuery.Sql, compiledQuery.NamedBindings);

            return result;
        }


    }
}
