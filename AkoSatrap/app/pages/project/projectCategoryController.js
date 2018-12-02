mainApp.lazy.controller('productCategoryController', ['$rootScope', '$scope', 'invokeServerService', 'arrayHelperFactory', 'messageFactory', 'cfpLoadingBar'
   , function ($rootScope, $scope, invokeServerService, arrayHelperFactory, messageFactory, cfpLoadingBar) {
       
       $rootScope.pageTitle = 'مدیریت دسته بندی پروژه ها';

       $scope.persianPattern = /^[\u0600-\u06FF\s]+$/;
       $scope.englishPattern = /^[a-z\u0590-\u05fe\s]+$/i;
       $scope.numberPattern = /^\+?\d+$/;
       $scope.phonePattern = /^[0-9-]*$/;
       $scope.englishWithNumber = /^[a-zA-Z0-9]*$/;
       $scope.sitePattern = /https?:\/\/www\.[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,3}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)/;
       $scope.emailPattern = /^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/;
       $scope.isUpdate = false;

       

       $scope.addProjectCategory = function () {
           $scope.myPromise = invokeServerService.Post('/PProject/AddProjectCategory', { model: $scope.productCategory })
               .success(function (result) {
               if (result.Success) {
                   messageFactory.showAlert(result.Message, 'success');

                   $("#tabInsertUpdate").removeClass("active");
                   $("#navInsertUpdate").removeClass("active");

                   $("#tabRadiusTypeInformation").addClass("active");
                   $("#navRadiusTypeInformation").addClass("active");
                   gridDataSource.read();

                   $scope.productCategory = null;
               }
               else {
                   messageFactory.showAlert(result.Message, 'danger');
               }
           });
       }

       $scope.updateProjectCategory = function () {
           $scope.myPromise = invokeServerService.Post('/PProject/UpdateProjectCategory', { model: $scope.productCategory })
               .success(function (result) {
               if (result.Success) {
                   messageFactory.showAlert(result.Message, 'success');
                   $("#tabInsertUpdate").removeClass("active");
                   $("#navInsertUpdate").removeClass("active");
                   $("#tabRadiusTypeInformation").addClass("active");
                   $("#navRadiusTypeInformation").addClass("active");
                   gridDataSource.read();
                   $scope.isUpdate = false;
                   $scope.productCategory = null;
               }
               else {
                   messageFactory.showAlert(result.Message, 'danger');
               }
           });
       }

       $scope.newProductCategory = function()
       {
           $scope.productCategory = null;
           $scope.isUpdate = false;
       }
       
       $scope.confirmDelete = function () {

       };

       var gridDataSource = new kendo.data.DataSource({
           transport: {
               read: "/PProject/GetProjectCategoryList",
               parameterMap: function (data, type) {
                   
                   if (type === "read") {
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
                   
                   return response.Total; // total is returned in the "total" field of the response
               },
               data: function (response) {
                   
                   return response.Data; // total is returned in the "total" field of the response
               }
           },
           pageSize: 5,
           serverPaging: true,
           serverSorting: true
       });

       $scope.radiusGridOption = {
           dataSource: gridDataSource,
           sortable: true,
           //pageable: true,
           pageable: {
               messages: {
                   empty: "رکوردی وجود ندارد"
               }
           },
           columns: [
               { field: "Id", hidden: true },
               { field: "Title", title: "دسته بندی", width: "240px" },
               { field: "EnTitle", title: "به لاتین", width: "240px" },
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
                           $scope.productCategory = dataItem;
                           $("#tabInsertUpdate").addClass("active");
                           $("#navInsertUpdate").addClass("active");

                           $("#tabRadiusTypeInformation").removeClass("active");
                           $("#navRadiusTypeInformation").removeClass("active");

                           $scope.isUpdate = true;

                           $scope.$apply();
                       }
                   }, title: "بروزرسانی", width: "100px"
               }
           ]
       };
   }]);