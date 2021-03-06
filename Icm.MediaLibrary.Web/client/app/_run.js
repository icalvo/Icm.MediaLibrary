﻿$(function () {
    var restApi = new RestApi(Configuration.RestRoot, Configuration.RestMetadataUrl);

    var app = new AppViewModel(new AppDataModel(restApi), restApi);

    app.addViewModel({
        name: "Home",
        bindingMemberName: "home",
        factory: HomeViewModel
    });


    app.addViewModel({
        name: "Login",
        bindingMemberName: "login",
        factory: LoginViewModel,
        navigatorFactory: function (app) {
            return function () {
                app.errors.removeAll();
                app.user(null);
                app.view(app.Views.Login);
            };
        }
    });


    app.addViewModel({
        name: "Manage",
        bindingMemberName: "manage",
        factory: ManageViewModel,
        navigatorFactory: function (app) {
            return function(externalAccessToken, externalError) {
                app.errors.removeAll();
                app.view(app.Views.Manage);

                if (typeof (externalAccessToken) !== "undefined" || typeof (externalError) !== "undefined") {
                    app.manage().addExternalLogin(externalAccessToken, externalError);
                } else {
                    app.manage().load();
                };
            };
        }
    });

    app.addViewModel({
        name: "Register",
        bindingMemberName: "register",
        factory: RegisterViewModel
    });


    app.addViewModel({
        name: "RegisterExternal",
        bindingMemberName: "registerExternal",
        factory: RegisterExternalViewModel,
        navigatorFactory: function (app) {
            return function (userName, loginProvider, externalAccessToken, loginUrl, state) {
                app.errors.removeAll();
                app.view(app.Views.RegisterExternal);
                app.registerExternal().userName(userName);
                app.registerExternal().loginProvider(loginProvider);
                app.registerExternal().externalAccessToken = externalAccessToken;
                app.registerExternal().loginUrl = loginUrl;
                app.registerExternal().state = state;
            };
        }
    });

    app.initialize();

    // Activate Knockout
    ko.validation.init({ grouping: { observable: false } });

    $.fn.dataTableExt.oApi.fnReloadAjax = function (oSettings, sNewSource, fnCallback, bStandingRedraw) {
        // DataTables 1.10 compatibility - if 1.10 then versionCheck exists.
        // 1.10s API has ajax reloading built in, so we use those abilities
        // directly.
        if ($.fn.dataTable.versionCheck) {
            var api = new $.fn.dataTable.Api(oSettings);

            if (sNewSource) {
                api.ajax.url(sNewSource).load(fnCallback, !bStandingRedraw);
            }
            else {
                api.ajax.reload(fnCallback, !bStandingRedraw);
            }
            return;
        }

        if (sNewSource !== undefined && sNewSource !== null) {
            oSettings.sAjaxSource = sNewSource;
        }

        // Server-side processing should just call fnDraw
        if (oSettings.oFeatures.bServerSide) {
            this.fnDraw();
            return;
        }

        this.oApi._fnProcessingDisplay(oSettings, true);
        var that = this;
        var iStart = oSettings._iDisplayStart;
        var aData = [];

        this.oApi._fnServerParams(oSettings, aData);

        oSettings.fnServerData.call(oSettings.oInstance, oSettings.sAjaxSource, aData, function (json) {
            /* Clear the old information from the table */
            that.oApi._fnClearTable(oSettings);

            /* Got the data - add it to the table */
            var aData = (oSettings.sAjaxDataProp !== '') ?
                that.oApi._fnGetObjectDataFn(oSettings.sAjaxDataProp)(json) : json;

            for (var i = 0 ; i < aData.length ; i++) {
                that.oApi._fnAddData(oSettings, aData[i]);
            }

            oSettings.aiDisplay = oSettings.aiDisplayMaster.slice();

            that.fnDraw();

            if (bStandingRedraw === true) {
                oSettings._iDisplayStart = iStart;
                that.oApi._fnCalculateEnd(oSettings);
                that.fnDraw(false);
            }

            that.oApi._fnProcessingDisplay(oSettings, false);

            /* Callback user function - for event handlers etc */
            if (typeof fnCallback == 'function' && fnCallback !== null) {
                fnCallback(oSettings);
            }
        }, oSettings);
    };

    ko.bindingHandlers.dataTable = {
        init: function (element, valueAccessor) {
            var options = ko.utils.unwrapObservable(valueAccessor()) || {};
            setTimeout(function () {
                var table;
                var $inputs = $('tfoot input', $(element));

                table = $(element).dataTable({
                    'bProcessing': true,
                    'bServerSide': true,
                    'sAjaxSource': 'http://localhost:52241/Home/DataTable',
                    'aoColumnDefs': [
                        { 'bSearchable': true, 'aTargets': ['_all'] },
                    ],
                    'bScrollInfinite': true,
                    'bScrollCollapse': true,
                    'sScrollY': '400px',
                });

                $inputs.keyup(function () {
                    /* Filter on the column (the index) of this element */
                    table.fnFilter(this.value, $inputs.index(this));
                });

            }, 0);
        }
    };

    ko.applyBindings(app);
});
