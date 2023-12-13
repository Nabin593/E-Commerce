using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Entities
{
    public class BaseEnitity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Mark the property as identity
        public int Id { get; set; }
    }
}
