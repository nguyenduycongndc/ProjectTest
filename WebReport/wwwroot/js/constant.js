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
                    //function support get items by search condition
                    "method": "GET",
                    "path": "/Search"
                },
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