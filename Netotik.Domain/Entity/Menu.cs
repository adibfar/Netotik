using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class Menu
    {
        public Menu()
        {
            this.SubMenues = new List<Menu>();
        }

        public int Id { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
        public virtual ICollection<Menu> SubMenues { get; set; }
        public virtual Menu Parent { get; set; }
    }
}
