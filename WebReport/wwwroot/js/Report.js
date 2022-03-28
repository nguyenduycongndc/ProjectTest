function onSearch() {
    var obj = {
        'name': $('#Name').val().trim(),
        'department': $('#Department').val(),
        'branch': $('#Branch').val(),
        'todate': Date.parse($('#ToDate').val()),
        'fromdate': Date.parse($('#FromDate').val()),
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
        //$("#reportTable").dataTable().fnDestroy();
        tbBody.html('');
        for (var i = 0; i < rspn.length; i++) {
            var obj = rspn[i];
            var dateconvert = new Date(parseInt(obj.date + "000")).toLocaleDateString();
            var html = '<tr>' +
                //'<td class="text-center"></td>' +
                '<td>' + obj.branch + '</td>' +
                '<td>' + obj.department + '</td>' +
                '<td>' + obj.name + '</td>' +
                '<td>' + dateconvert + '</td>' +
                '<td class="text-center col-action">' +
                //mở lại comment khi có quyền
                '<a type="button" class="btn icon-delete btn-action-custom" onclick="Delete(' + obj.id + ')"><i data-toggle="tooltip" title="Xóa" class="fa fa-trash" aria-hidden="true"></i></a>' +
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
                    "targets": [0, 3, 4],
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
        //$("#reportTable").dataTable().fnDestroy();
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
                    "targets": [0, 3, 4],
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
