mainApp.lazy.controller('driverController', ['$rootScope', '$scope', 'invokeServerService', 'arrayHelperFactory', 'messageFactory', 'cfpLoadingBar'
   , function ($rootScope, $scope, invokeServerService, arrayHelperFactory, messageFactory, cfpLoadingBar) {

       $rootScope.pageTitle = 'مدیریت سوخت رسان';

       $scope.persianPattern = /^[\u0600-\u06FF\s]+$/;
       $scope.englishPattern = /^[a-z\u0590-\u05fe]+$/i;
       $scope.numberPattern = /^\+?\d+$/;
       $scope.phonePattern = /^[0-9-]*$/;
       $scope.englishWithNumber = /^[a-zA-Z0-9]*$/;
       $scope.sitePattern = /https?:\/\/www\.[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,3}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)/;
       $scope.emailPattern = /^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/;


       $scope.searchDrivers = function () {
           driverGridDataSource.read();
       }

       $scope.saveDriver = function () {
           $scope.myPromise = invokeServerService.Post('/Driver/InsertDriver', { driver: $scope.driver }).success(function (result) {
               debugger;
               if (result.Success) {

               }
               else {

               }
           });
       };

       $scope.confirmDelete = function () {

       };

       $scope.newDriver = function () {

           $scope.driver = null;

           $("#tabInsertUpdate").addClass("active");
           $("#navInsertUpdate").addClass("active");

           $("#tabDriverInformation").removeClass("active");
           $("#navDriverInformation").removeClass("active");
       };
        
       var driverGridDataSource = new kendo.data.DataSource({
           transport: {
               read: "/Driver/GetAll",
               parameterMap: function (data, type) {
                   debugger;
                   if (type == "read") {
                       // send take as "$top" and skip as "$skip"                    
                       return {
                           //   documentDetail: angular.toJson($scope.document),
                           pageSize: data.pageSize,
                           page: data.page
                       }
                   }
               }
           },
           schema: {
               total: function (response) {
                   debugger;
                   return response.Total; // total is returned in the "total" field of the response
               },
               data: function (response) {
                   debugger;
                   return response.Data; // total is returned in the "total" field of the response
               }
           },
           pageSize: 5,
           serverPaging: true,
           serverSorting: true
       });

       $scope.driverGridOption = {
           dataSource: driverGridDataSource,
           sortable: true,
           //pageable: true,
           pageable: {
               messages: {
                   empty: "رکوردی وجود ندارد"
               }
           },
          // scrollable: true,
           //dataBound: function () {
           //    this.expandRow(this.tbody.find("tr.k-master-row"));
           //},
           //dataBound: function () {
           //    debugger;
           //    var data = this.dataSource.data();
           //    $(data).each(function (i, row) {
           //        if (row.get("temporaryData") == true && row.get("hasError") == true) {
           //            var element = $('tr[data-uid="' + row.uid + '"] ');
           //            element.addClass("error-row");
           //        }
           //    });
           //},
           columns: [
               { field: "Id", hidden: true },
               { field: "vFirstName", title: "نام", width: "80px" },
               { field: "vLastName", title: "نام خانوادگی", width: "110px" },
               { field: "National_Code", title: "کد ملی", width: "80px" },
               { field: "vMobileNumber", title: "شماره موبایل", width: "100px" },
               { field: "iVehicleTypeId", title: "نوع سوخت رسان", width: "80px" },
               { field: "iOperatingCityId", title: "شهر", width: "80px" },
               { field: "vVehicleMakeModel", title: "مدل سوخت رسان", width: "120px" },
               { field: "vVehicleRegistrationNumber", title: "پلاک", width: "150px" },
               { field: "eMobileVerify", title: "تایید موبایل", width: "80px" },
               { field: "eAutoLogin", title: "ورود خودکار", width: "80px" },
               { field: "eDocumentVerify", title: "تایید مدارک", width: "80px" },
               { field: "eVehicleVerify", title: "تایید وسایل نقلیه", width: "80px" },
               { field: "vDeviceToken", title: "توکن", width: "80px" },
               { field: "eGoOnline", title: "سرویس دهی", width: "80px" },
               { field: "eAvailability", title: "در دسترس", width: "80px" },
               { field: "eStatus", title: "وضعیت", width: "80px" },
               { field: "app_type", title: "نوع موبایل", width: "80px" },
               { field: "app_version", title: "ورژن", width: "80px" },
               {
                   command: {
                       text: "حذف", click: function (e) {
                           e.preventDefault();
                           var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
                           $scope.dataItem = dataItem;
                           messageFactory.showConfirmModal("آیا برای حذف سیستمی مطمئن هستید؟", function () { $scope.confirmDelete(); });
                           $scope.$apply();
                       }
                   }, title: " ", width: "80px"
               },
               {
                   command: {
                       text: "ویرایش"
                       , click: function (e) {
                           e.preventDefault();
                           var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
                           $scope.driver = dataItem;
                           $("#tabInsertUpdate").addClass("active");
                           $("#navInsertUpdate").addClass("active");

                           $("#tabDriverInformation").removeClass("active");
                           $("#navDriverInformation").removeClass("active");
                           $scope.$apply();
                       }
                   }, title: "بروزرسانی", width: "100px", locked: true
               }
           ]
       };
   }]);