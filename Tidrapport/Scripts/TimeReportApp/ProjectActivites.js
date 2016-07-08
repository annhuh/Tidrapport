//<script type="text/javascript">
$('#ProjectId').change(function () {
    var selectedProject = $(this).val();
    if (selectedProject != null && selectedProject != '') {
        $.getJSON('@Url.Action("Activities", "Activities")', { id: selectedProject }, function (activities) {
            var activitiesSelect = $('#ActivityId');
            activitiesSelect.empty();
            $.each(activities, function (index, activity) {
                activitiesSelect.append($('<option/>', {
                    value: activity.value,
                    text: activity.text
                }));
            });
        });
    }
});
//</script>