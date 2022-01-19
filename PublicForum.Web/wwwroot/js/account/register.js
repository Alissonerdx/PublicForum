$(document).ready(function () {
    Events();

    function Events() {
        $(document).on('submit', '#registerForm', function (e) {
            e.preventDefault();

            let data = new FormData($(this)[0]);

            $.ajax({
                type: 'POST',
                url: "/Account/Register",
                data: data,
                processData: false,
                contentType: false

            }).done(function (response) {
                if (response.success === true) {
                    iziToast.success({
                        id: "success",
                        title: "Success!",
                        message: response.message,
                        position: 'topRight',
                        transitionIn: 'bounceInLeft',
                    });
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