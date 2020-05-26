function convidarPaciente() {
    //associar perfilXmedico
    function getDataConvite() {
        return {
            email: $("#emailC").val(),
            nome: $("#nome").val()
        }
    }

    var convite = getDataConvite();
    console.log(JSON.stringify(convite));
    Loading('body');
    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        url: '/Convite/ConvidarPacienteAsync/',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(convite),
        success: function (perfil) {
            console.log(perfil);
            $("#lblEmail").removeClass("off");
            $("#lblEmail").addClass("sucesso");
            $("#lblEmail").text("Paciente Convidado com sucesso!");

        }
    }).always(function () {
        LoadingStop('body');
    });
}

function Loading(elemento) {
    $(elemento).loading({
        theme: 'dark',
        message: 'Aguarde...'
    });
}

function LoadingStop(elemento) {
    $(elemento).loading('stop');
}