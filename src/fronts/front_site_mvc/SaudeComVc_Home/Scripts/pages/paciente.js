function getDataUsuarioPaciente() {
    return {
        nome: $('#nome').val(),
        usuarioCriacao: 1,
        usuarioEdicao: 1,
        ativo: true,
        status: 1,
        idCliente: 12,
        login: $('#email').val(),
        email: $('#email').val(),
        sobrenome: $('#sobrenome').val(),
        senha: $('#senha').val(),
        vAdmin: "False",
        Termo: true,
        enviarEmail: true,
    }
}

function cadPaciente() {
    $('#btnCadastrar').text("Aguarde..");

    if ($("#senha").val() == $("#confirmarSenha").val()) {
        console.log(getDataUsuarioPaciente());


        var urlU = "http://servicepix.com.br:5300/api/Seguranca/Principal/salvarUsuario/12/999";

        var usuario = { 'usuario': getDataUsuarioPaciente() };
        console.log(JSON.stringify(usuario));

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

                function getDataPaciente() {
                    return {
                        nome: $("#nome").val(),
                        usuarioCriacao: 1,
                        usuarioEdicao: 1,
                        ativo: true,
                        idCliente: 12,
                        status: 1,
                        sexo: $("#cmbSexo option:selected").val(),
                        dataNascimento: $("#datanascimento").val(),
                        peso: $("#peso").val().replace("KG", ""),
                        altura: $("#altura").val(),
                        sobrenome: $("#sobrenome").val(),
                        cpf: $("#cpf").val(),
                        codigoExterno: dataUser.id,
                        convenioId: $("#cbmConvenio option:selected").val(),
                        telefone: {
                            ativo: true,
                            status: 1,
                            idCliente: 12,
                            numero: $('#telefone').val(),
                            usuarioCriacao: dataUser.id
                        },
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
                            idUsuario: dataUser.id
                        }
                    };
                }

                var urlP = "http://servicepix.com.br:5300/api/Seguranca/WpPacientes/SalvarPaciente/12/999";

                var paciente = { 'paciente': getDataPaciente() };
                console.log(JSON.stringify(paciente));

                $.ajax({
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    url: urlP,
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify(paciente),
                    success: function (dataPaciente) {
                        console.log(dataPaciente, 'background: #222; color: #bada55');
                        perfilPaciente(dataUser, dataPaciente)

                    }
                });
            }
        });
    }
    else {
        $("#lblTermos").removeClass('off').addClass("ativo").text("*A confirmação da senha não está correta");
        $('#btnCadastrar').text("Cadastrar");
    }
};

function perfilPaciente(result, user) {
    //associar perfilXmedico
    function getDataPerfilPaciente() {
        return {
            vinculoId: 0,
            perfilId: 14,
            usuarioId: result.id,
        }
    }


    var urlPerfil = "http://servicepix.com.br:5300/api/Perfil/SaveUsuarioXPerfil";

    var perfila = getDataPerfilPaciente();
    console.log(JSON.stringify(perfila));

    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        url: '/Paciente/VincularPerfilAsync/',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(perfila),
        success: function (perfil) {
            console.log(perfil);
            var token = $("#token").val();

            if (token == undefined) {
                var settings = {
                    "async": true,
                    "data": result,
                    "crossdomain": true,
                    "url": '/login/setusuariologado',
                    "method": "post"
                };

                $.ajax(settings).done(function (response) {
                    if (response == 'ok') {
                        enviarNotificacoes(perfila.usuarioId);
                        
                    }
                    else {
                        $('#btncadastrar').text("Cadastrar");
                    }
                });

            }
            else {
                altIdConvidado(token, result);
                buscarMedico(result, user);
            }
        }

    });
}

function enviarNotificacoes(usuarioId) {

    var envio = {
        ID: usuarioId,
    };

    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        url: '/Paciente/EnviarNotificacoes/',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(envio),
        success: function (result) {
            window.location.href = "/Paciente/Feed";
        },
    });
}

function altIdConvidado(token, result) {

    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        url: '/LandingPage/GetConvitePorTokenAsync?token=' + token,
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        success: function (obj) {
            console.log(obj);

            obj.IdConvidado = result.id;

            $.ajax({
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                url: '/LandingPage/SaveConviteAsync/',
                data: JSON.stringify(obj),
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                success: function (alt) {
                    console.log(alt);
                }

            });
        }

    });
}

function buscarMedico(result, user) {

    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        url: '/LandingPage/SearchMedicoAsync/',
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        success: function (dataMedico) {
            console.log(dataMedico);

            function getDataVinculo() {
                return {
                    usuario: $("#nome").val(),
                    idUsuario: result.id,
                    idPaciente: user.id,
                    idMedico: dataMedico.ID
                }
            }

            var vinculo = getDataVinculo();
            console.log(JSON.stringify(vinculo));

            $.ajax({
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                url: '/LandingPage/VincularMedicoXPacienteAsync/',
                data: JSON.stringify(vinculo),
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                success: function (dataVinculo) {
                    console.log(dataVinculo);

                    var settings = {
                        "async": true,
                        "data": result,
                        "crossdomain": true,
                        "url": '/login/setusuariologado',
                        "method": "post"
                    }

                    $.ajax(settings).done(function (response) {
                        if (response == 'ok') {
                            window.location.href = "/Paciente/Feed";
                        }
                        else {
                            $('#btncadastrar').text("Cadastrar");
                        }
                    });
                }
            });

        }
    });
}

function vinculoMedicoXPaciente(result, user, ) {

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

function buscarCpf() {
    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        url: '/Paciente/ValidarCpfAsync?cpf=' + $('#cpf').val(),
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        success: function (cpf) {

            if (cpf.length > 0) {
                $("#lblCpf").removeClass().addClass("ativo").text("*O cpf já está em uso");
                $('#btnCadastrar').prop("disabled", true);
            }
            else {
                $("#lblCpf").removeClass().addClass("off").text("*Obrigatório");
                $('#btnCadastrar').prop("disabled", false);
            }
        }
    });
}

function buscarEmail() {
    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        url: '/Paciente/ValidarEmailAsync?email=' + $('#email').val(),
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        success: function (result) {

            console.log(result);

            if (result == true) {
                $("#lblEmail").removeClass().addClass("ativo").text("*O email já está em uso");
                $('#btnCadastrar').prop("disabled", true);
            }
            else {
                $("#lblEmail").removeClass().addClass("off").text("*Obrigatório");
                $('#btnCadastrar').prop("disabled", false);
            }
        }
    });
}