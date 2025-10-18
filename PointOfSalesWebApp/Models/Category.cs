using System.ComponentModel.DataAnnotations;

namespace PointOfSalesWebApp.Models
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
        public string? Description { get; set; }
        
            
        public bool Active { get; set; } = true;
    }
}
