namespace EMS.Core.TicketMgmt
{
    public class Ticket : BaseEntity
    {
        // Properties
        public long Id { get; }
        public string Code { get; set; } = string.Empty;
        public decimal BasePrice { get; }
       

        private Ticket() { }

        public static Ticket Create()
        {
            return new Ticket();
        }
    }


    
    //entity
    //dto
    //database

    //ticket entity
        //ticket in persitance
}
