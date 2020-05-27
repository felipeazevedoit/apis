$('#btnEntrar').on('click', function () {
    carregaModalLogin();
});

function carregaModalLogin(result) {

    var settings = {
        "url": "/Login/_Login",
        "method": "GET",
    }

    $.ajax(settings).done(function (response) {
        $('#modal').html(response);//home
        closeModal();
        $('#modalLogin').modal('show');//partial
        if (result) {
            $('#return').removeClass('fade');
            $('#return').text(result);
        }
    });

}

//$('#btnSenha').on('click', function () {
//    alert("nera");
//});

$("#btnSenha").click(function () {
    carregaModalSenha();
});

function carregaModalSenha() {

    var settings = {
        "url": "/Login/_EsqueciSenha",
        "method": "GET",
    }

    $.ajax(settings).done(function (response) {
        $('#modalLogin').modal('hide');
        $('#modal').html(response);//home
        closeModal();
        $('#modalSenha').modal('show');//partial
        $('#modalLogin').modal('hide');
    });

}

$("#news").click(function () {
    carregaModalNews();
});

function carregaModalNews() {

    var settings = {
        "url": "/Home/_News",
        "method": "GET",
    }

    $.ajax(settings).done(function (response) {
        $('#modal').html(response);//home
        closeModal();
        $('#modalNews').modal('show');//partial
        //$('#myModal').modal('hide');
    });

}

function carregaModalPaciente() {

    var settings = {
        "url": "/Paciente/_Paciente",
        "method": "GET",
    }

    $.ajax(settings).done(function (response) {
        $('#modal').html(response);//home
        closeModal();
        $('#modalPaciente').modal('show');//partial
        $('#myModal').modal('hide');
    });

}

$("#btnLista").click(function () {
    carregaModalLista();
});

function carregaModalLista() {

    var settings = {
        "url": "/Lista/_Lista",
        "method": "GET",
    }

    $.ajax(settings).done(function (response) {
        $('#myModal').modal('hide');
        closeModal();
        $('#modal').html(response);//home
        $('#modalLista').modal('show');//partial
    });

}

$("#btnClinicas").click(function () {
    carregaModalClinica();
});

function carregaModalClinica() {

    var settings = {
        "url": "/Clinica/_Clinica",
        "method": "GET",
    }

    $.ajax(settings).done(function (response) {
        $('#modalLista').modal('hide');
        $('#myModal').modal('hide');
        $('#modal').html(response);//home
        closeModal();
        $('#modalClinica').modal('show');//partial
        $(".chosen-select").chosen({ no_results_text: "Nada encontrado!" });
        $(".chosen-select").chosen({ allow_single_deselect: true });
        $("#cmbUfCrm_chosen").css("width", "100%");
    });

}

$("#btnMedico").click(function () {
    carregaModalMedico();
});

function carregaModalMedico() {

    var settings = {
        "url": "/Medico/_Medico",
        "method": "GET",
    }

    $.ajax(settings).done(function (response) {
        $('#modal').html(response);//home
        closeModal();
        $('#modalMedico').modal('show');//partial
        $(".chosen-select").chosen({ no_results_text: "Nada encontrado!" });
        $(".chosen-select").chosen({ allow_single_deselect: true });
        $("#cmbUf_chosen").css("width", "100%");
        $("#cmbEspecialidade_chosen").css("width", "100%");
        $("#cmbClinica_chosen").css("width", "100%");
        $('#modalLista').modal('hide');
    });

}

$("#btnPrestador").click(function () {
    carregaModalProfissional();
});

function carregaModalProfissional() {

    var settings = {
        "url": "/Profissional/_Profissional",
        "method": "GET",
    }

    $.ajax(settings).done(function (response) {
        $('#modal').html(response);//home
        closeModal();
        $('#modalProfissional').modal('show');//partial
        $(".chosen-select").chosen({ no_results_text: "Nada encontrado!" });
        $(".chosen-select").chosen({ allow_single_deselect: true });
        $("#cmbUf_chosen").css("width", "100%");
        $("#cmbArea_chosen").css("width", "100%");
        $('#modalLista').modal('hide');
    });

}

$("#btnFornecedor").click(function () {
    carregaModalProfissional();
});

function carregaModalFornecedor() {

    var settings = {
        "url": "/Fornecedor/_Fornecedor",
        "method": "GET",
    }

    $.ajax(settings).done(function (response) {
        $('#modal').html(response);//home
        closeModal();
        $('#modalFornecedor').modal('show');//partial
        $('#modalLista').modal('hide');
    });

}

//$("#btnEscolha").click(function () {
//    carregaModalEscolha();
//});

function carregaModalEscolha() {

    var settings = {
        "url": "/Home/_Escolha",
        "method": "GET",
    }

    $.ajax(settings).done(function (response) {
        $('#modal').html(response);//home
        closeModal();
        $('#myModal').modal('show');//partial
    });

}

function fecharClinica() {
    $('#modalClinica').modal('hide');
    $('#modalLista').modal('hide');
    $('#myModal').modal('hide');
    $('#modal').modal('hide');
    closeModal();
}

function fecharFornecedor() {
    $('#modalFornecedor').modal('hide');//partial
}

function fecharPaciente() {
    $('#modalPaciente').modal('hide');//partial
}

function fecharProfissional() {
    $('#modalProfissional').modal('hide');//partial
}

function fecharMedico() {
    $('#modalMedico').modal('hide');//partial
}

function fecharLista() {
    $('#modalLista').modal('hide');//partial
}

function fecharLogin() {
    $('#modalLogin').modal('hide');//partial
}

function fecharSenha() {
    $('#modalSenha').modal('hide');//partial
}

function fecharNews() {
    $('#modalNews').modal('hide');//partial
}

function LoadingInit(elemento) {
    $(elemento).loading({
        theme: 'dark',
        message: 'Aguarde...',
        onStart: function (loading) {
            loading.overlay.slideDown(400);
        },
        onStop: function (loading) {
            loading.overlay.slideUp(400);
        }
    });
}

function LoadingStop(elemento) {
    $(elemento).loading('stop');
}

function closeModal() {
    $('.modal-backdrop').remove();
}