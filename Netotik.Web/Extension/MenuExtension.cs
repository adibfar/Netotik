using Netotik.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace Netotik.Web.Extension
{
    public static class MenuExtension
    {
        public static string GetName(Menu menu, string text)
        {
            return (menu != null) ? MenuExtension.GetName(menu.Parent, menu.Text + " >> " + text) : text;
        }
    }
}