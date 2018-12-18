mainApp.service('fileUploaderService', ["$q", "$http", function ($q, $http) {

    this.uploadFileToUrl = function (data, url,id) {
        
        var deferred = $q.defer();
        var param = getModelAsFormData(data)// { fileAttachment: getModelAsFormData(data) }
        param.append('Id', id);
        $http({
            url: url,
            method: "POST",
            data: param,
            transformRequest: angular.identity,
            headers: { "Content-Type": undefined }
        }).success(function (data, status) {
            
            deferred.resolve(data);

        }).error(function (result, status) {
            
            //deferred.reject(status);
            if (status == 401 || status == 400) {
                var pageUrl = result.Url;
                var loginUrl = result.LoginUrl;
                window.location = loginUrl;
                event.stopImmediatePropagation();
                return;
            }
            return 'error';
        });

        return deferred.promise;
    }

    var getModelAsFormData = function (data) {
        
        var dataAsFormData = new FormData();
        angular.forEach(data, function (value, key) {
            dataAsFormData.append(key, value);
        });
        return dataAsFormData;
    };
}]);
