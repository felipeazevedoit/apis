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

function LoadingStop(element) {
    $(element).loading('stop');
}

function _arrayBufferToBase64(buffer) {
    var binary = '';
    var bytes = new Uint8Array(buffer);
    var len = bytes.byteLength;
    for (var i = 0; i < len; i++) {
        binary += String.fromCharCode(bytes[i]);
    }
    return window.btoa(binary);
}

var fileExtension;
var file;

$('#file').on('change', function () {
    LoadingInit('body');
    var files = $('#file').prop("files");

    var fileReader = new FileReader();
    if (fileReader && files && files.length) {
        fileReader.readAsArrayBuffer(files[0]);
        fileReader.onload = function () {
            var imageData = fileReader.result;
            console.log(imageData);
            file = _arrayBufferToBase64(imageData);

            if (file !== null && file !== '') {

                fileExtension = $("#file").val().split('.').pop();

                if (fileExtension == 'jpg') {
                    $('#userPhoto').attr('src', 'data:image/jpg;base64,' + file);
                }
                else if (fileExtension == 'png') {
                    $('#userPhoto').attr('src', 'data:image/png;base64,' + file);
                }
                else {
                    alert('Selecione uma imagem válida.');
                }
            }
        };
    }
    LoadingStop('body');
});

function atualizarUsuario() {
    LoadingInit('body');

    if (file == null || file == undefined) {
        file = $('#b64Img').val();
        fileExtension = $('#extension').val();
    }

    if ($('#senha').val() == $('#confirmarSenha').val()) {
        var usuario = {
            id: $('#id').val(),
            vAdmin: $('#vAdmin').val(),
            nome: $('#nome').val(),
            sobrenome: $('#sobrenome').val(),
            login: $('#login').val(),
            senha: $('#senha').val(),
            peso: $('#peso').val(),
            altura: $('#altura').val(),
            cpf: $('#cpf').val(),
            sexo: $("#cmbSexo option:selected").val(),
            termo: $("#termo").val(),
            profileAvatar: file,
            avatarExtension: fileExtension,
            convenio: $("#cbmConvenio option:selected").text(),        
            idTel: $('#idTel').val(),
            telefone: $('#telefone').val(),
            idEnd: $('#idEnd').val(),
            cep: $("#cep").val(),
            estado: $("#uf").val(),
            cidade: $("#cidade").val(),
            bairro: $("#bairro").val(),
            rua: $("#rua").val(),
            numero: $("#numero").val(),
            uf: $("#uf").val(),
            desc: $("#desc").val()
        };

        $.ajax({
            url: "/Usuarios/AlterarUsuario",
            type: 'POST',
            data: usuario
        }).done(function (msg) {
            console.log(msg);
            //$("#lblAtualizado").removeClass("fade");
            //$("#lblAtualizado").text(msg);
            //$("#lblSpan").removeClass("lblSpan");
            window.location.href = "/Paciente/Feed";
            $.notify(msg, "success");
        }).fail(function (jqXHR, textStatus, msg) {
            $.notify("Não foi possível completar a operação.", "error");
        }).always(function () {
            file = undefined;
            fileExtension = undefined;
            LoadingStop('body');
        });
    }
    else {
        $.notify("Senhas não correspondem.", "error");
        LoadingStop('body');
    }
}

function atualizarUsuarioMedico() {
    LoadingInit('body');

    if (file == null || file == undefined) {
        file = $('#b64Img').val();
        fileExtension = $('#extension').val();
    }

    if ($('#senha').val() == $('#confirmarSenha').val()) {
        var usuario = {
            id: $('#id').val(),
            vAdmin: $('#vAdmin').val(),
            nome: $('#nome').val(),
            sobrenome: $('#sobrenome').val(),
            login: $('#login').val(),
            senha: $('#senha').val(),
            peso: $('#peso').val(),
            altura: $('#altura').val(),
            cpf: $('#cpf').val(),
            sexo: $("#cmbSexo option:selected").val(),
            termo: $("#termo").val(),
            profileAvatar: file,
            avatarExtension: fileExtension,
            convenio: $("#cbmConvenio option:selected").text(),
            idTel: $('#idTel').val(),
            telefone: $('#telefone').val(),
            idEnd: $('#idEnd').val(),
            cep: $("#cep").val(),
            estado: $("#uf").val(),
            cidade: $("#cidade").val(),
            bairro: $("#bairro").val(),
            rua: $("#rua").val(),
            numero: $("#numero").val(),
            uf: $("#uf").val(),
            desc: $("#desc").val()
        };

        $.ajax({
            url: "/Usuarios/AlterarUsuarioMedico",
            type: 'POST',
            data: usuario
        }).done(function (msg) {
            console.log(msg);
            //$("#lblAtualizado").removeClass("fade");
            //$("#lblAtualizado").text(msg);
            //$("#lblSpan").removeClass("lblSpan");
            window.location.href = "/Home/Index";
            $.notify(msg, "success");
        }).fail(function (jqXHR, textStatus, msg) {
            $.notify("Não foi possível completar a operação.", "error");
        }).always(function () {
            file = undefined;
            fileExtension = undefined;
            LoadingStop('body');
        });
    }
    else {
        $.notify("Senhas não correspondem.", "error");
        LoadingStop('body');
    }
}