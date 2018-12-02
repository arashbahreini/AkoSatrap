﻿mainApp.lazy.controller('projectController', ['$rootScope', '$scope', 'invokeServerService', 'arrayHelperFactory', 'messageFactory', 'cfpLoadingBar', 'fileUploaderService'
   , function ($rootScope, $scope, invokeServerService, arrayHelperFactory, messageFactory, cfpLoadingBar, fileUploaderService) {

       $rootScope.pageTitle = 'مدیریت پروژه ها';

       $scope.persianPattern = /^[\u0600-\u06FF\s]+$/;
       $scope.percentagePattern = /^[1-9][0-9]?$|^100$/;
       $scope.englishPattern = /^[a-z\u0590-\u05fe\s]+$/i;
       $scope.numberPattern = /^\+?\d+$/;
       $scope.phonePattern = /^[0-9-]*$/;
       $scope.englishWithNumber = /^[a-zA-Z0-9]*$/;
       $scope.sitePattern = /https?:\/\/www\.[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,3}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)/;
       $scope.emailPattern = /^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/;
       $scope.isUpdate = false;

       setup_file_input();

       $scope.myPromise = invokeServerService.Post('/PProject/GetProjectCategoryListForDropDown', {}).success(function (result) {
           $scope.projectCategoryDataSource = result;
       });

       $scope.addProject = function () {
           $scope.myPromise = invokeServerService.Post('/PProject/AddProject', { project: $scope.project }).success(function (result) {
               if (result.Success) {
                   messageFactory.showAlert(result.Message, 'success');

                   $("#tabInsertUpdate").removeClass("active");
                   $("#navInsertUpdate").removeClass("active");

                   $("#tabAllProduct").addClass("active");
                   $("#navAllProduct").addClass("active");


                   $scope.getProjects();

                   $scope.project = null;

               }
               else {
                   messageFactory.showAlert(result.Message, 'danger');
               }
           });
       };

       $scope.updateProduct = function () {
           
           $scope.myPromise = invokeServerService.Post('/PProduct/UpdateProduct', { project: $scope.project }).success(function (result) {
               if (result.Success) {
                   messageFactory.showAlert(result.Message, 'success');
                   $scope.project = null;
                   $("#tabInsertUpdate").removeClass("active");
                   $("#navInsertUpdate").removeClass("active");

                   $("#tabAllProduct").addClass("active");
                   $("#navAllProduct").addClass("active");


                   $scope.getProjects();
               }
               else {
                   messageFactory.showAlert(result.Message, 'danger');
               }
           });
       };

       $scope.newProduct = function () {
           $scope.project = null;
           $scope.isUpdate = false;
       }

       $scope.confirmDelete = function () {

       };

       $scope.getProjects = function () {
           $scope.projects = [];
           $scope.myPromise = invokeServerService.Get('/PProject/GetProjectList').success(function (result) {
               if (result.Data) {
                   $scope.projects = result.Data;
               }
           })
       }
       $scope.getProjects();
      
       $scope.uploadFile = function () {
           $scope.myPromise = fileUploaderService.uploadFileToUrl($scope.fileAttachment, '/PProduct/AddImage', $scope.project.Id)
               .then(function (result) {
                   if (result.Success) {

                       messageFactory.showAlert(result.Message, 'success');
                       updateProductImage();

                   }
                   else {
                       messageFactory.showAlert(result.Message, 'success');
                   }
               }
               , function () {

               });
       };

       $scope.deletePhoto = function (photoName) {
           $scope.myPromise = invokeServerService.Post('/PProduct/DeleteImage', { imageFolderName: $scope.project.ImageFolderName, fileName: photoName }).success(function (result) {
               if (result.Success) {
                   updateProductImage();
               }
           });
       }

       function updateProductImage() {
           $scope.myPromise = invokeServerService.Post('/PProduct/GetProductImages', { imageFolderName: $scope.project.ImageFolderName }).success(function (result) {
               if (result.Success) {
                   $scope.project.Images = result.Data;
               }
           });
       }

   }]);