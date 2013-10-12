function StewardScheduleViewModel() {
    this.stewards = ko.observableArray([]);
}

var objVM = new StewardScheduleViewModel();
ko.applyBindings(objVM);

function FetchStewards() {
    var ScheduleId = $("#ScheduleId").val();
    $.getJSON("/StewardSchedule/GetStewards/" + ScheduleId, null, function (data) {
        objVM.stewards(data);
    });
}