$(function () {
    $('#calendarioTurnos').datepicker({
        format: 'yyyy-mm-dd',
        language: 'es',
        todayHighlight: true
    }).on('changeDate', function () {
        $('#hdnFechaTurno').val(
            $('#calendarioTurnos').datepicker('getFormattedDate')
        );
    });
});