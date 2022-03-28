$(function () {
    $.ajaxSetup({
        beforeSend: function (xhr) {
            var rd = Math.floor((Math.random() * 9999999) + 1);
            xhr.setRequestHeader('Authorization', getSessionToken());
            xhr.setRequestHeader('Accept-Language', 'vi-VN');
            xhr.setRequestHeader('Loading-Id', rd);
            showLoading();
        },
        complete: function (xhr, status, error) {
            if (xhr.status == 401)
                swal("Unauthorized!", "Bạn cần phải đăng nhập vào hệ thống!", "warning");
                //toastr.error("Dữ liệu đầu vào hoặc thông tin tài khoản không hợp lệ!", "Lỗi!", { progressBar: true });
            else if (xhr.status == 404)
                //swal("Not found!", "Không tìm thấy đối tượng để xử lý!", "warning");
                toastr.error("Không tìm thấy đối tượng để xử lý!", "Lỗi!", { progressBar: true });
            else if (xhr.status == 500)
                //swal("Internal Server Error!", "Có lỗi xảy ra trong quá trình xử lý!", "warning");
                toastr.error("Có lỗi xảy ra trong quá trình xử lý!", "Lỗi!", { progressBar: true });
            else if (xhr.status == 400)
                //swal("Lỗi dữ liệu!", "Dữ liệu đầu vào hoặc thông tin tài khoản không hợp lệ!", "warning");
                toastr.error("Dữ liệu đầu vào hoặc thông tin tài khoản không hợp lệ!", "Lỗi!", { progressBar: true });
            //else if (xhr.status != 200)
            //    swal(error + "!", "Có lỗi trong quá trình xử lý!", "error");
        }
    });

    $(document).ajaxStop(function () {
        hideLoading();
    });
})
function getSessionToken() {
    if (sessionStorage['SessionToken'] != undefined)
        return 'Bearer ' + sessionStorage['SessionToken'];
    return null;
}
function callApi_report(controller, action, method, data, callbackSuccess, callbackError) {
    $.ajax({
        type: method,
        url: apiConfig.api.host_report_url + controller + action,
        contentType: "application/json; charset=utf-8",
        data: (method == 'GET' ? data : JSON.stringify(data)),
        success: function (result) {
            if (window[callbackSuccess] != undefined)
                window[callbackSuccess](result);
        },
        error: function (request, status, error) {
            if (window[callbackError] != undefined)
                window[callbackError](request, status, error);
        }
    });
}
