$(document).ready(function () {
    $('#motores').DataTable();
});

function mostrarAcoes(mId) {

    var url = "MotorAux/ModalAcoes?id=" + mId;

    var settings = {
        "async": true,
        "crossDomain": true,
        "url": url,
        "method": "GET",
    };

    $.ajax(settings).done(function (response) {
        $('#modal').html(response);
        $('#myModal').modal('show');

        $('#acoes').DataTable();
    });
}

function editarAcao() {
    let acaoId = $('input[name=acaoRadio]:checked', '#acoes').val();

    if (acaoId) {
        $('#myModal').modal('toggle');
        var url = "MotorAux/ModalAcao?id=" + acaoId;

        var settings = {
            "async": true,
            "crossDomain": true,
            "url": url,
            "method": "GET",
        };

        $.ajax(settings).done(function (response) {
            $('#modal').html(response);
            $('#modalAcao').modal('show');
        });
    }
    else {
        alert('Selecione uma ação para edição.');
    }
}

function adicionarAcao(idMotor) {
    $('#myModal').modal('toggle');
    var url = "MotorAux/SalvarAcao?motorId=" + idMotor;

    var settings = {
        "async": true,
        "crossDomain": true,
        "url": url,
        "method": "GET",
    };

    $.ajax(settings).done(function (response) {
        $('#modal').html(response);
        $('#modalAcao').modal('show');
    });
}

function salvaAcao() {
    var acaoViewModel = {
        id: $('#id').val(),
        nome: $('#nome').val(),
        descricao: $('#descricao').val(),
        ativo: $('#ativo').prop('checked'),
        status: $('#status').val(),
        idCliente: $('#idCliente').val(),
        idTipoAcao: $('#idTipoAcao').val(),
        caminho: $('#caminho').val(),
        idMotorAux: $('#idMotorAux').val()
    };

    var url = "MotorAux/SalvarAcao";
    var settings = {
        "async": true,
        "crossDomain": true,
        "url": url,
        "method": "POST",
        "data": acaoViewModel
    };

    $.ajax(settings).done(function (response) {
        alert(response);
    });
}

function mostrarParametros(acaoId) {
    $('#myModal').modal('toggle');
    var url = "MotorAux/ModalParametros?id=" + acaoId;

    var settings = {
        "async": true,
        "crossDomain": true,
        "url": url,
        "method": "GET",
    };

    $.ajax(settings).done(function (response) {
        $('#modal').html(response);
        $('#parametros').modal('show');
    });
}

function editarParametro() {
    let parametroId = $('input[name=parametroRadio]:checked', '#parametros').val();

    if (parametroId) {
        $('#parametros').modal('toggle');
        var url = "MotorAux/ModalParametro?id=" + parametroId;

        var settings = {
            "async": true,
            "crossDomain": true,
            "url": url,
            "method": "GET",
        };

        $.ajax(settings).done(function (response) {
            $('#modal').html(response);
            $('#modalParametro').modal('show');
        });
    }
    else {
        alert('Selecione um parâmetro para edição.');
    }
}

function adicionarParametro(idAcao) {
    $('#parametros').modal('toggle');
    var url = "MotorAux/SalvarParametro?acaoId=" + idAcao;

    var settings = {
        "async": true,
        "crossDomain": true,
        "url": url,
        "method": "GET",
    };

    $.ajax(settings).done(function (response) {
        $('#modal').html(response);
        $('#modalParametro').modal('show');
    });
}

function salvaParametro() {
    var parametroViewModel = {
        id: $('#id').val(),
        nome: $('#nome').val(),
        descricao: $('#descricao').val(),
        ativo: $('#ativo').prop('checked'),
        status: $('#status').val(),
        idCliente: $('#idCliente').val(),
        tipo: $('#tipo').val(),
        ordem: $('#ordem').val(),
        idAcao: $('#idAcao').val()
    };

    var url = "MotorAux/SalvarParametro";
    var settings = {
        "async": true,
        "crossDomain": true,
        "url": url,
        "method": "POST",
        "data": parametroViewModel
    };

    $.ajax(settings).done(function (response) {
        alert(response);
    });
}