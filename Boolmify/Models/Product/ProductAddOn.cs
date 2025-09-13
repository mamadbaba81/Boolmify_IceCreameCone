    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace Boolmify.Models;

    public class ProductAddOn
    {
        public int ProductAddOnId { get; set; }
        [Required]
        [MaxLength(100)]
        public string ProductAddOnsName { get; set; }= default!;
        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        public virtual List<OrderItemAddOn>  OrderItemsAddOns { get; set; }  = new();

        public virtual List<CartItemAddOn> CartItemAddOns { get; set; } = new();
        
        public virtual List<ProductAddOnMap> ProductAddOnMaps { get; set; } = new();



    }