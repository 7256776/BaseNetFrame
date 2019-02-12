 
    function doAction() {

        var url = $("#txtUrl").val();;
        var action = $("#selectAction").val();
         

        //http://localhost:50327/api/TestService/GetToJson
        //http://localhost:50327/api/TestService/PostToJson
        //http://localhost:50327/api/TestService/PutToJson
        //http://localhost:50327/api/TestService/DeleteToJson

        $.ajax({
            type: action,
            url: url,
            //dataType: 'json',
            //contentType: 'application/json'
            //dataType: "xml",
            //contentType: 'application/xml;charset=gb2312;'
        }).success(function (res) {
            console.log(res);
        }).error(function (xhr, status, e) {
            console.log(xhr);
        });

        //$.ajax({
        //    url: "http://localhost:53479/api/values/PostData",
        //    type: "POST",
        //    //headers: {
        //    //    "Authorization": "Bearer " + _this.abpFormData.token,
        //    //    "X-XSRF-TOKEN": _this.abpFormData.forgeToKen
        //    //},
        //    //dataType: "json",
        //    //contentType: 'application/json',
        //    complete: function (e, state, a) {
        //        debugger;
        //        var result = e.status;
        //    }
        //});


    }

 