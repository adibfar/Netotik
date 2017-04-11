using BankMellatServices.MellatWebServiceTeset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankMellatServices
{
    public class BpmServices
    {
        #region Base Variable Definition

        readonly string _callBackUrl;
        readonly long _terminalId;
        readonly string _userName;
        readonly string _password;

        string localDate = string.Empty;
        string localTime = string.Empty;
        #endregion


        private BpmServices()
        {

        }
        public BpmServices(long terminalId, string userName, string password, string callBackUrl)
        {
            try
            {
                _terminalId = terminalId;
                _userName = userName;
                _password = password;
                _callBackUrl = callBackUrl;

                localDate = DateTime.Now.ToString("yyyyMMdd");
                localTime = DateTime.Now.ToString("HHMMSS");
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }



        public string bpPayRequest(long orderId, long priceAmount, string additionalText)
        {
            try
            {
                var WebService = new PaymentGatewayImplService();
                return WebService.bpPayRequest(_terminalId, _userName, _password, orderId, priceAmount, localDate, localTime,
                                 additionalText, _callBackUrl, 0);

            }
            catch (Exception error)
            {
                throw new Exception(error.Message); ;
            }
        }

        public string VerifyRequest(long orderId, long saleOrderId, long saleReferenceId)
        {
            try
            {
                var WebService = new MellatWebService.PaymentGatewayImplService();
                return WebService.bpVerifyRequest(_terminalId, _userName, _password, orderId, saleOrderId, saleReferenceId);

            }
            catch (Exception Error)
            {
                throw new Exception(Error.Message);
            }
        }

        public string InquiryRequest(long orderId, long saleOrderId, long saleReferenceId)
        {
            try
            {
                MellatWebService.PaymentGatewayImplService WebService = new MellatWebService.PaymentGatewayImplService();
                return WebService.bpInquiryRequest(_terminalId, _userName, _password, orderId, saleOrderId, saleReferenceId);

            }
            catch (Exception Error)
            {
                throw new Exception(Error.Message);
            }
        }


        public string SettleRequest(long orderId, long saleOrderId, long saleReferenceId)
        {
            try
            {
                var WebService = new PaymentGatewayImplService();
                return WebService.bpSettleRequest(_terminalId, _userName, _password, orderId, saleOrderId, saleReferenceId);
            }
            catch (Exception Error)
            {
                throw new Exception(Error.Message);
            }
        }

        public string bpReversalRequest(long orderId, long saleOrderId, long saleReferenceId)
        {
            try
            {

                var WebService = new PaymentGatewayImplService();
                return WebService.bpReversalRequest(_terminalId, _userName, _password, orderId, saleOrderId, saleReferenceId);

            }
            catch (Exception error)
            {
                throw new Exception(error.Message); ;
            }
        }




        public String DesribtionStatusCode(int statusCode)
        {
            switch (statusCode)
            {
                case 0:
                    return MellatBankReturnCode.ﺗﺮاﻛﻨﺶ_ﺑﺎ_ﻣﻮﻓﻘﻴﺖ_اﻧﺠﺎم_ﺷﺪ.ToString();
                case 11:
                    return MellatBankReturnCode.ﺷﻤﺎره_ﻛﺎرت_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ.ToString();
                case 12:
                    return MellatBankReturnCode.ﻣﻮﺟﻮدی_ﻛﺎﻓﻲ_ﻧﻴﺴﺖ.ToString();
                case 13:
                    return MellatBankReturnCode.رﻣﺰ_ﻧﺎدرﺳﺖ_اﺳﺖ.ToString();
                case 14:
                    return MellatBankReturnCode.ﺗﻌﺪاد_دﻓﻌﺎت_وارد_ﻛﺮدن_رﻣﺰ_ﺑﻴﺶ_از_ﺣﺪ_ﻣﺠﺎز_اﺳﺖ.ToString();
                case 15:
                    return MellatBankReturnCode.ﻛﺎرت_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ.ToString();
                case 16:
                    return MellatBankReturnCode.دﻓﻌﺎت_ﺑﺮداﺷﺖ_وﺟﻪ_ﺑﻴﺶ_از_ﺣﺪ_ﻣﺠﺎز_اﺳﺖ.ToString();
                case 17:
                    return MellatBankReturnCode.ﻛﺎرﺑﺮ_از_اﻧﺠﺎم_ﺗﺮاﻛﻨﺶ_ﻣﻨﺼﺮف_ﺷﺪه_اﺳﺖ.ToString();
                case 18:
                    return MellatBankReturnCode.ﺗﺎرﻳﺦ_اﻧﻘﻀﺎی_ﻛﺎرت_ﮔﺬﺷﺘﻪ_اﺳﺖ.ToString();
                case 19:
                    return MellatBankReturnCode.ﻣﺒﻠﻎ_ﺑﺮداﺷﺖ_وﺟﻪ_ﺑﻴﺶ_از_ﺣﺪ_ﻣﺠﺎز_اﺳﺖ.ToString();
                case 111:
                    return MellatBankReturnCode.ﺻﺎدر_ﻛﻨﻨﺪه_ﻛﺎرت_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ.ToString();
                case 112:
                    return MellatBankReturnCode.ﺧﻄﺎی_ﺳﻮﻳﻴﭻ_ﺻﺎدر_ﻛﻨﻨﺪه_ﻛﺎرت.ToString();
                case 113:
                    return MellatBankReturnCode.ﭘﺎﺳﺨﻲ_از_ﺻﺎدر_ﻛﻨﻨﺪه_ﻛﺎرت_درﻳﺎﻓﺖ_ﻧﺸﺪ.ToString();
                case 114:
                    return MellatBankReturnCode.دارﻧﺪه_ﻛﺎرت_ﻣﺠﺎز_ﺑﻪ_اﻧﺠﺎم_اﻳﻦ_ﺗﺮاﻛﻨﺶ_ﻧﻴﺴﺖ.ToString();
                case 21:
                    return MellatBankReturnCode.ﭘﺬﻳﺮﻧﺪه_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ.ToString();
                case 23:
                    return MellatBankReturnCode.ﺧﻄﺎی_اﻣﻨﻴﺘﻲ_رخ_داده_اﺳﺖ.ToString();
                case 24:
                    return MellatBankReturnCode.اﻃﻼﻋﺎت_ﻛﺎرﺑﺮی_ﭘﺬﻳﺮﻧﺪه_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ.ToString();
                case 25:
                    return MellatBankReturnCode.ﻣﺒﻠﻎ_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ.ToString();
                case 31:
                    return MellatBankReturnCode.ﭘﺎﺳﺦ_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ.ToString();
                case 32:
                    return MellatBankReturnCode.ﻓﺮﻣﺖ_اﻃﻼﻋﺎت_وارد_ﺷﺪه_ﺻﺤﻴﺢ_ﻧﻤﻲ_ﺑﺎﺷﺪ.ToString();
                case 33:
                    return MellatBankReturnCode.ﺣﺴﺎب_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ.ToString();
                case 34:
                    return MellatBankReturnCode.ﺧﻄﺎی_ﺳﻴﺴﺘﻤﻲ.ToString();
                case 35:
                    return MellatBankReturnCode.ﺗﺎرﻳﺦ_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ.ToString();
                case 41:
                    return MellatBankReturnCode.ﺷﻤﺎره_درﺧﻮاﺳﺖ_ﺗﻜﺮاری_اﺳﺖ.ToString();
                case 42:
                    return MellatBankReturnCode.ﺗﺮاﻛﻨﺶ_Sale_یافت_نشد_.ToString();
                case 43:
                    return MellatBankReturnCode.ﻗﺒﻼ_Verify_درﺧﻮاﺳﺖ_داده_ﺷﺪه_اﺳﺖ.ToString();



                case 44:
                    return MellatBankReturnCode.درخواست_verify_یافت_نشد.ToString();
                case 45:
                    return MellatBankReturnCode.ﺗﺮاﻛﻨﺶ_Settle_ﺷﺪه_اﺳﺖ.ToString();
                case 46:
                    return MellatBankReturnCode.ﺗﺮاﻛﻨﺶ_Settle_نشده_اﺳﺖ.ToString();

                case 47:
                    return MellatBankReturnCode.ﺗﺮاﻛﻨﺶ_Settle_یافت_نشد.ToString();
                case 48:
                    return MellatBankReturnCode.تراکنش_Reverse_شده_است.ToString();
                case 49:
                    return MellatBankReturnCode.تراکنش_Refund_یافت_نشد.ToString();
                case 412:
                    return MellatBankReturnCode.شناسه_قبض_نادرست_است.ToString();
                case 413:
                    return MellatBankReturnCode.ﺷﻨﺎﺳﻪ_ﭘﺮداﺧﺖ_ﻧﺎدرﺳﺖ_اﺳﺖ.ToString();
                case 414:
                    return MellatBankReturnCode.سازﻣﺎن_ﺻﺎدر_ﻛﻨﻨﺪه_ﻗﺒﺾ_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ.ToString();
                case 415:
                    return MellatBankReturnCode.زﻣﺎن_ﺟﻠﺴﻪ_ﻛﺎری_ﺑﻪ_ﭘﺎﻳﺎن_رسیده_است.ToString();
                case 416:
                    return MellatBankReturnCode.ﺧﻄﺎ_در_ﺛﺒﺖ_اﻃﻼﻋﺎت.ToString();
                case 417:
                    return MellatBankReturnCode.ﺷﻨﺎﺳﻪ_ﭘﺮداﺧﺖ_ﻛﻨﻨﺪه_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ.ToString();
                case 418:
                    return MellatBankReturnCode.اﺷﻜﺎل_در_ﺗﻌﺮﻳﻒ_اﻃﻼﻋﺎت_ﻣﺸﺘﺮی.ToString();
                case 419:
                    return MellatBankReturnCode.ﺗﻌﺪاد_دﻓﻌﺎت_ورود_اﻃﻼﻋﺎت_از_ﺣﺪ_ﻣﺠﺎز_ﮔﺬﺷﺘﻪ_اﺳﺖ.ToString();
                case 421:
                    return MellatBankReturnCode.IP_نامعتبر_است.ToString();

                case 51:
                    return MellatBankReturnCode.ﺗﺮاﻛﻨﺶ_ﺗﻜﺮاری_اﺳﺖ.ToString();
                case 54:
                    return MellatBankReturnCode.ﺗﺮاﻛﻨﺶ_ﻣﺮﺟﻊ_ﻣﻮﺟﻮد_ﻧﻴﺴﺖ.ToString();
                case 55:
                    return MellatBankReturnCode.ﺗﺮاﻛﻨﺶ_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ.ToString();
                case 61:
                    return MellatBankReturnCode.ﺧﻄﺎ_در_واریز.ToString();

            }
            return "";
        }
    }
}
