using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class Slider
    {
        public int Id { get; set; }
        public string BigText { get; set; }
        public string SmallText { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
        public virtual Picture Picture { get; set; }
        public int PictureId { get; set; }
    }
}
