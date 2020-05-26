function consulteseAgr() {
    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        url: '/TelDoctor/VerificarResultado/',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            //var win = window.open('https://teldoctor.com.br/', '_blank');
            //if (win) {
            //    //Browser has allowed it to be opened
            //    win.focus();
            //} else {
            //    //Browser has blocked it
            //    console.log("Ouve um erro durante o redirecionamento para a teldoctor.");
            //}
            carregarModalTelDoctor(data);
        }
    });
}

function carregarModalTelDoctor(data) {

    var settings = {
        "url": "/TelDoctor/_TelDoctor",
        "method": "GET",
    }

    $.ajax(settings).done(function (response) {
        $('#modal').html(response);//home
        closeModal();
        $('#modalTelDoctor').modal('show');//partial
        temporizador(data);
    });

}

function temporizador(data) {
        var time = 5;

        var interval = setInterval(function () {
            if (time >= 0) {
            document.getElementById('countdown').innerHTML = time--;
        }
            else {
                //realizarLogin(data);
                var win = window.open('https://teldoctor.com.br/', '_blank');
                $("#redirecionar").removeClass('fade')
                $("#redirecionar").fadeIn();
                win.focus();
                clearInterval(interval);
    }
}, 1000);
};


function realizarLogin(result) {

    function getDataResult() {
        return {
        client_id: result.Login,
        bearer: result.AccessToken
        }
    };

    console.log(getDataResult());

    var url = "http://teldoctor:sucesso@qc.teldoctor.com.br/auth/api/99999";

    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        url: url,
        type: 'POST',
        data: JSON.stringify(getDataResult()),
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            console.log(data)
        }
    });
}