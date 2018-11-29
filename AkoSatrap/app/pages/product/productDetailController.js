mainApp.lazy.controller('productCategoryController', ['$rootScope', '$scope', 'invokeServerService', 'arrayHelperFactory', 'messageFactory', 'cfpLoadingBar'
   , function ($rootScope, $scope, invokeServerService, arrayHelperFactory, messageFactory, cfpLoadingBar) {
       
       $rootScope.pageTitle = 'مدیریت جزییات محصولات';

       $scope.persianPattern = /^[\u0600-\u06FF\s]+$/;
       $scope.englishPattern = /^[a-z\u0590-\u05fe\s]+$/i;
       $scope.numberPattern = /^\+?\d+$/;
       $scope.phonePattern = /^[0-9-]*$/;
       $scope.englishWithNumber = /^[a-zA-Z0-9]*$/;
       $scope.sitePattern = /https?:\/\/www\.[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,3}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)/;
       $scope.emailPattern = /^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/;
       $scope.isUpdate = false;


       $scope.myPromise = invokeServerService.Post('/PProduct/GetProductListForDropDown', { }).success(function (result) {
           $scope.productDataSource = result;
           $scope.product = result[0];
           gridDataSource.read();
       });

       $scope.refreshGrid = function()
       {
           gridDataSource.read();   
       }

       $scope.addProductFeature = function () {
           $scope.productDetail.ProductId = $scope.product.Id;
           $scope.myPromise = invokeServerService.Post('/PProduct/AddProductFeature', { productFeature: $scope.productDetail }).success(function (result) {
               
               if (result.Success) {
                   messageFactory.showAlert(result.Message, 'success');

                   $("#tabInsertUpdate").removeClass("active");
                   $("#navInsertUpdate").removeClass("active");

                   $("#tabInformation").addClass("active");
                   $("#navInformation").addClass("active");


                   gridDataSource.read();

                   $scope.productDetail = null;
               }
               else {
                   messageFactory.showAlert(result.Message, 'danger');
               }
           });
       }

       $scope.updateProductFeature = function () {
           $scope.productDetail.ProductId = $scope.product.Id;
           $scope.myPromise = invokeServerService.Post('/PProduct/UpdateProductDetail', { productFeature: $scope.productDetail }).success(function (result) {
               
               if (result.Success) {
                   messageFactory.showAlert(result.Message, 'success');

                   $("#tabInsertUpdate").removeClass("active");
                   $("#navInsertUpdate").removeClass("active");

                   $("#tabInformation").addClass("active");
                   $("#navInformation").addClass("active");


                   gridDataSource.read();
                   $scope.isUpdate = false;
                   $scope.productDetail = null;
               }
               else {
                   messageFactory.showAlert(result.Message, 'danger');
               }
           });
       }

       $scope.newProductFeature = function () {
           $scope.productDetail = null;
           $scope.isUpdate = false;
       }

       var gridDataSource = new kendo.data.DataSource({
           transport: {
               read: "/PProduct/GetProductDetail",
               parameterMap: function (data, type) {
                   
                   if (type == "read") {
                       // send take as "$top" and skip as "$skip"                    
                       return {
                           //   documentDetail: angular.toJson($scope.document),
                           pageSize: data.pageSize,
                           page: data.page,
                           productId: $scope.product.Id

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

       $scope.gridOption = {
           dataSource: gridDataSource,
           sortable: true,
           //pageable: true,
           pageable: {
               messages: {
                   empty: "رکوردی وجود ندارد"
               }
           },
           autoBind:false,
           columns: [
               { field: "Id", hidden: true },
               { field: "Title", title: "عنوان جزییات", width: "240px" },
               { field: "EnTitle", title: "به لاتین", width: "240px" },
               //{
               //    command: {
               //        text: "حذف", click: function (e) {
               //            e.preventDefault();
               //            var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
               //            $scope.dataItem = dataItem;
               //            messageFactory.showConfirmModal("آیا برای حذف سیستمی مطمئن هستید؟", function () { $scope.confirmDelete(); });
               //            $scope.$apply();
               //        }
               //    }, title: " ", width: "80px"
               //},
               {
                   command: {
                       text: "ویرایش"
                       , click: function (e) {
                           e.preventDefault();
                           var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
                           $scope.productDetail = dataItem;
                           $("#tabInsertUpdate").addClass("active");
                           $("#navInsertUpdate").addClass("active");

                           $("#tabInformation").removeClass("active");
                           $("#navInformation").removeClass("active");

                           $scope.isUpdate = true;

                           $scope.$apply();
                       }
                   }, title: "بروزرسانی", width: "100px"
               }
           ]
       };
   }]);