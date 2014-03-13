$(function () {
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
                var asInitVals = new Array();

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

                $inputs = $('tfoot input', $(element));

                $inputs.keyup(function () {
                    /* Filter on the column (the index) of this element */
                    table.fnFilter(this.value, $inputs.index(this));
                });

                /*
                 * Support functions to provide a little bit of 'user friendlyness' to the textboxes in
                 * the footer
                 */
                $inputs.each(function (i) {
                    asInitVals[i] = this.value;
                });

                $inputs.focus(function () {
                    if (this.className == 'search_init') {
                        this.className = '';
                        this.value = '';
                    }
                });

                $inputs.blur(function (i) {
                    if (this.value == '') {
                        this.className = 'search_init';
                        this.value = asInitVals[$('tfoot input').index(this)];
                    }
                });

            }, 0);
        }
    };

    ko.applyBindings(app);
});
