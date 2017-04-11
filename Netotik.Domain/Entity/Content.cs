using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Netotik.Domain.Entity
{
    public partial class Content
    {
        public Content()
        {
            //this.ContentCategories = new List<ContentCategory>();
            this.ContentComments = new List<ContentComment>();
            this.ImageGalleryItems = new List<ImageGalleryItem>();
            this.ContentTages = new List<ContentTag>();
            this.FilesAttach = new List<File>();
        }

        public int Id { get; set; }
        public int? SubjectId { get; set; }
        [DisplayName("عنوان")]
        public string Title { get; set; }
        [DisplayName("url")]
        public string UrlName { get; set; }
        [DisplayName("محتوا")]
        public string Body { get; set; }
        [DisplayName("توضیح کوتاه")]
        public string BodyOverview { get; set; }
        [DisplayName("مجوز ارسال نظر")]
        public bool AllowComments { get; set; }
        [DisplayName("نمایش نظرات")]
        public bool AllowViewComments { get; set; }
        [DisplayName("تعداد نظرات")]
        public int CommentCount { get; set; }
        [DisplayName("تعداد نمایش")]
        public int CountView { get; set; }
        [DisplayName("شروع انتشار")]
        public Nullable<System.DateTime> StartDate { get; set; }
        [DisplayName("پایان انتشار")]
        public Nullable<System.DateTime> EndDate { get; set; }
        [DisplayName("کلمات متا")]
        public string MetaKeywords { get; set; }
        [DisplayName("توضیحات متا")]
        public string MetaDescription { get; set; }
        [DisplayName("عنوان متا")]
        public string MetaTitle { get; set; }
        [DisplayName("تاریخ ایجاد")]
        public System.DateTime CreateDate { get; set; }
        [DisplayName("تاریخ اخرین ویرایش")]
        public System.DateTime EditDate { get; set; }
        public long CreatedUserId { get; set; }
        public long EditedUserId { get; set; }
        [DisplayName("وضعیت")]
        public ContentStatus status { get; set; }
        [DisplayName("نظر مدیر")]
        public string AdminComment { get; set; }
        public Nullable<int> PictureId { get; set; }
        
        public virtual Picture Picture { get; set; }
        public virtual User UserCreated { get; set; }
        public virtual User UserEdited { get; set; }
        public virtual ICollection<ContentComment> ContentComments { get; set; }
        public virtual ICollection<ContentCategory> ContentCategories { get; set; }
        public virtual ICollection<ImageGalleryItem> ImageGalleryItems { get; set; }
        public virtual ICollection<ContentTag> ContentTages { get; set; }
        public virtual ICollection<File> FilesAttach { get; set; }
    }
    public enum ContentStatus : short
    {
        Accepted = 0,
        WaitForAccept = 1,
        NotAccepted = 2,
        Delete = 3,
    }
}
