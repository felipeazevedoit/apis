﻿
$(document).ready(function () {
    $(".chosen-select").chosen({ no_results_text: "Nada encontrado!" });
    $(".chosen-select").chosen({ allow_single_deselect: true });
    getEmpresas();
});

init();
controlarPaineis();
getAreaAtuacao();
aplicarMascaras();

$('#cadastrar').on('click', function () {
    if (validarCampos() == true) {
        carregaModal();
    }
});

$('#btnAgora').on('click', function () {

    PublicarAgora();
});

function PublicarAgora() {
    console.log('Entrou');
    LoadingInitBase('.body');
    var vaga = VagaViewModel();
    var settings = {
        "async": true,
        "crossDomain": true,
        "url": "/Vaga/PublicarAgora",
        "method": "POST",
        "data": vaga
    }

    $.ajax(settings).done(function (response) {
        if (response == "ok") {

            window.location = "/Vaga";
        }
        else {
            alert(response);
        }
    });
}

function PublicarDepois() {


}

function getFormData() {
    return {
        nome: $('#nome').val(),
        cep: $('#cep').val(),
        rua: $('#rua').val(),
        bairro: $('#bairro').val(),
        numero: $('#numero').val(),
        cidade: $('#cidade').val(),
        uf: $('#uf').val(),
        data: $('#data').val(),
        hora: $('#txtHoraInicio').val(),
        qtd: $('#qtd').val(),
        valor: $('#valor').val(),
        atuacao: $('#atuacao').val(),
        profissional: $('#profissional option:selected').val(),
        profissionalNome: $('#profissional').val(),
        Qtd: $('#qtd').val(),
        Total: $('#total').val()
    }
}

function aplicarMascaras() {
    //$('#data').mask('00/00/0000');
    $('#numero').mask('9999');
    $('#cep').mask('99999-999');
}

function VagaViewModel() {
    let data = $('#data').val();
    let dataEvento = Date.parse(data);

    var VagaViewModel = {
        Id: $('#vagaId').val(),
        Nome: $('#nome').val(),
        Cep: $('#cep').val(),
        Rua: $('#rua').val(),
        Bairro: $('#bairro').val(),
        Numero: $('#numero').val(),
        Cidade: $('#cidade').val(),
        Date: data,
        Complemento: $('#complemento').val(),
        Referencia: $('#complemento').val(),
        Uf: $('#uf').val(),
        DataEvento: dataEvento,
        Hora: $('#txtHoraInicio').val(),
        Qtd: $('#qtd').val(),
        Valor: $('#valor').val(),
        AreaAtuacao: $('#atuacao').val(),
        Profissional: $('#profissional option:selected').val(),
        ProfissionalNome: $('#profissional option:selected').text(),
        //Qtd: $('#qtd').val(),
        Total: $('#total').val(),
        IdEmpresa: $('#empresas option:selected').val(),
        EnderecoId: $('#endId').val(),
        DataCriacao: $('#vagaData').val(),
        EnderecoDataCriacao: $('#enderecoData').val(),
        Servico: $('#atuacao option:selected').text(),
        LocalOportunidade: $('#localOpt').val()
    };
    return VagaViewModel;
}

function getAreaAtuacao() {

    var Url = "/Profissionais/BuscarServicoTipo";
    var settings = {
        "async": true,
        "crossDomain": true,
        "url": Url,
        "method": "GET"
    }

    $.ajax(settings).done(function (response) {
        for (var i = 0; i < response.length; i++) {
            $('#atuacao').append('<option value="' + response[i].id + '">' + response[i].nome + '</option>');
            $('#atuacao').selectpicker('refresh');
        }

        let area = $('#areaSelected').val();

        if (area > 0) {
            $('#atuacao').val(area).change();
            $('#atuacao').trigger("chosen:updated");
        }
    });
}

function getCep() {
    var cep = $('#cep').val();
    cep = cep.replace("-", "");
    return int.parse(cep);
}

function getProfissionalPorAtuacao(id) {

    var Url = "/Profissionais/BuscarServico";
    var settings = {
        "async": true,
        "crossDomain": true,
        "url": Url,
        "method": "GET"
    }

    $.ajax(settings).done(function (response) {
        //loadSelect

        $("#profissional").empty();
        $('#profissional').append('<option value="0">Selecione</option>');
        $('#profissional').selectpicker('refresh');
        $('#profissional').focus();

        for (var i = 0; i < response.length; i++) {
            if (response[i].servicoTipoId == id) {

                $('#profissional').append('<option value="' + response[i].id + '">' + response[i].nome + '</option>');
                $('#profissional').selectpicker('refresh');
            }
        }

        let profissional = $('#profissionalSelected').val();
        if (profissional > 0) {
            $('#profissional').val(profissional).change();
            $('#profissional').trigger("chosen:updated");
        }
    });
}

function getForm() {
    return {
        nome: $('#nome'),
        cep: $('#cep'),
        rua: $('#rua'),
        bairro: $('#bairro'),
        cidade: $('#cidade'),
        complemento: $('#complemento'),
        uf: $('#uf'),
        data: $('#data'),
        hora: $('#txtHoraInicio'),
        qtd: $('#qtd'),
        valor: $('#valor'),
        atuacao: $('#atuacao'),
        profissional: $('#profissional'),
        referencia: $('#referencia')
    }
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

function LoadingInitBase(elemento) {
    $(elemento).loading({
        theme: 'dark',
        message: 'Aguarde...'
    });
}

function LoadingBodyStop() {
    $('body').loading('stop');
}

function getEmpresas() {

    var Url = "/Vaga/ListarEmpresas";
    var settings = {
        "async": true,
        "crossDomain": true,
        "url": Url,
        "method": "GET"
    };

    $.ajax(settings).done(function (response) {

        $.each(response, function (index, item) {
            //console.log(item);
            $('#empresas').append('<option value="' + item.Id + '">' + item.Nome + '</option>');
            $('#empresas').trigger("chosen:updated");
        });

        let empresa = $('#empresaSelected').val();
        $('#empresas').val(empresa);
        $('#empresas').trigger("chosen:updated");
    });
}

function init() {
    $('.selectpicker').selectpicker();
    $("#miolo").addClass("card-wizard");
    $(".pnProfissional").hide();
    $(".pnValores").hide();
}

function calcularValorContratacao() {
    var valor = $('#valor').val();
    var qtd = $('#qtd').val();
    var total = parseFloat(valor.replace(",", ".")) * qtd;
    return "R$" + total;
}

function limpaEndereco() {
    $("#cep").val("");
    $('#rua').val("");
    $('#bairro').val("");
    $('#cidade').val("");
    $('#uf').val("");
}

function desbloqueiaEndereco() {
    $("#rua").prop("disabled", false);
    $("#bairro").prop("disabled", false);
    $("#cidade").prop("disabled", false);
    $("#uf").prop("disabled", false);
    $("#numero").focus();

}

function bloqueiaEndereco() {
    $("#rua").prop("disabled", true);
    $("#bairro").prop("disabled", true);
    $("#cidade").prop("disabled", true);
    $("#uf").prop("disabled", true);
    $("#numero").focus();
    $('.card-body').loading('stop');
}
function LoadingStop(elemento) {
    $(elemento).loading('stop');
}


function preencherEndereco(cep) {
    var settings = {
        "async": true,
        "crossDomain": true,
        "url": "/Enderecos/BuscarEnderecoPorCep",
        "method": "POST",
        "headers": {
            "Content-Type": "application/json",
            "Cache-Control": "no-cache",
            "Postman-Token": "fa43c255-9862-4a93-a554-47cb98066396"
        },
        "processData": false,
        "data": "{\"cep\": \"" + cep + "\"}"
    }

    LoadingInit('body');
    $.ajax(settings).done(function (response) {
        console.log(response);
        if (response.endereco == '' || response.endereco == null) {
            demo.showNotification('top', 'right', 'Por favor digite um CEP válido!');
            limpaEndereco();
            desbloqueiaEndereco();
            $("#cep").focus();
        }
        else {
            $('#rua').val(response.endereco);
            $('#bairro').val(response.bairro);
            $('#cidade').val(response.cidade);
            $('#uf').val(response.uf);

            bloqueiaEndereco();

        }

            LoadingStop('body');
    });


}

function carregaModal() {

    LoadingInit('body');
    var vagaViewModel = VagaViewModel();
    console.log(vagaViewModel);
    var settings = {
        "url": "/Vaga/ModalConfirmarVaga",
        "method": "POST",
        "data": vagaViewModel
    }

    $.ajax(settings).done(function (response) {
        $('#modal').html(response);
        $('#myModal').modal('show');
        LoadingBodyStop();
    });

    return true;
}

function controlarPaineis() {

    getForm().valor.on("change", function () {
        $("#total").val(calcularValorContratacao());
        $("#qtd").focus();
    });

    getForm().complemento.on("change", function () {
        $("#referencia").focus();
    });

    //getForm().referencia.on("change", function () {
    //    $("#data").focus();
    //});
    getForm().qtd.on("change", function () {
        $("#total").val(calcularValorContratacao());
    });

    getForm().atuacao.on("change", function () {
        LoadingInitBase('body');
        getProfissionalPorAtuacao($(this).val());
        $(".pnProfissional").fadeIn();
        LoadingStop('body');
    });

    getForm().profissional.on("change", function () {
        $(".pnValores").fadeIn();
        $("#valor").focus();

    });

    getForm().nome.on("change", function () {

        $("#cep").focus();
    });

    getForm().cep.on("change", function () {
        preencherEndereco($(this).val());
    });

    getForm().data.on("change", function () {
        valiidarData();
    });

    getForm().hora.on("change", function () {

        $("#atuacao").focus();
    });

}

function getData() {
    var data = getFormData().data;
    return Date.parse(data);
}

function valiidarData() {
    var data = getFormData().data;
    var date = new Date();
    var hoje = date.getFullYear() + "-" + date.getMonth() + "-" + date.getDay();

    var month = date.getMonth() + 1;
    var day = date.getDate();

    var output = date.getFullYear() + '/' +
        (month < 10 ? '0' : '') + month + '/' +
        (day < 10 ? '0' : '') + day;

    console.log(output);
    console.log(Date.parse(hoje));
    if (Date.parse(data) <= Date.parse(output)) {
        demo.showNotification('top', 'right', 'Não é possível cadastrar oportunidades retroativas!');
        $('#data').val(" ");
        $('#data').focus();
        return false;
    }
    else {
        return true;
    }

}

function validarCampos() {
    var form = getFormData();

    if (form.nome == "" || form.nome == null) {
        demo.showNotification('top', 'right', 'Campo nome é obrigatório!');
        $('#nome').focus();
        return false;
    }
    else if (form.cep == "" || form.rua == "" || form.cidade == "" || form.uf == "" || form.bairro == "") {
        demo.showNotification('top', 'right', 'Informe um endereço válido!');
        $('#cep').focus();
        return false;
    }
    else if (form.numero == "" || form.numero == null) {
        demo.showNotification('top', 'right', 'Digite um numero para o endereço!');
        $('#numero').focus();

        return null;
    }

    //else if (valiidarData() == false) {
    //    return false;
    //}

    else if (form.valor == "" || form.valor == null) {
        demo.showNotification('top', 'right', 'Informe o valor unitário da contratação!');
        $('#valor').focus();
        return null;
    }
    else if (form.qtd <= 0 || form.qtd == "" || form.qtd == null) {
        demo.showNotification('top', 'right', 'Informe uma quantidade válida!');
        $('#qtd').focus();
        return null;
    }
    else if (form.profissional == "" || form.profissional == 0) {
        demo.showNotification('top', 'right', 'selecione o profissional!');
        $('#profissional').focus();
        return null;
    }
    else if (form.hora == null || form.hora == "") {
        demo.showNotification('top', 'right', 'Informe o horário de início!');
        $('#hora').focus();
        return null;
    }
    else
        return true;
}