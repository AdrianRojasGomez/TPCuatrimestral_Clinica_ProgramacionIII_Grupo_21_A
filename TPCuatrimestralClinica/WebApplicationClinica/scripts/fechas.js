var calendarioHabilitado = false;

$(function () {
    $('#calendarioTurnos').datepicker({
        format: 'yyyy-mm-dd',
        language: 'es',
        todayHighlight: true,
        startDate: new Date()
    }).on('changeDate', function (e) {

        // Si está deshabilitado, ignorar el click
        if (!calendarioHabilitado) {
            $('#calendarioTurnos').datepicker('clearDates');
            return;
        }

        var fechaFormateada = e.format('yyyy-mm-dd');

        $('#hdnFechaTurno').val(fechaFormateada);
        __doPostBack(fechaLinkId, '');
    });
});

// funciones en el scope global
function habilitarCalendario() {
    calendarioHabilitado = true;
    $('#calendarioTurnos').removeClass('datepicker-disabled');
}

function deshabilitarCalendario() {
    calendarioHabilitado = false;
    $('#calendarioTurnos')
        .addClass('datepicker-disabled')
        .datepicker('clearDates');

    $('#hdnFechaTurno').val('');
}
