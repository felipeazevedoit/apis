$(document).ready(function() {
    var notifications = $('body').ttwSimpleNotifications(),
    msgs = [
        'Baixe diversas apostilas no yeeba.me',
        'Tenha um forum personalizado apenas com temas do seu curso',
        'Descubra quem gosta das mesmas coisas que voce',
        'Aprenda a cada dia com o site'
    ];

    notifications.show({msg:'Nos ajude enviando um e-mail com sugestoes: yeebame@gmail.com', autoHideDelay:6000});

    setTimeout(function() {
        notifications.show({msg:'Acesse o melhor site de tutoriais da internet YeebaPlay', icon:'images/icon.png'});
    }, 13000);

    setTimeout(function() {
        notifications.show({msg:'Qual o melhor site para estudo? <a href="http://yeeba.me/" target="_blank">yeeba.me</a> diversas apostilas e forum personalizado.', icon:'images/icon.png', autoHide:false});

        setTimeout(function() {
            notifications.show({msg:'Obrigado por escolher nosso site <a href="http://www.yeebaplay.com.br/blog/" target="_blank">YeebaPlay</a>', icon:'images/icon.png', autoHide:false});

        }, 5000);
    }, 2000);

    var i = 0;
    setInterval(function(){
        notifications.show({msg:msgs[i], icon:'images/icon.png'});
        i++;

        if(i >= msgs.length)
            i=0;
    }, 8000);
});