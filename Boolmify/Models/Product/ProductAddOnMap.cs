    namespace Boolmify.Models;

    public class ProductAddOnMap
    {
        
        public int  ProductId { get; set; }

        public virtual Product  Product { get; set; } = default!;
        
        
        public int ProductAddOnId { get; set; }
        
        public virtual ProductAddOn  ProductAddOn { get; set; } =default!;
        
        
        
    }