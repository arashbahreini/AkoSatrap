mainApp.directive('dateP', function () {
    return {
        restrict: 'A',
        require: 'ngModel',

        link: function (scope, element, attr, ngModel) {            
            element.datepicker({
                format: 'yyyy/mm/dd',
                todayBtn: true,
                todayBtn: true,
                autoclose: false
            });
            element.next().bind('click', function () {
                debugger;
                element.datepicker('show');
            })
        }
    }
});