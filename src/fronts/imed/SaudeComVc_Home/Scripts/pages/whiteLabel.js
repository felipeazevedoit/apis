
$("#inicio").mask("00/00/0000");
$("#saida").mask("00/00/0000");

ListarGaleria();
loadclickImg();
//ListarHistoricoProfissional();

function carregaModalConfirma(id) {

    var settings = {
        "url": "/WhiteLabel/_Deletar",
        "method": "GET",
    }

    $.ajax(settings).done(function (response) {
        $('#modalConfirma').modal('show');//partial
        $('#modal').html(response);//home
        $('#modalConfirma').modal('show');//partial
        $("#idFoto").val(id);
    });

}

function excluirFoto(id) {
    Loading("body");
    $('#imagens').html("");
    $.ajax({
        url: '/Midias/ExcluirAsync/' + id,
        processData: false,
        contentType: false,
        type: 'GET',
        success: function (data) {
            // console.log(data);
            //$('#modalConfirma').modal('hide');
            //$('#imagens').html(data);
            location.reload();
        }
    }).always(function () {
        LoadingStop('body');
    });
}

$('#btnSalvar').click(function () {

    Loading('body');
    $('#wlForm').submit();


    //var data = {codigoExterno: $('#codExterno').val() };
    //$.ajax({
    //    url: "HistoricoProfissional/_Listagem",
    //    headers: {
    //        'Accept': 'application/json',
    //        'Content-Type': 'application/json'
    //    },
    //    type: 'GET',
    //    data: JSON.stringify(data),
    //    success: function (result) {
    //        console.log(result);
    //        if (result != null) {

    //            $('#dvHistoricos').html("");
    //            $('#dvHistoricos').html(result);
    //        }



    //    }
    //});

});

$('#btnCadastrar').click(function () {

    //Loading('body');
    ////$('#form').submit();


    //var data = { codigoExterno: $('#codExterno').val() };
    //$.ajax({
    //    url: "HistoricoProfissional/_Listagem",
    //    headers: {
    //        'Accept': 'application/json',
    //        'Content-Type': 'application/json'
    //    },
    //    type: 'GET',
    //    data: JSON.stringify(data),
    //    success: function (result) {
    //        console.log(result);
    //        if (result != null) {

    //            $('#dvHistoricos').html("");
    //            $('#dvHistoricos').html(result);
    //        }



    //    }
    //});

});

function SubmitButtonOnclick() {
    Loading('body');
    var formData = new FormData();
    var imagefile = document.getElementById("upload").files[0];
    formData.append("filebase", imagefile);
    var xhr = new XMLHttpRequest();
    xhr.open("POST", "/Midias/CadastrarGaleriaAsync", true);
    xhr.addEventListener("load", function (evt) { UploadComplete(evt); }, false);
    xhr.addEventListener("error", function (evt) { UploadFailed(evt); }, false);
    xhr.send(formData);

}

function UploadComplete(evt) {
    if (evt.target.status == 200) {
        ListarGaleria();
        $("#sucessImg").removeClass("fade");
        LoadingStop('body');
    }


    else {
        alert("Erro ao fazer upload do arquivo, tente novamente.");

        LoadingStop('body');
    }
}

function Loading(elemento) {
    $(elemento).loading({
        theme: 'dark',
        message: 'Aguarde...'
    });
}

function LoadingStop(elemento) {
    $(elemento).loading('stop');
}

function ListarGaleria() {
    Loading("body");
    $('#imagens').html("");

    $.ajax({
        url: '/Midias/_ListarGaleriaAsync',
        processData: false,
        contentType: false,
        type: 'GET',
        success: function (data) {
            // console.log(data);
            $('#imagens').html(data);
            loadclickImg();
            LoadingStop('body');

        }
    });
}

function loadclickImg() {
    $('.Image').click(function () {
        //Loading("body");
        ////alert($(this).attr('id'));
        //$('#imagens').html("");
        //var id = $(this).attr('id');
        //$.ajax({
        //    url: '/Midias/ExcluirAsync/' + id,
        //    processData: false,
        //    contentType: false,
        //    type: 'GET',
        //    success: function (data) {
        //        // console.log(data);
        //        $('#imagens').html(data);

        //    }
        //}).always(function () {
        //    LoadingStop('body');
        //});
        var id = $(this).attr('id');
        carregaModalConfirma(id);
    });

}

function ListarHistoricoProfissional() {
    // _Listagem
    //dvHistoricos
    $.ajax({
        url: '/HistoricoProfissional/_Listagem',
        processData: false,
        contentType: false,
        type: 'GET',
        success: function (data) {
            $('#dvHistoricos').html(data);
            loadclickImg();
            LoadingStop('body');
            $('#dvHistoricos').fadeIn();
        }
    });
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

$('#logo').on('change', function () {
    var files = $('#logo').prop("files");

    var fileReader = new FileReader();
    if (fileReader && files && files.length) {
        fileReader.readAsArrayBuffer(files[0]);
        fileReader.onload = function () {
            var imageData = fileReader.result;
            console.log(imageData);
            file = _arrayBufferToBase64(imageData);

            if (file !== null && file !== '') {

                fileExtension = $("#logo").val().split('.').pop();
                console.log(fileExtension);

                if (fileExtension == 'jpg') {
                    $('#fotoLogo').attr('src', 'data:image/jpg;base64,' + file);
                }
                else if (fileExtension == 'png') {
                    $('#fotoLogo').attr('src', 'data:image/png;base64,' + file);
                }
                else {
                    alert('Selecione uma imagem válida.');
                }
                salvarLogo();
            }
        };
    }
});

function salvarLogo() {
    var logo = {
        ArquivoB64: file,
        Extensao: fileExtension
    };

    $.ajax({
        url: "/Midias/SalvarLogoAsync",
        type: 'POST',
        data: logo
    }).done(function (msg) {
        console.log(msg);
    }).fail(function (msg) {
        alert(msg);
    }).always(function () {
        file = undefined;
        fileExtension = undefined;
    });
}

function carregaModalHistorico() {

    var settings = {
        "url": "/Medico/_Historico/?medicoId=" + $("#medicoId").val() + "&medicoNome=" + $("#medicoNome").val(),
        "method": "GET",
    };

    $.ajax(settings).done(function (response) {
        $('#historicoModal').html(response);
        $('#historico').modal('show');
    });

}