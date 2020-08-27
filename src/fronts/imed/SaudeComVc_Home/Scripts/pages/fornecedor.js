//function carregarArea() {
//    var url = "http://201.73.1.17:5300/api/Seguranca/wpProfissionais/BuscarServico/12/999";

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

function getDataUsuarioFornecedor() {
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
        tipo: 2
    }
}

function cadFornecedor() {
    $('#btnCadastrar').text("Aguarde..");
    if ($("#senha").val() == $("#confirmarSenha").val() ) {
        console.log(getDataUsuarioFornecedor());


        var urlU = "http://201.73.1.17:5300/api/Seguranca/Principal/salvarUsuario/12/999";

        var usuario = { 'usuario': getDataUsuarioFornecedor() };
        console.log(JSON.stringify(usuario));

        //LoadingInit("body");
        $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            url: urlU,
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(usuario),
            success: function (dataUser) {
                console.log(dataUser);

                function getDataFornecedor() {
                    return {
                        nome: $('#nome').val(),
                        usuarioCriacao: 1,
                        usuarioEdicao: 1,
                        ativo: false,
                        status: 1,
                        idCliente: 12,
                        razaoSocial: $('#nome').val(),
                        descricao: $('#descricao').val(),
                        cnpj: $('#cnpj').val(),
                        email: $('#email').val(),
                        tipoEmpresaId: 3,
                        endereco: {
                            cep: $("#cep").val(),
                            estado: uf,
                            cidade: cidade,
                            Bairro: bairro,
                            local: rua,
                            numeroLocal: $("#numero").val(),
                            uf: uf,
                            nome: $('#nome').val(),
                            descricao: desc,
                            status: 1,
                            idCliente: 12,
                            complemento: $('#complemento').val()
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
                        codigoExterno: dataUser.id,
                        descricao: $('#site').val(),
                        CpfResponsavel: $('#cpf').val(),
                        EmailResponsavel: $('#emailResp').val(),
                        NomeResponsavel: $('#nomeResp').val()

                    }
                }

                var urlC = "http://201.73.1.17:5300/api/Seguranca/wpEmpresas/SalvarEmpresas/12/999";

                var fornecedor = { 'empresa': getDataFornecedor() };
                console.log(JSON.stringify(fornecedor));

                LoadingInit('body');
                $.ajax({
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    url: urlC,
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify(fornecedor),
                    success: function (data) {

                        console.log("DEU TUDO CERTO!");
                        console.log(data);
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
        });
    }
    else
    {
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