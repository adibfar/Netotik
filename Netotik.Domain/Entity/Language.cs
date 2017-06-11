using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Domain.Entity
{
    public class Language
    {
        public Language()
        {
            this.LocaleStringResources = new List<LocaleStringResource>();
            this.ContentCategories = new List<ContentCategory>();
            this.Contents = new List<Content>();
            this.ContentTags = new List<ContentTag>();
            this.Sliders = new List<Slider>();
            this.Menus = new List<Menu>();
            this.Sections = new List<IndexSection>();
            this.LanguageTranslationes = new List<LanguageTranslation>();
        }
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the language culture
        /// </summary>
        public string LanguageCulture { get; set; }

        /// <summary>
        /// Gets or sets the unique SEO code
        /// </summary>
        public string UniqueSeoCode { get; set; }

        /// <summary>
        /// Gets or sets the flag image file name
        /// </summary>
        public string FlagImageFileName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the language supports "Right-to-left"
        /// </summary>
        public bool Rtl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the language is published
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the language is Default
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }
        public virtual ICollection<LocaleStringResource> LocaleStringResources { get; set; }
        public virtual ICollection<ContentCategory> ContentCategories { get; set; }
        public virtual ICollection<Content> Contents { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
        public virtual ICollection<Slider> Sliders { get; set; }
        public virtual ICollection<LanguageTranslation> LanguageTranslationes { get; set; }
        public virtual ICollection<IndexSection> Sections { get; set; }
        public virtual ICollection<ContentTag> ContentTags { get; set; }
    }
}
