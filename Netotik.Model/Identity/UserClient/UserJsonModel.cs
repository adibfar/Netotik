using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Netotik.ViewModels.Identity.UserClient
{
    public class UserJsonModel
    {
        public string Birthday { get; set; }
        public string NationalCode { get; set; }
        public string IsMale { get; set; }
        public string CreateDate { get; set; }
        public string MarriageDate { get; set; }
        public string Age { get; set; }

    }
}
