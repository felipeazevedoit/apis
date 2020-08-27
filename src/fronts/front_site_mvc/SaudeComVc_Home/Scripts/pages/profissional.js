var d = new Date();


function getDataUsuarioProfissional() {
    return {
        nome: $('#nome').val(),
        usuarioCriacao: 1,
        usuarioEdicao: 1,
        ativo: false,
        status: 1,
        idCliente: 12,
        login: $('#email').val(),
        email: $('#email').val(),
        sobrenome: $('#sobrenome').val(),
        senha: $('#senha').val(),
        vAdmin: "False",
        enviarEmail: true,
        origem: "sitesaude",
        tipo: 4
    }
}

function cadProfissional() {
    $('#btnCadastrar').text("Aguarde..");

    if ($("#senha").val() == $("#confirmarSenha").val()) {
        console.log(getDataUsuarioProfissional());


        var urlU = "http://servicepix.com.br:5300/api/Seguranca/Principal/salvarUsuario/12/999";

        var usuario = { 'usuario': getDataUsuarioProfissional() };
        console.log(JSON.stringify(usuario));

        LoadingInit("body");
        $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            url: urlU,
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(usuario),
            success: function (data) {

                function getDataProfissional() {
                    return {
                        nome: $('#nome').val(),
                        usuarioCriacao: 1,
                        usuarioEdicao: 1,
                        ativo: false,
                        status: 1,
                        idCliente: 12,
                        email: $('#email').val(),
                        idUsuario: data.id,
                        dataNascimento: $('#dataNascimento').val(),
                        endereco: {
                            nome: $('#nome').val(),
                            descricao: desc,
                            usuarioCriacao: 1,
                            usuarioEdicao: 1,
                            ativo: true,
                            status: 1,
                            idCliente: 12,
                            cep: $('#cep').val(),
                            estado: uf,
                            cidade: cidade,
                            Bairro: bairro,
                            local: rua,
                            numeroLocal: $('#numero').val()
                        },
                        telefone: {
                            nome: $('#nome').val(),
                            usuarioCriacao: 1,
                            usuarioEdicao: 1,
                            ativo: true,
                            status: 1,
                            idCliente: 12,
                            numero: $('#telefone').val()
                        },
                        registroprofissional: $('#registro').val(),
                        UFRegistro: $('#cmbUf :selected').val(),
                        cpf: $('#cpf').val(),
                    }
                }

                var urlP = "http://servicepix.com.br:5300/api/Seguranca/wpProfissionais/SalvarProfissional/12/999";

                var profissional = { 'profissional': getDataProfissional() };
                console.log(JSON.stringify(profissional));

                $.ajax({
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    url: urlP,
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify(profissional),
                    success: function (obj) {
                        perfil(obj, data);
 
 
                    }

                });
            }

        });
    }
    else {
        $("#lblErro").removeClass('off').addClass("ativo").text("*A confirmação da senha não está correta");
        $('#btnCadastrar').text("Cadastrar");
    }
};

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

function perfil(data,obj) {
    //associar perfilXmedico
    function getDataPerfil() {
        return {
            vinculoId: 0,
            perfilId: 22,
            usuarioId: obj.id,
        }
    }


    var urlPerfil = "http://servicepix.com.br:5300/api/Perfil/SaveUsuarioXPerfil";

    var perfila = getDataPerfil();
    console.log(JSON.stringify(perfila));

    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        url: '/Profissional/VincularPerfilAsync/',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(perfila),
        success: function (perfil) {
            console.log(perfil);
            vincular(obj, data);
            
            
        }

    });
}

$(document).ready(function () {

   
});

//function carregarArea() {
//    var url = "http://servicepix.com.br:5300/api/Seguranca/wpProfissionais/BuscarServico/12/999";

//    $.ajax({
//        type: "GET",
//        url: url,
//        data: { especialidade: $("#cmbArea").val() },
//        dataType: 'json',
//        contentType: "application/json; charset=utf-8",
//        success: function (obj) {
//            console.log(obj)
//            var htmx = "";

//            if (obj != null) {
//                var selectbox = $('#cmbArea');
//                selectbox.find('option').remove()
//                $.each(obj, function (i, d) {
//                    htmx += '<option value="' + d.id + '"> ' + d.nome + '</option > '
//                });
//                selectbox.html(htmx);
//            }
//        }
//    });
//}

function vincular(obj, data) {

    function getDataServico() {
        return {
            UsuarioCriacao: obj.id,
            UsuarioEdicao: obj.id,
            Nome: obj.nome,
            Ativo: true,
            ServicoId: $('#cmbArea :selected').val(),
            UsuarioId : data.id,
            CodigoExterno: data.id,
            DataCriacao: d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate(),
            DataEdicao: d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate(),
            IdCliente: 12,
            Status: 1
        }
    }

    var urlP = "http://servicepix.com.br:5300/api/Seguranca/wpProfissionais/VincluarProfissionalServico/12/999";

    var servico = {'profissionalServico': getDataServico() };
    console.log(JSON.stringify(servico));

    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        url: urlP,
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(servico),
        success: function (servico) {
            
            console.log("DEU TUDO CERTO!");
            console.log(servico);
            //Limpar campos
            $(':input', '#form1')
                .not(':button, :submit, :reset, :hidden')
                .val('')
                .removeAttr('checked')
                .removeAttr('selected');
            $('#form1')[0].reset();
            //Parar Aguarde;
            LoadingStop('body');
            $("#lblSpan").removeClass("fade");
            $("#lblErro").removeClass('ativo').addClass("off").text("");
            $('#btnCadastrar').text("Cadastrar")

        }

    });


}