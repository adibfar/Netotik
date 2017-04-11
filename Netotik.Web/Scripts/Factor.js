
var AjaxForm = new Object();

AjaxForm.EnableAjaxFormvalidate = function (formId) {
    $.validator.unobtrusive.parse('#' + formId);
};


AjaxForm.ValidateForm = function (formId) {
    var val = $('#' + formId).validate();
    val.form();
    return val.valid();
};

AjaxForm.FactorSubmit = function (element, formId) {
    var shipping = $("input[type='radio'][name='ShipingMethodId']:checked");
    if (shipping.length <= 0) {
        alert('لطفا شیوه ارسال را مشخص کنید');
        return;
    }
    var payment = $("input[type='radio'][name='PaymentTypeId']:checked");
    if (payment.length <= 0) {
        alert('لطفا درگاه پرداخت را مشخص کنید');
        return;
    }

    if (!AjaxForm.ValidateForm(formId)) {
        alert('اطلاعات تماس خود را کامل وارد نمایید.');
        return;
    }
    $(element).button('loading');
    $('#' + formId).submit();
};

UpdateTrolly = function (productId) {
    $('#form' + productId).submit();
};


AjaxForm.CustomSubmit = function (element, formId) {
    if (!AjaxForm.ValidateForm(formId)) return;
    $(element).button('loading');
    $('#' + formId).submit();
};
function FillCity() {
    try {
        var stateId = $('#StateId').val();
        if (stateId == '') return;
        $.ajax({
            url: '/Factor/FillCity',
            type: 'Post',
            dataType: 'JSON',
            data: { stateId: stateId },
            success: function (cities) {
                $('#CityId').html("");
                $.each(cities, function (i, city) {
                    $("#CityId").append(
                        $('<option></option>').val(city.Id).html(city.Name));
                });
            }
        });
    }
    catch (a) { };
}

function addCommas(nStr) {
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}

function CheckCoupon() {
    try {
        var coupon = $('#coupon').val();
        var totalPrice = parseInt($('#totalPrice').text().replace(',', ''));

        if (coupon == '') {
            $('#couponPrice').text('00');
            UpdateFactorPaymentPrice();
            return
        };
        $.ajax({
            url: '/Factor/CheckCoupon',
            type: 'Post',
            dataType: 'JSON',
            data: { couponText: coupon, totalPrice: totalPrice },
            success: function (off) {
                if (off > 0) {
                    $('#couponPrice').text(addCommas(off));
                    UpdateFactorPaymentPrice();
                    alert('تبریک!! تخفیف شما : ' + off);
                } else {
                    $('#couponPrice').text('00');
                    UpdateFactorPaymentPrice();
                    alert('کد تخفیف شما نامعتبر است.');
                }
            }
        });
    }
    catch (a) { };
}

function UpdateFactorPaymentPrice() {
    var couponPrice = parseInt($('#couponPrice').text().replace(',', ''));
    var totalPrice = parseInt($('#totalPrice').text().replace(',', ''));
    var totalOffPrice = parseInt($('#totalOffPrice').text().replace(',', ''));
    var factorOff = parseInt($('#factorOff').text().replace(',', ''));
    var totalTaxPrice = parseInt($('#totalTaxPrice').text().replace(',', ''));
    var totalShippingPrice = parseInt($('#totalShippingPrice').text().replace(',', ''));

    $('#totalPaymentPrice').text(addCommas(totalPrice - totalOffPrice - couponPrice - factorOff + totalTaxPrice + totalShippingPrice));
}


// SHIPPING CHANGE 
function changeShipment(price) {
    $('#totalShippingPrice').text(addCommas(price));
    UpdateFactorPaymentPrice();
};

