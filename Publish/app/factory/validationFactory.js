mainApp.factory('validationFactory', function () {

    var serviceObj = {};

    serviceObj.setValidationState = function (objectId, state, message) {
        ui_validation_state(objectId, state, message);
    };
    return serviceObj;
});