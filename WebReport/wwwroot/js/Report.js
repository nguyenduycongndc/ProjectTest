
function openView(type, value) {
    var index = $("#view");
    var detail = $("#detail");
    if (type === 0) {
        index.show();
        detail.hide();
        setTimeout(function () {
            onSearch();
        }, 100);
    }
    else if (type === 1) {
        index.hide();
        detail.show();
        $("#subjectId").val(value)
        setTimeout(function () {
            onSearchDetail();
        }, 100);
    }
}
window.onload = function () {
    setTimeout(function () {
        openView(0, 0);
    }, 100);

}
function onSearch() {
    var convert_todate = new Date($('#ToDate').val() + ',' + "00:00:01").getTime() / 1000;
    var convert_fromdate = new Date($('#FromDate').val() + ',' + "23:59:59").getTime() / 1000;
    //var convert_todate = Date.parse($('#ToDate').val()).toString();
    //var index_todate = convert_todate.lastIndexOf("000");
    //var sub_todate = convert_todate.substring(index_todate,0)
    //var convert_fromdate = Date.parse($('#FromDate').val()).toString();
    //var index_fromdate = convert_fromdate.lastIndexOf("000");
    //var sub_fromdate = convert_fromdate.substring(index_fromdate,0)
    var obj = {
        'name': $('#Name').val().trim(),
        'department': $('#Department').val().trim(),
        'description': $('#Description').val().trim(),
        'todate': convert_todate,
        'fromdate': convert_fromdate,
        //'todate': parseInt(sub_todate),
        //'fromdate': parseInt(sub_fromdate),
        'page_size': parseInt($("#cbPageSize").val()),
        'start_number': (parseInt($("#txtCurrentPage").val()) - 1) * parseInt($("#cbPageSize").val())
    }
    callApi_report(
        apiConfig.api.report.controller,
        apiConfig.api.report.action.search.path,
        apiConfig.api.report.action.search.method,
        { 'jsonData': JSON.stringify(obj) }, 'fnSearchSuccess', 'msgError');
}
function fnSearchSuccess(rspn) {
    showLoading();
    if (rspn !== undefined && rspn !== null) {
        var tbBody = $('#reportTable tbody');
        $("#reportTable").dataTable().fnDestroy();
        tbBody.html('');
        for (var i = 0; i < rspn.data.length; i++) {
            var obj = rspn.data[i];
            var dateconvert = new Date(parseInt(obj.timestamp + "000")).toLocaleDateString();
            var dateconvertfirstlogin = new Date(parseInt(obj.firstlogin + "000")).toLocaleTimeString();
            var dateconvertlastlogout = obj.lastlogout != null ? new Date(parseInt(obj.lastlogout + "000")).toLocaleTimeString() : "";
            var Hoursfirstlogin = new Date(parseInt(obj.firstlogin + "000")).getHours();
            var Minutesfirstlogin = new Date(parseInt(obj.firstlogin + "000")).getMinutes();
            var Hourslastlogout = new Date(parseInt(obj.lastlogout + "000")).getHours();
            var Minuteslastlogout = new Date(parseInt(obj.lastlogout + "000")).getMinutes();

            //var timedateconvert = obj.time;
            //var timedateconvertfirstlogin = obj.timefirstlogin;
            //var timedateconvertlastlogout = obj.timelastlogout;
            //var timeHoursfirstlogin = new Date(obj.timefirstlogin).getHours();
            //var timeMinutesfirstlogin = new Date(obj.timefirstlogin).getMinutes();
            //var timeHourslastlogout = new Date(obj.timelastlogout).getHours();
            //var timeMinuteslastlogout = new Date(obj.timelastlogout).getMinutes();

            var html = '<tr>' +
                '<td class="text-center"></td>' +
                '<td>' + obj.description + '</td>' +
                '<td>' + obj.department + '</td>' +
                '<td>' + obj.name + '</td>' +
                '<td>' + obj.job_number + '</td>' +
                '<td>' + obj.email + '</td>' +
                '<td>' + obj.phone + '</td>' +
                '<td>' + dateconvert + '</td>' +
                '<td>' + dateconvertfirstlogin + '</td>' +
                '<td>' + dateconvertlastlogout + '</td>' +
                '<td>' + (obj.lastlogout != null ? ((((Hourslastlogout <= 11) && (Minuteslastlogout < 30)) || ((Hoursfirstlogin >= 13) && (Minutesfirstlogin > 0))) ? (Math.abs(Hourslastlogout - Hoursfirstlogin) + "giờ" + Math.abs(Minuteslastlogout - Minutesfirstlogin) + "phút") : ((Hourslastlogout - Hoursfirstlogin) == 8 && (Minuteslastlogout - Minutesfirstlogin) > 0 ? "8 giờ" : (Math.abs(Hourslastlogout - Hoursfirstlogin - 1) + "giờ" + Math.abs(Minuteslastlogout - Minutesfirstlogin - 30) + "phút"))) : "") + '</td>' +
                '<td>' + ((Hoursfirstlogin >= 10) ? "Vắng buổi sáng" : ((Hourslastlogout <= 15) ? "Vắng buổi chiều" : "")) + '</td>' +
                '<td>' + (obj.lastlogout != null ? ((((Hourslastlogout <= 11) && (Minuteslastlogout < 30)) || ((Hoursfirstlogin >= 13) && (Minutesfirstlogin > 0))) ? (8 - Math.abs(Hourslastlogout - Hoursfirstlogin) + "giờ" + (Math.abs(Minuteslastlogout - Minutesfirstlogin) != 0 ? (60 - Math.abs(Minuteslastlogout - Minutesfirstlogin)) : Math.abs(Minuteslastlogout - Minutesfirstlogin)) + "phút") : ((Hourslastlogout - Hoursfirstlogin) == 8 && (Minuteslastlogout - Minutesfirstlogin) > 0 ? "8 giờ" : (8 - Math.abs(Hourslastlogout - Hoursfirstlogin - 1) + "giờ" + (Math.abs(Minuteslastlogout - Minutesfirstlogin - 30) != 0 ? (60 - Math.abs(Minuteslastlogout - Minutesfirstlogin - 30)) : Math.abs(Minuteslastlogout - Minutesfirstlogin - 30)) + "phút"))) : "") + '</td>' +
                '<td>' + obj.camera_position + '</td>' +
                '<td class="text-center col-action">' +
                '<a type="button" class="btn-action-custom" onclick="openView(1,' + obj.subject_id + ')"><i data-toggle="tooltip" title="Xem thông tin" class="fa fa-eye" aria-hidden="true"></i></a>' +
                '</td>' +
                '</tr>';
            tbBody.append(html);
        }
        var page_size = (parseInt($("#txtCurrentPage").val()) - 1) * parseInt($("#cbPageSize").val())
        var t = $("#reportTable").DataTable({
            "bPaginate": false,
            "bLengthChange": false,
            "bFilter": false,
            "bInfo": false,
            "columnDefs": [
                {
                    "targets": 0,
                    "className": "text-center",
                    "orderable": false,
                    "data": null,
                    "order": [],
                    render: function (data, type, row, meta) {

                        return meta.row + page_size + 1;
                    }
                },
                {
                    "targets": null,
                    "searchable": false,
                    "orderable": false
                }],
            "order": [],
            "drawCallback": function (settings) {
                $('[data-toggle="tooltip"]').tooltip();
            },
        });
        t.on('order.dt search.dt', function () {
            t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                cell.innerHTML = i + page_size + 1;
            });
        }).draw();
        reCalculatPagesCustom(rspn.total);
        viewBtnActionPage();
        hideLoading();
    } else if (rspn == "") {
        var tbBody = $('#reportTable tbody');
        $("#reportTable").dataTable().fnDestroy();
        tbBody.html('');

        var page_size = (parseInt($("#txtCurrentPage").val()) - 1) * parseInt($("#cbPageSize").val())
        var t = $("#reportTable").DataTable({
            "bPaginate": false,
            "bLengthChange": false,
            "bFilter": false,
            "bInfo": false,
            "columnDefs": [
                {
                    "targets": 0,
                    "className": "text-center",
                    "orderable": false,
                    "data": null,
                    "order": [],
                    render: function (data, type, row, meta) {

                        return meta.row + page_size + 1;
                    }
                },
                {
                    "targets": null,
                    "searchable": false,
                    "orderable": false
                }],
            "order": [],
            "drawCallback": function (settings) {
                $('[data-toggle="tooltip"]').tooltip();
            },
        });
        t.on('order.dt search.dt', function () {
            t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                cell.innerHTML = i + page_size + 1;
            });
        }).draw();
        reCalculatPagesCustomNull();
        hideLoading();
    }
}
//xuất excel

function ExportExcel() {
    var convert_todate = new Date($('#ToDate').val() + ',' + "00:00:01").getTime() / 1000;
    var convert_fromdate = new Date($('#FromDate').val() + ',' + "23:59:59").getTime() / 1000;
    var obj = {
        'name': $('#Name').val().trim(),
        'department': $('#Department').val().trim(),
        'description': $('#Description').val().trim(),
        'todate': convert_todate,
        'fromdate': convert_fromdate,
        'page_size': parseInt($("#cbPageSize").val()),
        'start_number': (parseInt($("#txtCurrentPage").val()) - 1) * parseInt($("#cbPageSize").val())
    }

    var jsonData = JSON.stringify(obj);

    var request = new XMLHttpRequest();
    request.responseType = "blob";
    request.open("GET", apiConfig.api.host_report_url + apiConfig.api.report.controller +
        apiConfig.api.report.action.exportexcel.path + "?jsonData=" + jsonData);
    //request.setRequestHeader('Authorization', getSessionToken());
    request.setRequestHeader('Accept-Language', 'vi-VN');
    request.onload = function () {
        if (this.status == 200) {
            var link = document.createElement('a');
            link.href = window.URL.createObjectURL(this.response);
            link.download = "ReportFile.xlsx";
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        }
        //if (this.status == 404) {
        //    /*toastr.error("Không tìm thấy dữ liệu!", "Lỗi!", { progressBar: true });*/
        //    swal("Error!", "Không tìm thấy dữ liệu", "error");
        //}
        //if (this.status == 400) {
        //    //toastr.error("Có lỗi xảy ra!", "Lỗi!", { progressBar: true });
        //    swal("Error!", "Có lỗi trong quá trình xử lý!", "error");
        //}
    }
    request.send();
}
function onSearchDetail() {
    var obj = {
        'subject_id': parseInt($('#subjectId').val()),
        'page_size': parseInt($("#cbPageSizeDetail").val()),
        'start_number': (parseInt($("#txtCurrentPageDetail").val()) - 1) * parseInt($("#cbPageSizeDetail").val())
    }
    callApi_report(
        apiConfig.api.report.controller,
        apiConfig.api.report.action.searchdetail.path,
        apiConfig.api.report.action.searchdetail.method,
        { 'jsonData': JSON.stringify(obj) }, 'fnSearchSuccessDetail', 'msgError');
}
function fnSearchSuccessDetail(rspn) {
    showLoading();
    var tbBody = $('#reportTableDetail tbody');
    $("#reportTableDetail").dataTable().fnDestroy();
    tbBody.html('');
    if (rspn !== undefined && rspn !== null) {
        for (var i = 0; i < rspn.data.length; i++) {
            var obj = rspn.data[i];
            var html = '<tr>' +
                '<td class="text-center"></td>' +
                '<td>' + (obj.description != null ? obj.description : "") + '</td>' +
                '<td>' + (obj.department != null ? obj.department : "") + '</td>' +
                '<td>' + (obj.name != null ? obj.name : "") + '</td>' +
                '<td>' + (obj.job_number != null ? obj.job_number : "") + '</td>' +
                '<td>' + (obj.email != null ? obj.email : "") + '</td>' +
                '<td>' + (obj.phone != null ? obj.phone : "") + '</td>' +
                '<td>' + (obj.time != null ? obj.time : "") + '</td>' +
                '<td>' + (obj.timeaccuracy != null ? obj.timeaccuracy : "") + '</td>' +
                '<td>' + (obj.camera_position != null ? obj.camera_position : "") + '</td>' +
                '</tr>';
            tbBody.append(html);
        }
        var page_size = (parseInt($("#txtCurrentPageDetail").val()) - 1) * parseInt($("#cbPageSizeDetail").val())
        var t = $("#reportTableDetail").DataTable({
            "bPaginate": false,
            "bLengthChange": false,
            "bFilter": false,
            "bInfo": false,
            "columnDefs": [
                {
                    "targets": 0,
                    "className": "text-center",
                    "orderable": false,
                    "data": null,
                    "order": [],
                    render: function (data, type, row, meta) {

                        return meta.row + page_size + 1;
                    }
                },
                {
                    "targets": null,
                    "searchable": false,
                    "orderable": false
                }],
            "order": [],
            "drawCallback": function (settings) {
                $('[data-toggle="tooltip"]').tooltip();
            },
        });
        t.on('order.dt search.dt', function () {
            t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                cell.innerHTML = i + page_size + 1;
            });
        }).draw();
        reCalculatPagesCustomDetail(rspn.total);
        viewBtnActionPageDetail();
        hideLoading();
    } else if (rspn == "") {
        var tbBody = $('#reportTableDetail tbody');
        $("#reportTableDetail").dataTable().fnDestroy();
        tbBody.html('');

        var page_size = (parseInt($("#txtCurrentPageDetail").val()) - 1) * parseInt($("#cbPageSizeDetail").val())
        var t = $("#reportTableDetail").DataTable({
            "bPaginate": false,
            "bLengthChange": false,
            "bFilter": false,
            "bInfo": false,
            "columnDefs": [
                {
                    "targets": 0,
                    "className": "text-center",
                    "orderable": false,
                    "data": null,
                    "order": [],
                    render: function (data, type, row, meta) {

                        return meta.row + page_size + 1;
                    }
                },
                {
                    "targets": null,
                    "searchable": false,
                    "orderable": false
                }],
            "order": [],
            "drawCallback": function (settings) {
                $('[data-toggle="tooltip"]').tooltip();
            },
        });
        t.on('order.dt search.dt', function () {
            t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                cell.innerHTML = i + page_size + 1;
            });
        }).draw();
        reCalculatPagesCustomNull();
        hideLoading();
    }
}
//xuất excel

function ExportExcelDetail() {
    var obj = {
        'subject_id': parseInt($('#subjectId').val()),
        'page_size': parseInt($("#cbPageSizeDetail").val()),
        'start_number': (parseInt($("#txtCurrentPageDetail").val()) - 1) * parseInt($("#cbPageSizeDetail").val())
    }

    var jsonData = JSON.stringify(obj);

    var request = new XMLHttpRequest();
    request.responseType = "blob";
    request.open("GET", apiConfig.api.host_report_url + apiConfig.api.report.controller +
        apiConfig.api.report.action.exportdetail.path + "?jsonData=" + jsonData);
    //request.setRequestHeader('Authorization', getSessionToken());
    request.setRequestHeader('Accept-Language', 'vi-VN');
    request.onload = function () {
        if (this.status == 200) {
            var link = document.createElement('a');
            link.href = window.URL.createObjectURL(this.response);
            link.download = "ReportFileDetail.xlsx";
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        }
        //if (this.status == 404) {
        //    /*toastr.error("Không tìm thấy dữ liệu!", "Lỗi!", { progressBar: true });*/
        //    swal("Error!", "Không tìm thấy dữ liệu", "error");
        //}
        //if (this.status == 400) {
        //    //toastr.error("Có lỗi xảy ra!", "Lỗi!", { progressBar: true });
        //    swal("Error!", "Có lỗi trong quá trình xử lý!", "error");
        //}
    }
    request.send();
}