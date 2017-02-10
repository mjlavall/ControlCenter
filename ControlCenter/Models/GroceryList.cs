using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlCenter.Models
{
    public class GroceryList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public virtual ICollection<GroceryItem> Items { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
    }

    public class GroceryItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public virtual GroceryUnit Unit { get; set; }
        public bool Complete { get; set; }
    }

    public class GroceryUnit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
