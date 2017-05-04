

$(function () {
    AjaxForm.EnablePostbackValidation();
    Selectize();
    StartSummerNote();

    $(".datepicker").datetimepicker({ defaultDate: "" });
    $("#unslider").unslider({ animation: "fade", infinite: !0 });
});


var AjaxForm = new Object();

AjaxForm.EnableAjaxFormvalidate = function (formId) {
    $.validator.unobtrusive.parse('#' + formId);
};


AjaxForm.ValidateForm = function (formId) {
    var val = $('#' + formId).validate();
    val.form();
    return val.valid();
};

Icheck = function () {
    $('.skin-flat input').iCheck({ checkboxClass: "icheckbox_flat-green", radioClass: "iradio_flat-green" });
}
Selectize = function () {
    $('.selectize-select').selectize();
}

EditSelectize = function () {
    $('#Edit .selectize-select').selectize();
}

CardBlock = function (element) {
    $(element).block(
        {
            message: 'در حال پردازش <div class="icon-spinner10 icon-spin icon-lg"></div>',
            overlayCSS: { backgroundColor: "#fff", opacity: .7, cursor: "wait" },
            css: { border: 0, padding: 0, backgroundColor: "transparent" }
        });
};

CardUnBlock = function (element) {
    $(element).unblock();
};

StartSummerNote = function () {
    $(".summernote").summernote({
        minHeight: 300,             // set minimum height of editor
    });
}


PostData = function (path, params, method) {
    method = method || "post"; // Set method to post by default if not specified.

    // The rest of this code assumes you are not using a library.
    // It can be made less wordy if you use one.
    var form = document.createElement("form");
    form.setAttribute("method", method);
    form.setAttribute("action", path);

    for (var key in params) {
        if (params.hasOwnProperty(key)) {
            var hiddenField = document.createElement("input");
            hiddenField.setAttribute("type", "hidden");
            hiddenField.setAttribute("name", key);
            hiddenField.setAttribute("value", params[key]);

            form.appendChild(hiddenField);
        }
    }

    document.body.appendChild(form);
    form.submit();
}


AjaxForm.Logout = function () {
    $.post('/Account/LogOut', null, null, null);
    window.location = '/Account/LogIn';
};

AjaxForm.Post = function (element, formId) {
    if (!AjaxForm.ValidateForm(formId)) return;
    $(element).button('loading');
    $('#' + formId).submit();
};



AjaxForm.ResetButton = function (id) {
    $('#' + id).button('reset');
};

AjaxForm.EnablePostbackValidation = function () {
    $('form').each(function () {
        $(this).find('div.form-group').each(function () {
            if ($(this).find('span.field-validation-error').length > 0) {
                $(this).addClass('has-error');
            }
        });
    });
};


function makeUploadFile(id) {
    $("#" + id).fileinput({
        showUpload: false,
        previewFileType: "image",
        msgInvalidFileType: "از فایل معتبر استفاده کنید",
        maxFileSize: "10000",
        msgSizeTooLarge: "حجم فایل مورد نظر بیشتر از حجم مورد قبول است",
        browseClass: "btn btn-success",
        browseLabel: "انتخاب تصویر",
        browseIcon: '<i class="icon-picture"></i>',
        removeClass: "btn btn-danger",
        removeLabel: "حذف",
        removeIcon: '<i class="icon-trash"></i>',
        allowedFileExtensions: ["jpg", "gif", "png"]
    });
}

function Routin() {
    $("input").attr("autocomplete", "off");
};



//$.validator.setDefaults({
//    ignore: "",
//    showErrors: function (errorMap, errorList) {
//        this.defaultShowErrors();
//        $("." + this.settings.validClass).tooltip("destroy");
//        for (var i = 0; i < errorList.length; i++) {
//            var error = errorList[i];
//            $(error.element).tooltip({ trigger: "focus" }) 
//                            .attr("data-original-title", error.message);
//        }
//    },
//    highlight: function (element, errorClass, validClass) {
//        if (element.type === 'radio') {
//            this.findByName(element.name).addClass(errorClass).removeClass(validClass);
//        } else {
//            $(element).addClass(errorClass).removeClass(validClass);
//            $(element).closest('.control-group').removeClass('has-success').addClass('has-error');
//        }
//        $(element).trigger('highlited');
//    },
//    unhighlight: function (element, errorClass, validClass) {
//        if (element.type === 'radio') {
//            this.findByName(element.name).removeClass(errorClass).addClass(validClass);
//        } else {
//            $(element).removeClass(errorClass).addClass(validClass);
//            $(element).closest('.control-group').removeClass('has-error').addClass('has-success');
//        }
//        $(element).trigger('unhighlited');
//    }
//});


$.validator.setDefaults({
    ignore: "",
    highlight: function (element) {
        $(element).closest('.form-group').addClass('error');
    },
    unhighlight: function (element) {
        $(element).closest('.form-group').removeClass('error');
    },
    errorElement: 'span',
    errorClass: '',
    errorPlacement: function (error, element) {
        if (element.parent('.input-group').length) {
            error.insertAfter(element.parent());
        } else {
            error.insertAfter(element);
        }
    }
});

$(function () {
    $('form').each(function () {
        $(this).find('.form-group').each(function () {
            if ($(this).find('span.field-validation-error').length > 0) {
                $(this).addClass('has-error');
            }
        });
    });
});




