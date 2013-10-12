function PilotScheduleViewModel() {
    this.pilots = ko.observableArray([]);
}

var objVM = new PilotScheduleViewModel();
ko.applyBindings(objVM);

function FetchPilots() {
    var ScheduleId = $("#ScheduleId").val();
    $.getJSON("/PilotSchedule/GetPilots/" + ScheduleId, null, function (data) {
        objVM.pilots(data);
    });
}