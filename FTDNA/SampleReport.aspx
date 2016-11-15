<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SampleReport.aspx.cs" Inherits="FTDNA.SampleReport" %>

<!DOCTYPE html>

<html data-ng-app="myApp">
<head runat="server">
    <style>  
        .gridStyle  
        {  
            border: 5px solid #d4d4d4;  
            height: 500px;  
        }

    </style>  
    <link href="Content/ng-grid.css" rel="stylesheet" />
    <script src="scripts/jquery-1.9.1.min.js"></script>
    <script src="scripts/angular.js"></script>
    <script src="scripts/ng-grid.js"></script>
    <script src="scripts/ng-grid.min.js"></script>
     <script type="text/javascript">  
         var app = angular.module('myApp', ['ngGrid']);
         app.controller('MyCtrl', function ($scope,$http) {
             $scope.myData = [];
             $scope.gridOptions = { data: 'myData' };

             $scope.GetAllSamples = function () {
                 $http.get('api/Ftdna/ReturnAllSample').
                    then(function (response) {
                        $scope.myData = response.data;
                    });
             };

             $scope.GetSampleByStatus = function () {
                 var status = $("#Status").val();
                 $http.get('api/Ftdna/SamplesWithStatus?status=' + status).
                    then(function (response) {
                        $scope.myData = response.data;
                    });
             };

             $scope.GetSamplesContainsUser = function () {
                 var token = $("#nameToken").val();
                 $http.get('api/Ftdna/SamplesContainsUser?searchToken=' + token).
                    then(function (response) {
                        $scope.myData = response.data;
                    });
             };

             $scope.CreateNewSample = function () {
                 var barCode = $("#newBarcode").val();
                 var dateCreated = $("#dateCreated").val();
                 var userId = $("#userId").val();
                 var statusId = $("#statusId").val();
                 $http.post('api/Ftdna/CreateNewSample?barcode=' + barCode + '&createdAt=' + dateCreated + '&createBy=' + userId + '&statusId=' + statusId)
                    .success(function(status){alert("You have added a new sample, Run the first Test to view the whole sample info");})
                    .error(function(status){alert("Error. Check your input please")});
             };

         });

         function datePicker($scope) {
             $scope.date = new Date();
         }

         function userCreated($scope, $http) {
             $http.get('api/Ftdna/getAllUser').
                    then(function (response) {
                        $scope.users = response.data;
                    });
         }

         function newStatus($scope, $http) {
             $http.get('api/Ftdna/getAllStatuses').
                    then(function (response) {
                        $scope.allStatus = response.data;
                    });
         }

    </script> 
    <title></title>
</head>
<body data-ng-controller="MyCtrl"> 
    <div>
        <label> Test Web API 1: Click to get all sample data </label>
        <button name="GetAllSamples" data-ng-click="GetAllSamples()">Get All Samples</button>
    </div>
    <div>
        <label> Test Web API 2: Search sample data by status </label>
        <select id="Status">
            <option value="" ></option>
            <option value="Received">Received</option>
            <option value="Accessioning">Accessioning</option>
            <option value="In Lab">In Lab</option>
            <option value="Report Generation">Report Generation</option>
        </select>
        <button name="GetSampleByStatus" data-ng-click="GetSampleByStatus()">Get Samples</button>
    </div>
    <div>
        <label> Test Web API 3: Search sample data created by (Name contain string)</label>
        <input type="text" id="nameToken" />
        <button name="GetSampleContainsUser"  data-ng-click="GetSamplesContainsUser()">Get Samples</button>
    </div>
    <div>
        <label> Test Web API 4: Create New Sample   </label>
    </div>
    <div>
        <label>Barcode</label>
        <input type="text" id="newBarcode"  />
    </div>
    <div data-ng-controller="datePicker"> 
        <label>Created Time</label>   
        <input id="dateCreated" type="date" data-ng-model="date" value="{{ date | date: 'yyyy-MM-dd' }}" />    
    </div> 
    <div data-ng-controller="userCreated">
        <label>Created By</label>
        <select id="userId" data-ng-model="createdBy" data-ng-options="name.UserId as name.FirstName +' '+ name.LastName for name in users"></select>
    </div>

     <div data-ng-controller="newStatus">
        <label>Status</label>
        <select id="statusId" data-ng-model="status" data-ng-options="status.StatusId as status.Status for status in allStatus"></select>
    </div>
    <div>
         <button name="SubmitNew"  data-ng-click="CreateNewSample()">Submit</button>
    </div>
    <p style="font-size:large;text-align:center;">Sample Result</p>
    <div class="gridStyle" data-ng-grid="gridOptions"></div>  
</body> 
</html>
