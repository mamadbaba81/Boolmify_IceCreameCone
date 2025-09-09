    namespace Boolmify.Models;

    public class ProductAddOnsMap
    {
        
        public int  ProductId { get; set; }

        public Product  Product { get; set; }
        
        
        public int ProductAddOnId { get; set; }
        
        public ProductAddOn  ProductAddOn { get; set; }
        
        
        
    }