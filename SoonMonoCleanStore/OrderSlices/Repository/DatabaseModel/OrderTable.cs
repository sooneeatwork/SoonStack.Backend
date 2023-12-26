namespace OrderSlices.Repository.DatabaseModel
{
    public class OrderTable
    {
        public const string TableName = "Orders";
        public int OrderId { get; set; }
        public int CustomerId { get; private set; }
        public DateTime OrderDate { get; set; }

    }
}
