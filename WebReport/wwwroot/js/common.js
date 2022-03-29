String.prototype.isBlank = function () {
    if (this == undefined)
        return true;
    var result = this.trim();
    if (result.length > 0) {
        result = false;
    } else {
        result = true;
    }
    return result
}

function viewValue(val) {
    return val == undefined || val == null ? '' : val;
}
String.prototype.strStatus = function () {
    if (this == undefined)
        return localizationResources.InActive;
    var result = this.trim();
    if (result == 1 || result == true || result == 'true') {
        result = localizationResources.Active;
    } else {
        result = localizationResources.InActive;
    }
    return result
}
Boolean.prototype.strStatus = function () {
    if (this == undefined)
        return localizationResources.InActive;
    var result = localizationResources.InActive;
    if (this == true || this == 1) {
        result = localizationResources.Active;
    } else {
        result = localizationResources.InActive;
    }
    return result
}

function msgError(respon, status, error) {
    if (respon.status == 401)
        swal("Unauthorized!", localizationResources.Error401, "warning");
    else if (respon.status == 404)
        swal("Not found!", localizationResources.Error404, "warning");
    else
        swal(error + "!", localizationResources.Error500, "error");
}

function formatNumberByLocate(value) {
    if (value == undefined || value == null || value == "")
        return "0";
    return Number(value).toLocaleString('vi')
}

function rowNo(page, pageSize, i) {
    page = parseInt(page);
    pageSize = parseInt(pageSize);
    i = parseInt(i);
    if (isNaN(page) || isNaN(pageSize) || isNaN(i))
        return 0;
    return (page - 1) * pageSize + i + 1;
}

function btnOnSearch() {
    $("#txtCurrentPage").val(1);
    onSearch();
}


//function showLoading() {
//    $('#preloader').css('display', 'block');
//}
//function hideLoading() {
//    setTimeout(function () {
//        var keys = Object.keys(localStorage);
//        var isOk = true;
//        for (var i = 0; i < keys.length; i++) {
//            if (keys[i].startsWith('loading'))
//                isOk = false;
//        }
//        if (isOk)
//            $('#preloader').css('display', 'none');
//    }, 500);
//}
//function fakeValue(value) {
//    if (value == undefined || value == null || value == '')
//        return 0;
//    return value;
//}

function clearMsgInvalid() {
    var inputInvalid = $('.is-invalid');
    var labelInvalid = $('.invalid-feedback');
    for (var i = 0; i < inputInvalid.length; i++) {
        $(inputInvalid[i]).removeClass('is-invalid');
    }
    for (var i = 0; i < labelInvalid.length; i++) {
        $(labelInvalid[i]).remove();
    }
}

function validateRequired(parent) {
    parent = !parent ? '' : parent;
    var allRequired = $(parent + ' .required');
    var isValid = true;
    var itemFocus = null;
    for (var i = 0; i < allRequired.length; i++) {
        var grInput = $(allRequired[i]).parent().find('input');
        var grSelect = $(allRequired[i]).parent().find('select');
        var grTextarea = $(allRequired[i]).parent().find('textarea');

        var msg = $(allRequired[i]).parent().find('label').text() + ' ' + localizationResources.CanNotNull;
        $(allRequired[i]).parent().find('.invalid-feedback').remove();

        if (grInput.length > 0 || grSelect.length > 0 || grTextarea.length > 0) {
            var eleWork = grInput.length > 0 ? grInput : grSelect.length > 0 ? grSelect : grTextarea;
            eleWork.removeClass('is-invalid');

            var val = eleWork.val();

            if (val == undefined || val == null || val.isBlank()) {
                itemFocus = itemFocus == null ? $(allRequired[i]) : itemFocus;
                eleWork.addClass('is-invalid');
                $(eleWork).parent().append('<div class="invalid-feedback">' + msg + '</div>')
                isValid = false;
            }
        }
    }
    if (!isValid)
        itemFocus.focus();
    return isValid;
}

function onFocus(parent) {
    parent = !parent ? 'body' : parent;
    var grInput = $(parent).find('input:not(:hidden)');
    var grSelect = $(parent).find('select');
    var grTextarea = $(parent).find('textarea');
    var eleWork = grInput.length > 0 ? grInput : grSelect.length > 0 ? grSelect : grTextarea;
    console.log(eleWork[0]);
    $(eleWork[0]).focus();
}

function collapseDelegate() {
    $('.btn-collapse').on('click', function (ele) {
        var target = ele.currentTarget.dataset["target"];

        if (target != undefined && target != null && target != '') {
            var clsList = ele.currentTarget.classList;
            if (clsList.contains('shown')) {
                ele.currentTarget.classList.remove('shown');
                ele.currentTarget.classList.add('hidden');
                $(target).hide();
                if (ele.currentTarget.childElementCount > 0)
                    ele.currentTarget.childNodes[0].classList.value = 'fas fa-plus';
            }
            else {
                ele.currentTarget.classList.remove('hidden');
                ele.currentTarget.classList.add('shown');
                $(target).show();
                if (ele.currentTarget.childElementCount > 0)
                    ele.currentTarget.childNodes[0].classList.value = 'fas fa-minus';


                var btn = $(ele.currentTarget.dataset["target"]).find('button.hidden');
                if (btn.length > 0) {
                    $(target).show();

                    for (var i = 0; i < btn.length; i++) {
                        var tg = btn[i].dataset['target'];
                        $(tg).hide();
                    }
                }
                else $(target).show();

            }
        }
    });
}

function onBack() {
    var modal = $('#card-update');
    var cardManag = $('#card-index');

    var headerManager = $('#header-manager');
    var headerCreate = $('#header-create');
    var headerUpdate = $('#header-update');
    var headerDetail = $('#header-detail');
    var headerImport = $('#header-import');

    modal.hide();
    cardManag.show();

    headerManager.show();
    headerCreate.hide();
    headerUpdate.hide();
    headerDetail.hide();
    headerImport.hide();
    localStorage[keyCurrentStage] = 'manager';
    location.hash = '';
}

function generateComboOptions(data, n, prop, extenal, idField, nameField) {
    var html = '';
    var nbsp = '';
    for (var i = 0; i < n; i++) {
        nbsp += '-&nbsp';
    }
    idField = idField ? idField : 'id';
    nameField = nameField ? nameField : 'name';
    if (data != undefined && data != null && data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            var obj = data[i];
            var cls = obj[prop] == undefined || obj[prop] == null || obj[prop].length == 0 ? '' : ' class="font-weight-bold" ';
            var dataExtenal = '';
            if (extenal && obj[extenal]) {
                dataExtenal += ' data-' + extenal + '="' + obj[extenal] + '" ';
            }
            html += '<option ' + dataExtenal + cls + ' value="' + obj[idField] + '">' + nbsp + obj[nameField] + '</option>';
            html += generateComboOptions(obj[prop], n + 1, prop, extenal, idField, nameField);
        }
    }
    return html;
}

function getStatusCode(code) {
    var val = localizationResources['Error' + code];
    return val ? val : code;
}
function getOptionValue(opt) {
    if (opt == undefined || opt == null || opt.name == undefined || opt.name == null)
        return '';
    return opt.name;
}
function getScoreMethod(opt) {
    if (opt == 0) return localizationResources.Automation;
    return opt == 1 ? localizationResources.Manual : "";
}
function updateFail(request, status, error) {
    swal("Error!", localizationResources.SaveFail, "error");
}
function getCode(obj) {
    return typeof (obj) != 'object' || obj == undefined || obj == null || obj == '' ? '' : obj.code;
}
function getName(obj) {
    return typeof (obj) != 'object' || obj == undefined || obj == null || obj == '' ? '' : obj.name;
}
function getIssueLevel(val) {
    return val == 1 ? localizationResources.Low : val == 2 ? localizationResources.Mid : val == 3 ? localizationResources.High : '';
}
function getIssueRange(min, max, minF, maxF) {

    var minStr = '';
    switch (minF) {
        case 'gte': minStr = '<=';
            break;
        case 'gt': minStr = '<';
            break;
        default: minStr = '';
            break;
    }
    var maxStr = '';
    switch (maxF) {
        case 'lte': maxStr = '<=';
            break;
        case 'lt': maxStr = '<';
            break;
        default: maxStr = '';
            break;
    }

    if (minStr == '' && maxStr == '')
        return '';

    return (minStr != '' ? (min + ' ' + minStr + ' ') : '') + localizationResources.Point + (maxStr != '' ? (' ' + maxStr + ' ' + max) : '');
}
function checkNumberValue(ele, compareWith, formula) {
    if (!ele || ele.type != 'number')
        return;
    var vl = parseFloat(ele.value);
    var min = parseFloat(ele.min);
    var max = parseFloat(ele.max);
    if (ele.min) {
        if (vl < min) {
            swal('Error', localizationResources.ValueInvalid, 'error');
            ele.value = min;
            return;
        }
    }
    if (ele.max) {
        if (vl > max) {
            swal('Error', localizationResources.ValueInvalid, 'error');
            ele.value = max;
            return;
        }
    }
    if (ele.max && ele.min && (vl > max || vl < min)) {
        swal('Error', localizationResources.ValueInvalid, 'error');
        ele.value = '';
        return;
    }
    if (compareWith && formula) {
        var compareValue = $(compareWith).val();
        var cp = parseFloat(compareValue);
        if (compareValue && !isNaN(cp)) {
            if (formula == 'gt' && vl < cp) {
                swal('Error', localizationResources.ValueInvalid, 'error');
                ele.value = '';
                return;
            }
            if (formula == 'lt' && vl > cp) {
                swal('Error', localizationResources.ValueInvalid, 'error');
                ele.value = '';
                return;
            }
        }
    }
}
function IsCheckPemission(menucode, permission_code) {
    if (localStorage["CurrentPermission"] != null && localStorage["CurrentPermission"] != undefined) {
        var list_permission = JSON.parse(localStorage["CurrentPermission"]);
        var _permission = menucode + "_" + permission_code;
        var ischeck = list_permission.some(el => el.permission === _permission);
        return ischeck;
    }
    return false;
}
function getAssessmentStage(val) {
    return val == 1 ? localizationResources.Year : val == 2 ? localizationResources.HalfYear : val == 3 ? localizationResources.Quarter : '';
}
function getAssessmentState(val) {
    return !val || val == 0 ? localizationResources.InProcess : localizationResources.Completed;
}
function getProcessState(val) {
    return val == -1 || val == null || val == undefined ? localizationResources.NotStart : val == 0 ? localizationResources.InProcess : localizationResources.Completed;
}
function getAssessmentPullState(val) {
    return localizationResources.InComplete;
}
function getAssessmentPullLast(val) {
    return '';
}

function getImportStatus(code) {
    code = (code + '').trim();
    if (!code || code == null || code == '')
        return '';
    var val = localizationResources['Import' + code];
    return val ? val : code;
}
function getImportResult(note) {
    if (!note || note == null || note == '')
        return '';
    var spt = note.split(',');

    var str = '';
    for (var i = 0; i < spt.length; i++) {
        var code = spt[i].trim();
        str += getImportStatus(code) + '<br/>';
    }
    return str;
}


function ResetPageSize() {
    $("#txtCurrentPage").val(1);
    reCalculatPagesCustom(0);
    viewBtnActionPage();
}
function checkFilesize(files) {

    var result = false;
    if (localStorage["SystemParam"] != null && localStorage["SystemParam"] != undefined) {
        var list_param = JSON.parse(localStorage["SystemParam"]);
        var param = list_param.find(el => el.name === "FILESIZE");
        var filesize = parseInt(param.value);
        const k = 1024;
        const dm = 2;
        var convert_size = files.size / Math.pow(k, dm);
        if (convert_size > filesize) {
            result = true;
        }
    }
    return result;
}
function getFilesizeSystem() {
    var _size = "0 MB";
    if (localStorage["SystemParam"] != null && localStorage["SystemParam"] != undefined) {
        var list_param = JSON.parse(localStorage["SystemParam"]);
        var param = list_param.find(el => el.name === "FILESIZE");
        _size = param.value + " MB";
    }
    return _size;
}


//$.getScript('/plugins/jquery-validation/jquery.validate.min.js', function () {
//    $("#frmRequestModal").validate({
//        rules: {
//            approver: { required: true },
//        },
//        submitHandler: function () {
//            var id = $("#frmRequestModal").find("#item_id").val();
//            var approvaluser = $("#frmRequestModal").find("#approver").val();
//            var function_code = $("#frmRequestModal").find("#function_code").val();
//            var function_name = $("#frmRequestModal").find("#function_name").val();
//            var obj = {
//                'item_id': id,
//                'approvaluser': approvaluser,
//                'function_name': function_name,
//                'function_code': function_code,
//            }
//            callApi_userservice(
//                apiConfig.api.approvalfunction.controller,
//                apiConfig.api.approvalfunction.action.requestapproval.path,
//                apiConfig.api.approvalfunction.action.requestapproval.method,
//                obj, 'fnRequestApprovalSuccess');
//            var obj_log = {
//                'item_id': parseInt(id),
//                'item_type': getTypeLogKitano(function_code),
//                'type': 'Gửi duyệt',
//                'content': 'Đã gửi duyệt',
//                'version': $("#formDetail").find("#versionDetail").val(),
//            }
//            callApi_auditservice(
//                apiConfig.api.discussionhistory.controller,
//                apiConfig.api.discussionhistory.action.savediscussionhistory.path,
//                apiConfig.api.discussionhistory.action.savediscussionhistory.method,
//                obj_log, '', 'msgError');
            
//        }
//    });
//    $("#frmRejectModal").validate({
//        rules: {
//            reasonnote: { required: true }
//        },
//        submitHandler: function () {
//            var id = $("#frmRejectModal").find("#item_id").val();
//            var reasonnote = $("#frmRejectModal").find("#reasonnote").val();
//            var function_code = $("#frmRejectModal").find("#function_code").val();
//            var function_name = $("#frmRejectModal").find("#function_name").val();
//            var obj = {
//                'item_id': id,
//                'function_name': function_name,
//                'function_code': function_code,
//                'reason_note': reasonnote,
//            }
//            callApi_userservice(
//                apiConfig.api.approvalfunction.controller,
//                apiConfig.api.approvalfunction.action.rejectapproval.path,
//                apiConfig.api.approvalfunction.action.rejectapproval.method,
//                obj, 'fnRejectApprovalSuccess');
//            var obj_log = {
//                'item_id': parseInt(id),
//                'item_type': getTypeLogKitano(function_code),
//                'type': 'Từ chối phê duyệt',
//                'content': reasonnote,
//                'version': $("#formDetail").find("#versionDetail").val(),
//            }
//            callApi_auditservice(
//                apiConfig.api.discussionhistory.controller,
//                apiConfig.api.discussionhistory.action.savediscussionhistory.path,
//                apiConfig.api.discussionhistory.action.savediscussionhistory.method,
//                obj_log, '', 'msgError');
//        }
//    });
//    $("#frmApprovalRequestModal").validate({
//        rules: {
//            approver: { required: true },
//        },
//        submitHandler: function () {
//            var id = $("#frmApprovalRequestModal").find("#item_id").val();
//            var approvaluser = $("#frmApprovalRequestModal").find("#approver_level").val();
//            var function_code = $("#frmApprovalRequestModal").find("#function_code").val();
//            var function_name = $("#frmApprovalRequestModal").find("#function_name").val();
//            var year = $("#frmApprovalRequestModal").find("#year").val();
//            var obj = {
//                'item_id': id,
//                'approvaluser': approvaluser,
//                'function_name': function_name,
//                'function_code': function_code,
//                'year': year
//            }
//            callApi_userservice(
//                apiConfig.api.approvalfunction.controller,
//                apiConfig.api.approvalfunction.action.submitapproval.path,
//                apiConfig.api.approvalfunction.action.submitapproval.method,
//                obj, 'fnApprovalSuccess');

//            var obj_log = {
//                'item_id': id,
//                'item_type': getTypeLogKitano(function_code),
//                'type': 'Duyệt',
//                'content': 'Đã duyệt',
//                'version': $("#formDetail").find("#versionDetail").val(),
//            }
//            callApi_auditservice(
//                apiConfig.api.discussionhistory.controller,
//                apiConfig.api.discussionhistory.action.savediscussionhistory.path,
//                apiConfig.api.discussionhistory.action.savediscussionhistory.method,
//                obj_log, '', 'msgError');
//        }
//    });
//});

