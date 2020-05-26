$(function () {
    alert("nera");
    var time = 30;

    var interval = setInterval(function () {
        if (time >= 0) {
            document.getElementById('countdown').innerHTML = time--;
        }
        else {
            alert("Nera")
            clearInterval(interval);
        }
    }, 1000);

});