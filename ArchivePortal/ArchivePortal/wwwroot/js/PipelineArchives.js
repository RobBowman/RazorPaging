"use strict";

var $loadingIndicator = $('#loading-indicator');
$loadingIndicator.show();

$.ajax("/api/pipelinearchives/",
    { method: "get" })
    .then(function (response) {
        $("#pipelinearchives").dataTable({
            data: response,
            columns: [
                { "data": "createdOnString" },
                { "data": "bodyIntro" },
                { "data": "receiveLocation" },
                { "data": "receivedFilename" },
                { "data": "sendPort" }
            ]
        });
    });
            
$loadingIndicator.hide();

