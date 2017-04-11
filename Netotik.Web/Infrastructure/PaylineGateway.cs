using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.Text;
using System.IO;

namespace Netotik.Web.Infrastructure
{
    public class PaylineGateway
    {
        public PaylineGateway()
        {
            //
            // TODO: Add constructor logic here
            //
        }



        public string Send(string url, string api, decimal amount, string redirect)
        {
            string URI = url;
            WebRequest webRequest = WebRequest.Create(URI);

            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";

            string parameters = "api=" + api + "&amount=" + amount + "&redirect=" + redirect;
            byte[] bytes = Encoding.UTF8.GetBytes(parameters);

            webRequest.ContentLength = bytes.Length;
            Stream dataStream = webRequest.GetRequestStream();
            dataStream.Write(bytes, 0, bytes.Length);
            dataStream.Close();


            WebResponse webResponse = webRequest.GetResponse();
            dataStream = webResponse.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFields = reader.ReadToEnd();
            reader.Close();

            string result = "-1";

            try
            {
                result = responseFields.ToString();

            }
            catch
            {
                return "-1";
            }


            return result;
        }


        public string Get(string url, string api, string trans_id, string id_get)
        {
            string URI = url;
            WebRequest webRequest = WebRequest.Create(URI);

            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";

            string parameters = "api=" + api + "&trans_id=" + trans_id + "&id_get=" + id_get;
            byte[] bytes = Encoding.UTF8.GetBytes(parameters);

            webRequest.ContentLength = bytes.Length;
            Stream dataStream = webRequest.GetRequestStream();
            dataStream.Write(bytes, 0, bytes.Length);
            dataStream.Close();


            WebResponse webResponse = webRequest.GetResponse();
            dataStream = webResponse.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFields = reader.ReadToEnd();
            reader.Close();


            string result = "-1";

            try
            {
                result = responseFields.ToString();

            }
            catch
            {
                return "-1";
            }


            return result;
        }


        public string GetErrorByResultId(string id)
        {
            switch (id)
            {
                case "-1":
                    return "Api درگاه پی لاین نامعتبر است.";
                case "-2":
                    return "رقم پرداختی کمتر از حد استاندارد است.";
                case "-3":
                    return "صفحه نهایی نامعتبر است.";
                case "-4":
                    return "درگاه پرداخت نامعتبر است";
                default: return "";
            }
        }

    }
}