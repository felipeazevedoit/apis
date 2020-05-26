function getDataClinica() {
    return {
        nome: $('#nome').val(),
        usuarioCriacao: 1,
        usuarioEdicao: 1,
        ativo: false,
        status: 9,
        idCliente: 12,
        razaoSocial: $('#nome').val(),
        cnpj: $('#cnpj').val(),
        email: $('#email').val(),
        tipoEmpresaId: 1,
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
            idCliente: 12
        },
        responsavel: {
            nome: $('#responsavel').val(),
            crm: $('#crm').val(),
            uf_crm: $('#cmbUfCrm :selected').val()
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
        origem: "sitesaude",
        tipo: 1,
        idsEspecialidades: $('#especialidades').val()
    };
}


function dados() {
    function getDataClinica() {
        return {
            nome: $('#nome').val(),
            usuarioCriacao: 1,
            usuarioEdicao: 1,
            ativo: false,
            status: 9,
            idCliente: 12,
            razaoSocial: $('#nome').val(),
            cnpj: $('#cnpj').val(),
            email: $('#email').val(),
            tipoEmpresaId: 1,
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
                idCliente: 12
            },
            responsavel: {
                nome: $('#responsavel').val(),
                crm: $('#crm').val(),
                uf_crm: $('#cmbUfCrm :selected').val()
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
            origem: "sitesaude",
            tipo: 1,
            idsEspecialidades: $('#especialidades').val()
        };
    }

    var clinica = { 'empresa': getDataClinica() };
    console.log(JSON.stringify(clinica));
}


function cadClinica() {
    $('#btnCadastrar').text("Aguarde..");

    console.log(getDataClinica());


    var urlC = "http://201.73.1.17:5300/api/Seguranca/wpEmpresas/SalvarEmpresas/12/999";

    var clinica = { 'empresa': getDataClinica() };
    console.log(JSON.stringify(clinica));

        LoadingInit('body');
        $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            url: urlC,
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(clinica),
            success: function (data) {
                EnviarEmail();
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
                $('#btnCadastrar').text("Cadastrar");
            }

        });

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

function buscarCnpj() {
    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        url: '/Clinica/ValidarCnpjAsync?cnpj=' + $('#cnpj').val(),
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        success: function (cnpj) {

            console.log(cnpj.length);

            if (cnpj.length > 0) {
                $("#lblCnpj").removeClass().addClass("ativo").text("*O cnpj já está em uso");
                $('#btnCadastrar').prop("disabled", true);
            }
            else {
                $("#lblCnpj").removeClass().addClass("off").text("*Obrigatório");
                $('#btnCadastrar').prop("disabled", false);
            }
        }
    });
}

function EnviarEmail() {
    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        url: '/Clinica/EnviarEmails',
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        success: function (email) {
            
        }

    });
}

