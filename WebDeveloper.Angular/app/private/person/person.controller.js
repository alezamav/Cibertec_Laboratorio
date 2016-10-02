(function () {
    var apiUrl = 'http://localhost/WebDeveloper.API/person/';
    'use strict';
    angular.module('app')
        .controller('personController', personController);

    personController.$inject = ['dataService'];

    function personController(dataService) {
        var vm = this;
        vm.title = 'Person Controller';
        vm.personList = [];
        vm.getPersonDetail = getPersonDetail;
        vm.person;
        init();

       
        function init() {
            loadData();
        
        }

        function loadData() {
            vm.personList = [];
            var url = apiUrl + 'list/1/15';
            dataService.getData(url)
                .then(function (result) {
                    vm.personList = result.data;
                },
                function (error) {
                    console.log(error);
                });
        }

        function getPersonDetail(id) {
            dataService.getData(apiUrl + id).then(
                function (result) {
                    vm.person = result.data;
                },
                function (error) {
                    vm.person = null;
                    console.log(error);
                });
        }

       
    }

})();