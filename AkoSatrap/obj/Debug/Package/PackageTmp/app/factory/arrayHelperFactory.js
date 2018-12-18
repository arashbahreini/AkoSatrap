mainApp.factory('arrayHelperFactory', function () {

    var serviceObj = {};

    serviceObj.GetRowById = function (list, id) {
        var rows = $.grep(list, function (e) { return e.Id == id });
        if (rows.length > 0) {
            return rows[0];
        }
        else {
            return -1;
        }
    };

    serviceObj.GetRowByPropertyName = function (arrayList, searchValue, property) {
        var rows = $.grep(arrayList, function (e) { return e[property] == searchValue });
        if (rows.length > 0) {
            return rows[0];
        }
        else {
            return -1;
        }
    };

    serviceObj.GetIndexOfArray = function (arrayList, searchValue, property) {
        for (var i = 0, len = arrayList.length; i < len; i++) {
            if (arrayList[i][property] === searchValue) return i;
        }
        return -1;
    };

    return serviceObj;
});