    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace Boolmify.Models;

    public class OrderItem
    {
        public int  OrderItemId { get; set; }

        public int  OrderId { get; set; }

        public Order Order { get; set; } = default!;
        
        public int ProductId { get; set; }

        public Product Product { get; set; } = default!;
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.0, double.MaxValue)] 
        public decimal  UnitPrice { get; set; }
        
        [NotMapped]
        public decimal  SubTotal => UnitPrice * Quantity;
        
        public virtual List<OrderItemAddOn> OrderItemsAddOns { get; set; } = new();

        
    }