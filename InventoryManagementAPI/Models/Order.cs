namespace InventoryManagementAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public int UserId { get; set; }
    }
}
