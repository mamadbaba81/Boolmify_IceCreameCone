    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace Boolmify.Models;

    public class OrderItem
    {
        public int  OrderItemId { get; set; }

        public int  OrderId { get; set; }

        public Order Order { get; set; }
        
        public int ProductId { get; set; }

        public Product Product { get; set; }
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.0, double.MaxValue)]
        public decimal  UnitPrice { get; set; }
        
        public List<OrderItemsAddOn> OrderItemsAddOns { get; set; } = new();

        
    }