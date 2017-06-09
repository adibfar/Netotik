using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Netotik.Domain.Entity
{
    public partial class Content
    {
        public Content()
        {
            this.ContentCategories = new List<ContentCategory>();
            this.ContentComments = new List<ContentComment>();
            this.ImageGalleryItems = new List<ImageGalleryItem>();
            this.ContentTages = new List<ContentTag>();
            this.FilesAttach = new List<File>();
        }

        public int Id { get; set; }
        public int? SubjectId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Body { get; set; }
        public string BodyOverview { get; set; }
        public bool AllowComments { get; set; }
        public bool AllowViewComments { get; set; }
        public int CommentCount { get; set; }
        public int CountView { get; set; }
        public bool HasSideBar { get; set; }
        public bool DontShowBlog { get; set; }
        public bool DontShowImageDetail { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime EditDate { get; set; }
        public long CreatedUserId { get; set; }
        public long EditedUserId { get; set; }
        public ContentStatus status { get; set; }
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
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
        Deleted = 3,
    }
}
