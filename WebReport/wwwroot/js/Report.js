function onSearch() {
    var obj = {
        'name': $('#Name').val().trim(),
        'department': $('#Department').val(),
        'branch': $('#Branch').val(),
        //'page_size': parseInt($("#cbPageSize").val()),
        //'start_number': (parseInt($("#txtCurrentPage").val()) - 1) * parseInt($("#cbPageSize").val())
    }
    callApi_report(
        apiConfig.api.report.controller,
        apiConfig.api.report.action.search.path,
        apiConfig.api.report.action.search.method,
        { 'jsonData': JSON.stringify(obj) }, 'fnSearchUnitTypeSuccess', 'msgError');
}