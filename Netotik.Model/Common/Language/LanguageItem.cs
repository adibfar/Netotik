using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Netotik.ViewModels.Common.Language
{
    public class LanguageItem
    {
        public int Id { get; set; }
        public int RowNumber { get; set; }
        public string Name { get; set; }
        public string LanguageCulture { get; set; }
        public string UniqueSeoCode { get; set; }
        public string FlagImageFileName { get; set; }
        public bool Rtl { get; set; }
        public bool Published { get; set; }
        public bool IsDefault { get; set; }
        public int DisplayOrder { get; set; }
    }
}
