<script>
     
        $('#login').focus();
     
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
     
     
     
        $('#senha').change(function () {
            $('#btnEntrar').focus();
        });
     
        $('#btnEntrar').on('click', function () {
            LoadingInit('body');
     
            var Url = "/Login/Login";
            var data = {
                login: $('#login').val(),
                senha: $('#senha').val()
            };
     
            if (data.senha == "" || data.senha == "") {
                $('#return').text('Digite um login e senha válidos!');
                LoadingStop('body');
     
            }
            else {
                var settings = {
                    "async": true,
                    "data": data,
                    "crossDomain": true,
                    "url": Url,
                    "method": "POST"
                }
     
                $.ajax(settings).done(function (response) {
                    if (response.mensagem == 'Ok') {
                        window.location.href = "/";
                    }
                    else {
                        $('#return').text(response.mensagem);
                        LoadingStop('body');
                    }
                });
     
            }
     
     
        });
     </script>