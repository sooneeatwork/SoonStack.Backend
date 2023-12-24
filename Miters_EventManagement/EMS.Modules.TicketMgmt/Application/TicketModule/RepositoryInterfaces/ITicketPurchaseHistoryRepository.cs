namespace EMS.UseCases.TicketMgmt.Application.TicketModule.RepositoryInterfaces
{
    public interface ITicketPurchaseHistoryRepository
    {
        Task<int> GetCustomerPurchasedTicketCount(long customerId);
    }
}