/*############ public ##################*/
var Admin = new Object();


Admin.OneImageUpload = function (inputId) {
    $("#" + inputId).fileinput({
        maxFileCount: 10,
        previewFileType: "image",
        browseClass: "btn btn-primary",
        browseLabel: "انتخاب",
        browseIcon: '<i class="glyphicon glyphicon-picture"></i>',
        removeClass: "btn  btn-danger",
        removeLabel: "حذف",
        maxFileSize: 10000,
        removeIcon: '<i class="glyphicon glyphicon-trash"></i>',
        uploadClass: "btn btn-success",
        uploadLabel: "ارسال به سرور",
        allowedFileExtensions: ['jpg', 'gif', 'png', 'jpeg'],
        msgInvalidFileType: "از تصاویر فقط استفاده کنید",
        msgInvalidFileExtension: "از فایل های مجاز استفاده کنید[jpg,jpeg,png,gif]",
        msgFilesTooMany: "شما قادر به ارسال 10 عدد فایل میباشید",
        msgSizeTooLarge: "شما قادر به ارسال 10 مگا بایت فایل میباشید",
        uploadIcon: '<i class="glyphicon glyphicon-upload"></i>'

    });
};



Admin.OneFileUpload = function (inputId) {
    $("#" + inputId).fileinput({
        maxFileCount: 10,
        previewFileType: "image",
        browseClass: "btn btn-primary",
        browseLabel: "انتخاب",
        browseIcon: '<i class="glyphicon glyphicon-link"></i>',
        removeClass: "btn  btn-danger",
        removeLabel: "حذف",
        maxFileSize: 50000,
        removeIcon: '<i class="glyphicon glyphicon-trash"></i>',
        uploadClass: "btn btn-success",
        uploadLabel: "ارسال به سرور",
        allowedFileExtensions: ['doc', 'pdf', 'rar', 'zip'],
        msgInvalidFileType: "فایلهای ارسالی معتبر نیستند",
        msgInvalidFileExtension: "از فایل های مجاز استفاده کنید[pdf,zip,rar,doc]",
        msgFilesTooMany: "شما قادر به ارسال 10 عدد فایل میباشید",
        msgSizeTooLarge: "شما قادر به ارسال 50 مگا بایت فایل میباشید",
        uploadIcon: '<i class="glyphicon glyphicon-upload"></i>'

    });
};



Admin.OneSoundUpload = function (inputId) {
    $("#" + inputId).fileinput({
        maxFileCount: 10,
        previewFileType: "image",
        browseClass: "btn btn-primary",
        browseLabel: "انتخاب",
        browseIcon: '<i class="glyphicon glyphicon-link"></i>',
        removeClass: "btn  btn-danger",
        removeLabel: "حذف",
        maxFileSize: 50000,
        removeIcon: '<i class="glyphicon glyphicon-trash"></i>',
        uploadClass: "btn btn-success",
        uploadLabel: "ارسال به سرور",
        allowedFileExtensions: ['mp3', 'ogg'],
        msgInvalidFileType: "فایلهای ارسالی معتبر نیستند",
        msgInvalidFileExtension: "از فایل های مجاز استفاده کنید[mp3,ogg]",
        msgFilesTooMany: "شما قادر به ارسال 10 عدد فایل میباشید",
        msgSizeTooLarge: "شما قادر به ارسال 50 مگا بایت فایل میباشید",
        uploadIcon: '<i class="glyphicon glyphicon-upload"></i>'

    });
};


Admin.HighLightMenu = function () {

    $(document).ready(function () {
        $("#side-menu li > a").each(function () {
            var $a = $(this);
            var href = $a.attr("href");
            if (href && (location.pathname.toLowerCase().split('/')[2] === href.toLowerCase().split('/')[2])) {
                //صفحه جاری را یافتیم
                $a.closest("ul").addClass("in");
            }
            if (href && (location.pathname.toLowerCase() === href.toLowerCase())) {
                //صفحه جاری را یافتیم
                $a.closest("ul").addClass("in");
                $a.css("color", "#a94442");
            }
        });
    });
};


Admin.CheckAll = function (table, checkAllElement) {

    $("#" + table + " #" + checkAllElement).click(function () {
        if ($("#" + table + " #" + checkAllElement).is(':checked')) {
            $("#" + table + " input[type=checkbox]").each(function () {
                $(this).prop("checked", true);
            });

        } else {
            $("#" + table + " input[type=checkbox]").each(function () {
                $(this).prop("checked", false);
            });
        }
    });
};
Admin.showModal = function () {
    $('#adminModal').modal('show');
};

Admin.hideModal = function () {
    $('#lightBox').modal('hide');
}
Admin.DangerAlert = function (e) {

    swal({
        title: "Are you sure?",
        text: "You will not be able to recover this imaginary file!",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Yes, delete it!",
        cancelButtonText: "No, cancel plx!",
        closeOnConfirm: true,
        closeOnCancel: true
    },
        function (isConfirm) {
            if (isConfirm) {
                alert(isConfirm);
                e.preventDefault();
            }
        });
};
Admin.OnModalClose = function (form) {
    $('#adminModal').on('hidden.bs.modal', function () {
        $("#" + form).submit();
    });
};

var LightBox = new Object();
LightBox.onSuccess = function () {
    $('#lightBox').modal('show');
}



$(document).ready(function () {

    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
    });

    $('.color-picker').colorpicker();

    $('.summernote').summernote(
        {
            minHeight: 300
        });


    $('.test').summernote(
       {
           minHeight: 300
       });

    var config = {
        '.chosen-select': {},
        '.chosen-select-deselect': { allow_single_deselect: true },
        '.chosen-select-no-single': { disable_search_threshold: 10 },
        '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
        '.chosen-select-width': { width: "95%" }
    }
    for (var selector in config) {
        $(selector).chosen(config[selector]);
    }
});