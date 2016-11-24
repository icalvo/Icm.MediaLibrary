function HomeViewModel(app, dataModel, restApi) {
    var self = this;

    self.newTags = ko.observable();

    self.addTags = function() {
        restApi.media.addTags(newTags());
    };
}

