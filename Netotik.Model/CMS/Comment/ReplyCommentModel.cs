﻿using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.ViewModels.CMS.Comment
{
    public class ReplyCommentModel
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        [Display(Name = "پیــام")]
        public string CommentText { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Name")]
        public string CommentName { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Email")]
        public string CommentEmail { get; set; }



        [Display(Name = "پیــام")]
        [Required(ErrorMessage = "لطفا پیام را وارد کنید.")]
        public string Text { get; set; }

        [Required(ErrorMessage = "نام خود را وارد کنید")]
        [MaxLength(100, ErrorMessage = "حدااکثر 100 کاراکتر")]
        [MinLength(3, ErrorMessage = "حداقل 3 کاراکتر")]
        [Display(ResourceType = typeof(Captions), Name = "Name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "ایمیل خود را وارد کنید")]
        [MaxLength(300, ErrorMessage = "حداکثر 300 کاراکتر")]
        [Display(ResourceType = typeof(Captions), Name = "Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                    ErrorMessage = "ایمیل نامعتبر است")]
        public string Email { get; set; }

    }
}