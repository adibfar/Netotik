using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace Netotik.Web.Extension
{
    public static class ExceptionExtension
    {
        public static string GetDetailException(this DbEntityValidationException ex)
        {
            String message = "";
            foreach (var eve in ex.EntityValidationErrors)
            {
                message = String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                foreach (var ve in eve.ValidationErrors)
                {
                    message += String.Format("- Property: \"{0}\", Error: \"{1}\"",
                        ve.PropertyName, ve.ErrorMessage);
                }
            }
            return message;
        }
    }
}