mainApp.lazy.controller('projectCategoryController', ['$rootScope', '$scope', 'invokeServerService', 'arrayHelperFactory', 'messageFactory', 'cfpLoadingBar'
    , function ($rootScope, $scope, invokeServerService, arrayHelperFactory, messageFactory, cfpLoadingBar) {

        $rootScope.pageTitle = 'مدیریت جزییات پروژه ها';

        $scope.persianPattern = /^[\u0600-\u06FF\s]+$/;
        $scope.englishPattern = /^[a-z\u0590-\u05fe\s]+$/i;
        $scope.numberPattern = /^\+?\d+$/;
        $scope.phonePattern = /^[0-9-]*$/;
        $scope.englishWithNumber = /^[a-zA-Z0-9]*$/;
        $scope.sitePattern = /https?:\/\/www\.[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,3}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)/;
        $scope.emailPattern = /^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/;
        $scope.isUpdate = false;


        $scope.myPromise = invokeServerService.Post('/PProject/GetProjectListForDropDown', {}).success(function (result) {
            $scope.projectDataSource = result;
            $scope.project = result[0];
        });

        $scope.addProjectFeature = function () {
            $scope.projectDetail.ProjectId = $scope.project.Id;
            $scope.myPromise = invokeServerService.Post('/PProject/AddProjectFeature', { projectFeature: $scope.projectDetail }).success(function (result) {

                if (result.Success) {
                    messageFactory.showAlert(result.Message, 'success');

                    $("#tabInsertUpdate").removeClass("active");
                    $("#navInsertUpdate").removeClass("active");

                    $("#tabInformation").addClass("active");
                    $("#navInformation").addClass("active");

                    $scope.projectDetail = null;
                }
                else {
                    messageFactory.showAlert(result.Message, 'danger');
                }
            });
        }

        $scope.updateProjectFeature = function () {
            $scope.projectDetail.ProjectId = $scope.project.Id;
            $scope.myPromise = invokeServerService.Post('/PProject/UpdateProjectDetail', { projectFeature: $scope.projectDetail }).success(function (result) {

                if (result.Success) {
                    messageFactory.showAlert(result.Message, 'success');

                    $("#tabInsertUpdate").removeClass("active");
                    $("#navInsertUpdate").removeClass("active");

                    $("#tabInformation").addClass("active");
                    $("#navInformation").addClass("active");

                    $scope.isUpdate = false;
                    $scope.projectDetail = null;
                }
                else {
                    messageFactory.showAlert(result.Message, 'danger');
                }
            });
        }

        $scope.newProjectFeature = function () {
            $scope.projectDetail = null;
            $scope.isUpdate = false;
        }

        $scope.getProjectDetails = function () {
            $scope.projectDetails = [];
            $scope.myPromise = invokeServerService.Post('/PProject/GetProjectDetail', { projectId: $scope.project.Id }).success(function (result) {
                if (result.Data) {
                    $scope.projectDetails = result.Data;
                }
            })
        }

        $scope.editProjectDetails = function (item) {
            $scope.projectDetail = item;
            $("#tabInsertUpdate").addClass("active");
            $("#navInsertUpdate").addClass("active");

            $("#tabInformation").removeClass("active");
            $("#navInformation").removeClass("active");

            $scope.isUpdate = true;
        }
    }]);