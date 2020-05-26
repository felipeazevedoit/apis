var rua;
var bairro;
var cidade;
var uf;
var desc;

function preencherEndereco(cep) {
    var settings = {
        "async": true,
        "crossDomain": true,
        "url": "http://201.73.1.17:5300/api/Seguranca/endereco/BuscarEnderecoPorCep/12/999",
        "method": "POST",
        "headers": {
            "Content-Type": "application/json",
            "Cache-Control": "no-cache",
            "Postman-Token": "fa43c255-9862-4a93-a554-47cb98066396"
        },
        "processData": false,
        "data": "{\"cep\": \"" + cep + "\"}"
    }
    $.ajax(settings).done(function (response) {
        if (response.endereco == '' || response.endereco == null) {
            $("#lblCep").removeClass();
            $("#lblCep").addClass("ativo").text("*CEP Inválido");
            $("#cep").focus();
        }
        else {
            $("#lblCep").removeClass();
            $("#lblCep").addClass("off").text("*Por favor preencha o campo");
            $('#rua').val(response.endereco);
            rua = response.endereco;
            $('#bairro').val(response.bairro);
            bairro = response.bairro;
            $('#cidade').val(response.cidade);
            cidade = response.cidade;
            $('#uf').val(response.uf);
            //$('#cmbUf').val(response.uf);
            uf = response.uf;
            $("#local").val(response.endereco + ", " + response.bairro + ", " + response.cidade + "-" + response.uf);
            $("#localA").val(response.endereco + ", " + response.bairro + ", " + response.cidade + "-" + response.uf);
            desc = response.endereco + ", " + $("#numero").val() + " - " + response.bairro + " " + response.cidade + "-" + response.uf + ", " + response.cep;
            $('#desc').val(desc);
            console.log(desc);
        }

    });
}

$("#cep").on("change", function () {
    preencherEndereco($(this).val());
});

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