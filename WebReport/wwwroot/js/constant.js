/// <reference path="host.js" />
var apiConfig = {
    "api": {
        "host_report_url": hostApi.host_report,
        //"authen": {
        //    "controller": "/GenToken",
        //    'action': {
        //        'token': {
        //            'method': 'POST',
        //            'path': ''
        //        }
        //    }
        //},
        "report": {
            "controller": "/Report",
            "action": {
                "search": {
                    "method": "GET",
                    "path": "/Search"
                },
                "exportexcel": {
                    "method": "GET",
                    "path": "/Export"
                },
                "searchdetail": {
                    "method": "GET",
                    "path": "/SearchDetail"
                },
                "exportdetail": {
                    "method": "GET",
                    "path": "/ExportDetail"
                }
            }
        },
    }
}


//var userInfo = {
//    'id': 1,
//    'user_name': 'admin',
//    'full_name': 'Administrator',
//    'domain_id': 1
//};