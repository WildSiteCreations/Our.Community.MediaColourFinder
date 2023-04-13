(function () {
    "use strict";

    function mediaColourFinderController($scope) {
        var vm = this;
        vm.model = $scope.model.value;
        console.log(vm.model);
    }

    angular.module("umbraco").controller('wsc.mediaColourFinderController',
        ['$scope', mediaColourFinderController]);
})();