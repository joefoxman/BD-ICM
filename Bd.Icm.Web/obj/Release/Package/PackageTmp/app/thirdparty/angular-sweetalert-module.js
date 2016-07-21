﻿"use strict";
angular.module("oitozero.ngSweetAlert", []).factory("SweetAlert", ["$rootScope", function ($rootScope) {
    var swal = window.swal,
        self = {
            swal: function (arg1, arg2, arg3) {
                $rootScope.$evalAsync(function() {
                    "function" == typeof arg2 ? swal(arg1, function(isConfirm) {
                        $rootScope.$evalAsync(function() { arg2(isConfirm) });
                    }, arg3) : swal(arg1, arg2, arg3);
                });
            },
            success: function (title, message) {
                $rootScope.$evalAsync(function() {
                    swal(title, message, "success");
                });
            },
            error: function (title, message) {
                $rootScope.$evalAsync(function() {
                    swal(title, message, "error");
                });
            },
            warning: function (title, message) {
                $rootScope.$evalAsync(function() {
                    swal(title, message, "warning");
                });
            },
            info: function (title, message) {
                $rootScope.$evalAsync(function() {
                    swal(title, message, "info");
                });
            }
        };
        return self;
    }
]);