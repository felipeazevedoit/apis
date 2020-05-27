function carregaModalHistorico() {

    var settings = {
        "url": "/Medico/_Historico/?medicoId=" + $("#medicoId").val() + "&medicoNome=" + $("#medicoNome").val(),
        "method": "GET",
    };

    $.ajax(settings).done(function (response) {
        $('#historicoModal').html(response);
        $('#historico').modal('show');
    });

}