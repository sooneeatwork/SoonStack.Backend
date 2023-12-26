namespace OrderSlices.Repository.Repository.RepoInterfaces
{
    public interface IOrderRepository: IGenericRepository
    {
        Task<IEnumerable<OrderDetailsView>> GetOrderDetailsByIdAsync(long orderId);
    }
}
