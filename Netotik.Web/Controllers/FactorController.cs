using Netotik.Services.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using Netotik.Data;
using Netotik.ViewModels;
using System.Data.Entity;
using Netotik.Common.Controller;
using Netotik.Web.Extension;
using Netotik.Web.Infrastructure;
using Netotik.ViewModels.ShopPublic;
using Newtonsoft.Json;
using Netotik.Web.Infrastructure.Filters;
using Netotik.Domain.Entity;
using Netotik.Resources;

namespace Netotik.Web.Controllers
{
    public partial class FactorController : BaseController
    {
        private readonly IUserMailer _userMailerService;
        private readonly IOrderService _orderService;
        private readonly IOrderPaymentService _orderPaymentService;
        private readonly IDiscountService _discountService;
        private readonly IPaymentTypeService _paymentTypeService;
        private readonly IStateService _addressStateService;
        private readonly ICityService _addressCityService;
        private readonly IProductService _productSrevice;
        private readonly ICategoryService _categoryService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IUnitOfWork _uow;

        public FactorController(
            IUserMailer userMailerService,
            IOrderService orderService,
            IOrderPaymentService orderPaymentService,
            IDiscountService discountService,
            IPaymentTypeService paymentTypeService,
            ICityService addressCityService,
            IStateService addressStateService,
            IManufacturerService manufacturerService,
            ICategoryService categoryService,
            IProductService productService,
            IUnitOfWork uow)
        {
            _userMailerService = userMailerService;
            _orderPaymentService = orderPaymentService;
            _orderService = orderService;
            _discountService = discountService;
            _paymentTypeService = paymentTypeService;
            _addressCityService = addressCityService;
            _addressStateService = addressStateService;
            _categoryService = categoryService;
            _manufacturerService = manufacturerService;
            _productSrevice = productService;
            _uow = uow;
        }


        public virtual async Task<ActionResult> Index()
        {
            if (User != null)
            {
                await LoadState();
                await loadPaymentType();
            }

            await LoadTrolly();
            var itemCount = new List<SelectListItem>(); ;
            for (int i = 1; i <= 30; i++)
                itemCount.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
            ViewBag.ItemCount = itemCount;

            return View();
        }


        [HttpPost] // Show OrderDetail
        public virtual async Task<ActionResult> Index(FactorModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    this.MessageError(Messages.MissionFail, "اطلاعات وارد شده نامعتبر است لطفا دوباره اطلاعات را وارد کنید");
                    return RedirectToAction(MVC.Factor.ActionNames.Index);
                }
                List<ShoppingCartModel> cart;
                model.Email = "";
                cart = JsonConvert.DeserializeObject<List<ShoppingCartModel>>(HttpContext.GetCookieValue("ShoppingCart"));
                var result = await _orderService.GetFactorDetailAsync(model, cart);
                var order = await _orderService.RegisterFactorAsync(model, cart, HttpContext.Request.UserHostAddress, BuyerType.user);

                var orderPayment = order.OrderPayments.Last();
                var paymentType = orderPayment.PaymentType;


                //long orderID = 0; //شماره تراکنش که باید منحصر به فرد باشد
                //long priceAmount = 20000; // هزینه ایی که کاربر در صفحه پرداخت باید آن را بپردازد
                //string additionalText = "خرید یک محصول "; // توضیحات شما برای این تراکنش

                _orderPaymentService.Update(orderPayment);
                _uow.SaveChanges();


                //var bpmService = new BankMellatServices.BpmServices(paymentType.TerminalId, paymentType.UserName, paymentType.Password, Url.Action(MVC.Factor.ActionNames.Result, MVC.Factor.Name, new { asx = orderPayment.Id }));
                //string resultRequest = bpmService.bpPayRequest(orderPayment.Id, (long)orderPayment.Amont, "خرید محصول");
                //string[] StatusSendRequest = resultRequest.Split(',');

                //if (int.Parse(StatusSendRequest[0]) == (int)MellatBankReturnCode.ﺗﺮاﻛﻨﺶ_ﺑﺎ_ﻣﻮﻓﻘﻴﺖ_اﻧﺠﺎم_ﺷﺪ)
                //{
                //    ViewBag.RefId = StatusSendRequest[1];
                //    ViewBag.GateWay = paymentType.GateWayUrl;
                //}
                //else
                //{
                //    SetResultMessage(false, MessageColor.Danger, bpmService.DesribtionStatusCode(int.Parse(StatusSendRequest[0].Replace("_", " "))), "");
                //    return RedirectToAction(MVC.Factor.ActionNames.Index);
                //}


                return View(MVC.Factor.Views.OrderDetail, result);
            }
            catch
            {
                return RedirectToAction(MVC.Factor.ActionNames.Index);
            }
        }



        [HttpPost] // Register & Payment Factor
        public virtual async Task<ActionResult> Payment(FactorModel model)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    this.MessageError(Messages.MissionFail, "اطلاعات وارد شده نامعتبر است لطفا دوباره اطلاعات را وارد کنید");
                    return RedirectToAction(MVC.Factor.ActionNames.Index);
                }
                List<ShoppingCartModel> cart;
                cart = JsonConvert.DeserializeObject<List<ShoppingCartModel>>(HttpContext.GetCookieValue("ShoppingCart"));
                var order = await _orderService.RegisterFactorAsync(model, cart, HttpContext.Request.UserHostAddress, BuyerType.user);

                var orderPayment = order.OrderPayments.Last();
                var paymentType = orderPayment.PaymentType;


                //long orderID = 0; //شماره تراکنش که باید منحصر به فرد باشد
                //long priceAmount = 20000; // هزینه ایی که کاربر در صفحه پرداخت باید آن را بپردازد
                //string additionalText = "خرید یک محصول "; // توضیحات شما برای این تراکنش

                _orderPaymentService.Update(orderPayment);
                _uow.SaveChanges();


                //var bpmService = new BankMellatServices.BpmServices(paymentType.TerminalId, paymentType.UserName, paymentType.Password, Url.Action(MVC.Factor.ActionNames.Result, MVC.Factor.Name, new { asx = orderPayment.Id }));
                //string resultRequest = bpmService.bpPayRequest(orderPayment.Id, (long)orderPayment.Amont, "خرید محصول");
                //string[] StatusSendRequest = resultRequest.Split(',');

                //if (int.Parse(StatusSendRequest[0]) == (int)MellatBankReturnCode.ﺗﺮاﻛﻨﺶ_ﺑﺎ_ﻣﻮﻓﻘﻴﺖ_اﻧﺠﺎم_ﺷﺪ)
                //{
                //    return RedirectToAction("RedirectVPOS", "Payment", new { id = StatusSendRequest[1] });
                //}
                //SetResultMessage(false, MessageColor.Danger, bpmService.DesribtionStatusCode(int.Parse(StatusSendRequest[0].Replace("_", " "))), "");

            }
            catch
            {
                this.MessageError(Messages.MissionFail, "متاسفانه خطایی رخ داده است. لطفا بعدا امتحان کنید.");
            }
            return RedirectToAction(MVC.Factor.ActionNames.Index);





            //await Task.Run(() =>
            // {
            //     var payline = new PaylineGateway();
            //     string result = payline.Send("http://payline.ir/payment-test/gateway-send", paymentType.ApiCode, order.PaymentPrice * 10, Url.Action(MVC.Factor.ActionNames.Result, MVC.Factor.Name, new { asx = orderPayment.Id }, "http"));
            //     if (int.Parse(result) > 0)
            //     {
            //         orderPayment.Result = result;
            //         _orderPaymentService.Update(orderPayment);
            //         _uow.SaveChanges();

            //         Response.Redirect("http://payline.ir/payment-test/gateway-" + result);
            //     }
            //     else
            //     {
            //         SetResultMessage(false, MessageColor.Danger, payline.GetErrorByResultId(result), "");
            //     }

            // });


        }

        public virtual async Task<ActionResult> Result(int? asx)
        {

            try
            {

                if (Request.Form["trans_id"] != null && Request.Form["id_get"] != null && asx.HasValue)
                {
                    PaylineGateway GetPayline = new PaylineGateway();
                    string trans_id = Request.Form["trans_id"];
                    string id_get = Request.Form["id_get"];
                    var orderPayment = _orderPaymentService.GetById(asx.Value);

                    string result = "";//GetPayline.Get("http://payline.ir/payment-test/gateway-result-second", orderPayment.PaymentType.TerminalId, trans_id, id_get);

                    if (result == "1")
                    {
                        orderPayment.TransactionId = trans_id;
                        orderPayment.GetId = id_get;
                        orderPayment.IsSucess = true;
                        orderPayment.Order.PaymentStatus = PaymentStatus.Success;
                        _orderPaymentService.Update(orderPayment);
                        _uow.SaveChanges();
                        HttpContext.UpdateCookie("ShoppingCart", string.Empty);

                        //await _userMailerService.Factor(orderPayment, new string[] { orderPayment.Order.Address.Email }).SendAsync();

                        return View(orderPayment);
                    }
                    else
                    {
                        string message = "";
                        switch (result)
                        {
                            case "-1":
                                message = "api ارسالی با نوع api تعریف شده در payline .سازگار نیست";
                                break;
                            case "-2":
                                message = "trans_id .ارسال شده معتبر نمی باشد";
                                break;
                            case "-3":
                                message = "id_getارسالی معتبر نمی باشد";
                                break;
                            case "-4":
                                message = ".چنین تراکنشی در سیستم وجود ندارد و یا موفقیت آمیز نبوده است";
                                break;
                        }

                        this.MessageError(Messages.MissionFail, message);
                    }
                }
                return RedirectToAction(MVC.Factor.ActionNames.Index);
            }
            catch
            {
                return RedirectToAction(MVC.Factor.ActionNames.Index);
            }

        }



        [HttpPost]
        public virtual ActionResult ClearCart()
        {
            HttpContext.UpdateCookie("ShoppingCart", string.Empty);
            return RedirectToAction(MVC.Home.ActionNames.Index);
        }




        /// <summary>
        /// حذف محصول از سبد خرید
        /// </summary>
        /// <param name="id">کد محصول</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult Delete(int id)
        {

            List<ShoppingCartModel> cart;

            if (!string.IsNullOrEmpty(HttpContext.GetCookieValue("ShoppingCart")))
            {
                cart = JsonConvert.DeserializeObject<List<ShoppingCartModel>>(HttpContext.GetCookieValue("ShoppingCart"));
                var removeItem = cart.Find(x => x.Id == id);
                if (removeItem != null)
                {
                    cart.Remove(removeItem);
                    if (!string.IsNullOrEmpty(HttpContext.GetCookieValue("ShoppingCart")))
                        HttpContext.UpdateCookie("ShoppingCart", JsonConvert.SerializeObject(cart));
                }
            }

            return RedirectToAction(MVC.Factor.ActionNames.Index);
        }

        [HttpPost]
        public virtual ActionResult UpdateCart(int id, int count, int? colorId, int? sizeId)
        {
            var prod = _productSrevice
                .All()
                .AsNoTracking()
                .Include(x => x.Categories)
                .Include(x => x.Manufacturer.Discounts)
                .Include(x => x.Discounts)
                .FirstOrDefault(x => !x.IsDeleted && x.Price.HasValue && x.IsPublished && x.Id == id);


            List<ShoppingCartModel> cart;

            if (string.IsNullOrEmpty(HttpContext.GetCookieValue("ShoppingCart"))) cart = new List<ShoppingCartModel>();
            else cart = JsonConvert.DeserializeObject<List<ShoppingCartModel>>(HttpContext.GetCookieValue("ShoppingCart"));

            if (prod.OrderMaximumQuantity < count)
            {
                this.MessageWarning("توجه!!!", string.Format("حداکثر تعداد خرید برای این محصول {0} عدد می باشد", prod.OrderMaximumQuantity));
                return RedirectToAction(MVC.Factor.ActionNames.Index);
            }

            var cartItem = cart.FirstOrDefault(x => x.Id == prod.Id);
            if (cartItem != null) cartItem.Count = count;
            else return RedirectToAction(MVC.Factor.ActionNames.Index);
            cartItem.TotalPtice = (cartItem.UnitPrice - cartItem.OffPrice) * count;


            if (!string.IsNullOrEmpty(HttpContext.GetCookieValue("ShoppingCart")))
                HttpContext.UpdateCookie("ShoppingCart", JsonConvert.SerializeObject(cart));

            return RedirectToAction(MVC.Factor.ActionNames.Index);
        }

        #region Ajax

        [HttpPost]
        [AjaxOnly]
        public virtual ActionResult FillCity(int? stateId)
        {
            return stateId.HasValue ? Json(_addressCityService.GetByStateId(stateId.Value)) : Json(null);
        }


        [HttpPost]
        [AjaxOnly]
        public virtual async Task<ActionResult> CheckCoupon(decimal totalPrice, string couponText)
        {
            return Json(await _discountService.GetCouponDiscount(totalPrice, couponText));
        }

        #endregion


        #region Private

        private async Task loadPaymentType()
        {
            ViewBag.PaymentTypes = await _paymentTypeService
                .All()
                .AsNoTracking()
                .Include(x => x.Picture)
                .Where(x => x.IsActive)
                .Select(x => new PaymentTypeListModel
                {
                    Id = x.Id,
                    imgName = x.PictureId.HasValue ? x.Picture.FileName : "",
                    Name = x.Name,
                    Description = x.Description,
                    Selected = x.IsDefault
                }).ToListAsync();

        }
        private async Task LoadState()
        {
            var list = await _addressStateService.All().ToListAsync();
            ViewBag.States = new SelectList(list, "Id", "Name", list.FirstOrDefault(x => x.IsDefault));
        }

        private async Task LoadTrolly()
        {

            List<ShoppingCartModel> cart;
            string cartCookie = HttpContext.GetCookieValue("ShoppingCart");

            if (string.IsNullOrEmpty(cartCookie))
                cart = new List<ShoppingCartModel>();
            else cart = JsonConvert.DeserializeObject<List<ShoppingCartModel>>(cartCookie);


            var list = await _productSrevice.GetForFactorByIdsAsync(cart);
            ViewBag.Trolly = list;


            //calculate Discount Factor
            decimal factorPriceOff = await _discountService.GetDsicountFactorAsync(cart.Sum(x => x.UnitPrice * x.Count));
            ViewBag.sumPrice = factorPriceOff;
        }

        #endregion


    }
}