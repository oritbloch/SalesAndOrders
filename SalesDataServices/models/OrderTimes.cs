namespace SalesDataServices
{
    public class OrderTimes
    {
        public long OrderId { get; set; }
        public string OrderDate { get; set; }

        public string RequiredDate { get; set; }

        public string ShippedDate { get; set; }

        public int NumOfDiffDays { get; set; }
    }
}
