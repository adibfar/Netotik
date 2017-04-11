(function ($) {
    $.fn.MVCDeleteRow = function (options) {
        var defaults = {
            postUrl: '/',
            loginUrl: '/Customer/Login',
            beforePostHandler: null,
            completeHandler: null,
            errorHandler: null,
            id: 0
        };
        options = $.extend(defaults, options);

        function addToken(data) {
            data.__RequestVerificationToken = $("input[name=__RequestVerificationToken]").val();
            return data;
        }

        var row = this;
        return this.each(function () {
            //در اینجا می‌توان مثلا دکمه‌ای را غیرفعال کرد
            if (options.beforePostHandler)
                options.beforePostHandler(this);

            //اطلاعات نباید کش شوند
            $.ajaxSetup({ cache: false });

            $.ajax({
                type: "POST",
                url: options.postUrl,
                data: addToken({ id: options.id }), // اضافه کردن توکن
                dataType: "html", // نوع داده مهم است
                complete: function (xhr, status) {
                    var data = xhr.responseText;
                    if (xhr.status == 403) {
                        window.location = options.loginUrl;
                    }
                    else if (status === 'error' || !data || data == "nok") {
                        alert('خطایی رخ داده است');
                    }
                    else {
                        $(row).fadeTo(800, 0, function () {
                            $(this).remove();
                        });
                    }
                }

            });

        });

    }
})(jQuery);


