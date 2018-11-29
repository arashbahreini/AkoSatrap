mainApp.service('invokeServerService', function ($http) {
    this.Post = function (url, dataParameter) {
        return $http({
            method: "POST",
            url: url,
            dataType: 'json',
            data: dataParameter,
            headers: { "Content-Type": "application/json" }
        }).success(function (data, status) {
            debugger;
            return data;
        }).error(function (data, status) {
            debugger;
            //$.globalEval(data);
            if (status == 401 || status == 400) {
                var pageUrl = data.Url;
                var loginUrl = data.LoginUrl;
                window.location = loginUrl;
                event.stopImmediatePropagation();
                return;
            }
            return 'error';
        });
    }

    this.Get = function (url, dataParameter) {
        return $http({
            method: "GET",
            url: url,
            dataType: 'json',
            data: dataParameter,
            headers: { "Content-Type": "application/json" }
        }).success(function (data, status) {
            return data;
        }).error(function (data, status) {
            $.globalEval(data);
            return 'error';
        });
    }

});
