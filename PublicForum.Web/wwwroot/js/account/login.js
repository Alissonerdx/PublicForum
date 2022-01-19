$(document).ready(function () {
    Events();

    function Events() {
        $(document).on('submit', '#loginForm', function (e) {
            e.preventDefault();

            let data = new FormData($(this)[0]);

            $.ajax({
                type: 'POST',
                url: "/Account/Login",
                data: data,
                processData: false,
                contentType: false

            }).done(function (response) {
                if (response.success === true) {
                    if (response.redirect != null && response.redirect != undefined) {
                        window.location.href = response.redirect;
                    }
                }
                else {
                    iziToast.error({
                        id: "error",
                        title: "Error!",
                        message: response.message,
                        position: 'topRight',
                        transitionIn: 'bounceInLeft',
                    });
                }
            }).fail(function (response) {
                iziToast.error({
                    id: "error",
                    title: "Error!",
                    message: response.message,
                    position: 'topRight',
                    transitionIn: 'bounceInLeft',
                });
            });

            e.stopImmediatePropagation();
            return false;
        })
    }
})