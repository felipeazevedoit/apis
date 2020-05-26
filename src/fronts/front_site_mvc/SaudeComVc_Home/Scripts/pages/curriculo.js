function valorFile() {
    console.log($("#curriculo").val());
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

function base64ToArrayBuffer(base64) {
    var binaryString = window.atob(base64);
    var binaryLen = binaryString.length;
    var bytes = new Uint8Array(binaryLen);
    for (var i = 0; i < binaryLen; i++) {
        var ascii = binaryString.charCodeAt(i);
        bytes[i] = ascii;
    }
    return bytes;
}

var fileExtension;
var file;
var bites;
var caminho;

$('#curriculo').on('change', function () {
    var files = $('#curriculo').prop("files");

    if ($("#curriculo").val() != null && $("#curriculo").val() != "") {
        $(".custom-file-label.imgC").text($("#curriculo").val());
    }

    caminho = $("#curriculo").val();
    console.log(caminho);


    var fileReader = new FileReader();
    if (fileReader && files && files.length) {
        fileReader.readAsArrayBuffer(files[0]);
        fileReader.onload = function () {
            var imageData = fileReader.result;
            console.log(imageData);
            file = _arrayBufferToBase64(imageData);
            console.log(file);
            bites = base64ToArrayBuffer(file);
            console.log(bites);

            //if (file !== null && file !== '') {

            //    fileExtension = $("#file").val().split('.').pop();

            //    if (fileExtension == 'jpg') {
            //        $('#userPhoto').attr('src', 'data:image/jpg;base64,' + file);
            //    }
            //    else if (fileExtension == 'png') {
            //        $('#userPhoto').attr('src', 'data:image/png;base64,' + file);
            //    }
            //    else {
            //        alert('Selecione uma imagem válida.');
            //    }
            //}
        };
    }
})

function enviarDados() {
    var documento = { Arquivo: bites };

    if (bites != null) {

        $.ajax({
            type: "POST",
            url: "/Curriculo/SaveDocumentoAsync/",
            data: { "base64": file },
            dataType: "json",
            success: function (response) {
                console.log(response);
            }
        });
        $("#curriculoB").notify("Curriculo enviado com sucesso", { position: "right", className: "success" });
    }
    else {
        $("#curriculoB").notify("Sem nenhum arquivo para realizar upload!", { position: "right" });
    }
}