using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public MenuLocation MenuLocation { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public virtual ICollection<Menu> SubMenues { get; set; }
        public virtual Menu Parent { get; set; }
    }

    public enum MenuLocation : short
    {
        [Display(ResourceType =typeof(Captions),Name ="Header")]
        Header = 0,
        [Display(ResourceType = typeof(Captions), Name = "Footer")]
        Footer,
    }
}
