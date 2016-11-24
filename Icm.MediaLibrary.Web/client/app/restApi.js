function RestApi(restRoot, restMetadataUrl) {
    var self = this;

    self.urls = {};

    self.data = null;

    self.load =
        $.ajax(restMetadataUrl)
            .done(function (data) {
                self.data = data;
                $.each(data, function (i, action) {
                    if (!self[action.controllerName]) {
                        self.urls[action.controllerName] = {};
                        self[action.controllerName] = {};
                    }

                    var camelCaseActionName = action.actionName[0].toLowerCase() + action.actionName.substr(1);

                    self.urls[action.controllerName][camelCaseActionName] = function (parameters) {
                        var url = restRoot + action.url;
                        $.each(action.parameters, function (j, parameter) {
                            if (parameter.isUriParameter) {
                                if (parameters[parameter.name] === undefined) {
                                    console.debug('Missing parameter: ' + parameter.name + ' for API: ' + action.controllerName + '/' + action.actionName);
                                } else {
                                    url = url.replace("{" + parameter.name + "}", parameters[parameter.name]);
                                }
                            }
                        });

                        return url;
                    };

                    self[action.controllerName][camelCaseActionName] = function (parameters) {
                        var url = restRoot + action.url;
                        var uriParameters = [];
                        var nonUriArguments = {};
                        $.each(action.parameters, function (j, parameter) {
                            if (parameter.isUriParameter) {
                                if (parameters[parameter.name] === undefined) {
                                    console.debug('Missing parameter: ' + parameter.name + ' for API: ' + action.controllerName + '/' + action.actionName);
                                } else {
                                    url = url.replace("{" + parameter.name + "}", parameters[parameter.name]);
                                }
                                uriParameters.push(parameter.name);
                            }
                        });
                        if (typeof (parameters) !== 'undefined') {
                            for (name in parameters) {
                                if ($.inArray(name, uriParameters) === -1) {
                                    nonUriArguments[name] = parameters[name];
                                }
                            }
                        }

                        console.debug(action.method + ": " + url);
                        return $.ajax({
                            type: action.method,
                            url: url,
                            data: nonUriArguments
                        });
                    };
                });
            });
}
