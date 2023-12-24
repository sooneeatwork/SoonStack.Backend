using EMS.UseCases.TicketMgmt.Application.TicketModule.RepositoryInterfaces;
using EMS.Shared.Persistance;
using System.Data;


namespace EMS.Repository.TicketData
{
    public class TicketRepository : BaseRepository, ITicketRepository
    {
        public TicketRepository(IDbConnection dbConnection) : base(dbConnection) { }
    }
}
