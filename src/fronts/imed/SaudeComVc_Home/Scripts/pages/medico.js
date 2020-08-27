function getDataUsuarioMedico() {
    return {
        nome: $('#nome').val(),
        usuarioCriacao: 1,
        usuarioEdicao: 1,
        ativo: false,
        status: 1,
        idCliente: 12,
        login: $('#email').val(),
        email: $('#email').val(),
        senha: $('#senha').val(),
        VAdmin: "False",
        enviarEmail: true,
        origem: "sitesaude",
        tipo: 3
    }
}

var d = new Date();

function  cadMedico() {
    $('#btnCadastrar').text("Aguarde..");

    if ($("#senha").val() == $("#confirmarSenha").val()) {
        console.log(getDataUsuarioMedico());

        //salvar usuario

        var urlU = "http://201.73.1.17:5300/api/Seguranca/Principal/salvarUsuario/12/999";

        var usuario = { 'usuario': getDataUsuarioMedico() };
        console.log(JSON.stringify(usuario));

        LoadingInit('body');
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
                console.log(data);
                medico(data);
            }

        });
    }
    else {
        $("#lblErro").removeClass('off').addClass("ativo").text("*A confirmação da senha não está correta");
        $('#btnCadastrar').text("Cadastrar");
    }

};

function medico(data)
{
    var espc = $('#cmbEspecialidade :selected').val();

    //salvar medico

    function getDataMedico() {
        return {
            nome: $('#nome').val(),
            usuarioCriacao: 1,
            usuarioEdicao: 1,
            ativo: false,
            status: 1,
            idCliente: 12,
            idUsuario: data.id,
            codigoExterno: data.id,
            crm: $('#crm').val(),
            cpf: $('#cpf').val(),
            dataNascimento: $('#dataNascimento').val(),
            uf_crm: $('#cmbUf :selected').val(),
            especialidadeId: espc,
            telefone: {
                ativo: true,
                status: 1,
                idCliente: 12,
                numero: $('#telefone').val()
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
                idCliente: 12
            },
            idsClinicas: $('#clinicas').val()
        }
    }

    var urlM = "http://201.73.1.17:5300/api/Seguranca/WpMedicos/SalvarMedico/12/999";

    var medico = { 'medico': getDataMedico() };
    console.log(JSON.stringify(medico));

    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        url: urlM,
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(medico),
        success: function (obj) {
            console.log(obj);
            perfilMedico(data, obj);
        }

    });
}

function perfilMedico(data, obj) {
    //associar perfilXmedico
    function getDataPerfilMedico() {
        return {
            vinculoId: 0,
            perfilId: 12,
            usuarioId: data.id,
        }
    }


    var urlPerfil = "http://201.73.1.17:5300/api/Perfil/SaveUsuarioXPerfil";

    var perfila = getDataPerfilMedico();
    console.log(JSON.stringify(perfila));

    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        url: '/Medico/VincularPerfilAsync/',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(perfila),
        success: function (perfil) {
            console.log(perfil);
            pagina(obj, data);
        }

    });
}

function pagina(obj, data) {
    //cadastrar pagina
    function getDataPagina() {
        return {
            ID: 0,
            Nome: obj.nome,
            Descricao: "",
            DataCriacao: d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate(),
            DateAlteracao: d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate(),
            UsuarioCriacao: data.id,
            UsuarioEdicao: data.id,
            Ativo: false,
            Status: 9,
            IdCliente: 12,
            CodigoExterno: data.id,
            Banner: null,
            Apresentacao: ""
        }
    }


    var urlMP = "http://201.73.1.17:5300/api/Seguranca/Paginas/SalvarPagina/12/" + data.id + "";

    var paginaa = { 'pagina': getDataPagina() };
    console.log(JSON.stringify(paginaa));

    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        url: urlMP,
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify(paginaa),
        success: function (objPagina) {
            console.log(objPagina);

            console.log("DEU TUDO CERTO!");
            console.log(objPagina);
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
            $('#btnCadastrar').text("Cadastrar");

        }

    });
}

//function carregarEspecialidade() {
//    var url = "http://201.73.1.17:5300/api/Seguranca/WpMedicos/BuscarEspecialidades/12/999";

//    $.ajax({
//        type: "GET",
//        url: url,
//        dataType: 'json',
//        contentType: "application/json; charset=utf-8",
//        success: function (obj) {
//            console.log(obj)
//            var htmx = "";

//            if (obj != null) {
//                var selectbox = $('#cmbEspecialidade');
//                selectbox.find('option').remove()
//                $.each(obj, function (i, d) {
//                    htmx += '<option value="' + d.id + '"> ' + d.nome + '</option > '
//                });
//                selectbox.html(htmx);
//            }
//        }
//    });
//}


$(document).ready(function () {
 
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

function nera() {
    var espc = $('#cmbEspecialidade :selected').val();

    function getDataMedico() {
        return {
            nome: $('#nome').val(),
            usuarioCriacao: 1,
            usuarioEdicao: 1,
            ativo: false,
            status: 1,
            idCliente: 12,
            idUsuario: 1555,
            codigoExterno: 1555,
            crm: $('#crm').val(),
            cpf: $('#cpf').val(),
            dataNascimento: $('#dataNascimento').val(),
            uf_crm: $('#cmbUf :selected').val(),
            especialidadeId: espc,
            telefone: {
                ativo: true,
                status: 1,
                idCliente: 12,
                numero: $('#telefone').val()
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
                idCliente: 12
            },
            idsClinicas: $('#clinicas').val()
        }
    }

    var medico = { 'medico': getDataMedico() };
    console.log(JSON.stringify(medico));
}