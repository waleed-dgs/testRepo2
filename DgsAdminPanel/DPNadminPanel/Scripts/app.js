var myApp = angular.module('myApp', ['ngSanitize', 'ngCookies']);

myApp.controller('MyController', ['$scope', '$http', '$cookieStore', function ($scope, $http, $cookieStore) {

    $scope.clearJsonObj = function () {
        var data = JSON.stringify("");
        $http.post('/Widgets/SaveLayout', JSON.stringify({ jsonObj: data }));
    }

    $scope.WidgetSelected = function (url, obj) {
        var url = $("#" + url + "1").val();
        document.getElementById('myModalWidget').style.display = 'none';
        $http.get(url).then(function (response) {
            $('#column1').prepend(response.data);
            $scope.storeJson();
            iNettuts.newInit();
        });
    }
    
    $scope.LayoutSelected = function (url, obj) {
        var url = $("#" + url + "1").val();
        document.getElementById('myModal').style.display = 'none';
        $http.get(url).then(function (response) {
            $('#columns').empty();
            $('#columns').append(response.data);
            $scope.loadJsonOnLayoutChange(true);
        });
    }

    $scope.recursiveGetReqForWidget = function (i, jsonObj, layoutChange) {
        debugger;
        var widgetID, widgetPath;
        var columnID = "column1";
        var colorClass, collapseState, name, length, collapseLogo;

        if (i < jsonObj.widget.length) {

            widgetID = jsonObj.widget[i].id;
            widgetPath =  $("#" + widgetID + "1").val();
            if (layoutChange == false) {
                columnID = jsonObj.widget[i].parentid;
            }
            colorClass = jsonObj.widget[i].color;
            collapseState = jsonObj.widget[i].collapseState;
            name = jsonObj.widget[i].name;
            collapseLogo = jsonObj.widget[i].collapseLogo;
            $http.get(widgetPath).then(function (response) {

                $('#' + columnID).append(response.data);
                length = document.getElementById(columnID).children.length;
                document.getElementById(columnID).children[length - 1].className = colorClass;
                document.getElementById(columnID).children[length - 1].getElementsByTagName("h3")[0].innerHTML = name;
                iNettuts.newInit();
                document.getElementById(columnID).children[length - 1].getElementsByClassName("widget-content")[0].style.display = collapseState;
                document.getElementById(columnID).children[length - 1].getElementsByClassName("collapse")[0].style.backgroundPosition = collapseLogo;

                $scope.recursiveGetReqForWidget(++i, jsonObj, layoutChange);
            });
        } else {
            $scope.storeJson();
        }

    }

    $scope.loadJsonOnLayoutChange = function (layoutChange) {
       
        var widgetID, widgetPath;
        var jsonObj;
        var columnID, collapseState, name, length, collapseLogo, length;

        $http.get('/Widgets/GetLayout').then(function (response) {

            jsonObj = response.data;
            if (jsonObj != "") {
                
                columnID = document.getElementById('columns').children[0].children[0].id;

                if (jsonObj.widget != undefined && jsonObj.widget.length > 0) {

                    $scope.recursiveGetReqForWidget(0, jsonObj, layoutChange);
                }
            }


        });

        
    }

    $scope.loadJson = function () {

        var layoutID, layoutPath;
        var jsonObj;
        $http.get('/Widgets/GetLayout').then(function (response) {

            jsonObj = response.data;
            if (jsonObj != "") {
                
                if (jsonObj.layout != undefined && jsonObj.layout.length > 0) {

                    layoutID = jsonObj.layout[0].id;
                    layoutPath = $("#" + layoutID + "1").val();

                    $http.get(layoutPath).then(function (response) {
                        debugger;
                        $('#columns').empty();
                        $('#columns').append(response.data);

                        if (jsonObj.widget != undefined && jsonObj.widget.length > 0) {

                            $scope.recursiveGetReqForWidget(0, jsonObj, false);
                        }
                    })
                }
            }
        });        
    }

    $scope.storeJson = function () { 

        iNettuts.newInit();

        var layoutCache;
        var widgetCache;
        var i, j;
        var columns, colorClass, collapseState, collapseLogo, name;
        var jsonObj = {
            "layout": [],
            "widget": []
        }

        layoutCache = document.getElementById('columns').children[0].id;
        jsonObj.layout.push({
            "id": layoutCache
        })

        columns = document.getElementById('columns').children[0].children;

        for (i = 0; i < columns.length; i++) {
            for (j = 0; j < columns[i].children.length; j++) {
              
                widgetCache = columns[i].children[j].id;
                colorClass = columns[i].children[j].className;
                collapseState = columns[i].children[j].getElementsByClassName("widget-content")[0].style.display;
                name = columns[i].children[j].children[0].getElementsByTagName("h3")[0].innerHTML;
                collapseLogo = columns[i].children[0].getElementsByClassName("collapse")[0].style.backgroundPosition;
                jsonObj.widget.push({
                    "id": widgetCache,
                    "parentid": columns[i].id,
                    "index": j,
                    "color": colorClass,
                    "name": name,
                    "collapseState": collapseState,
                    "collapseLogo": collapseLogo
                });
            }
        }

        // $cookieStore.put(cookieJsonObj, jsonCache);
        var data = JSON.stringify(jsonObj);
        $http.post('/Widgets/SaveLayout', JSON.stringify({ jsonObj: data }));

    }

    $scope.loadJson();

}]);
