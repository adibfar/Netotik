using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Netotik.Web.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult sms(string from,string to,string message)
        {
            using (StreamWriter _testData = new StreamWriter(HostingEnvironment.MapPath("~/smsNew.txt"), true))
            {
                _testData.WriteLine(from + to + message); // Write the file.
            }
            return Content("Ok");
        }
    }
}