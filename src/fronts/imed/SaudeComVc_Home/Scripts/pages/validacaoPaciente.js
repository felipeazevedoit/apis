function cadastrarPaciente() {
    if ($("#nome").val() == "" || $("#sobrenome").val() == "" || $("#telefone").val() == "" || $("#local").val() == "" || $("#numero").val() == "" || $("#cep").val() == "" || $("#cpf").val() == "" || $("#peso").val() == "" || $("#altura").val() == "" || $("#email").val() == "" || $("#senha").val() == "" || $("#confirmarSenha").val() == "" || $("#ckbTermos").is(":checked") == false) {
        if ($("#nome").val() == "") {
            $("#lblNome").removeClass();
            $("#lblNome").addClass("ativo");
        }
        else {
            $("#lblNome").removeClass();
            $("#lblNome").addClass("off");
        }
        //-----------------------------------------------
        if ($("#sobrenome").val() == "") {
            $("#lblSobrenome").removeClass();
            $("#lblSobrenome").addClass("ativo");
        }
        else {
            $("#lblSobrenome").removeClass();
            $("#lblSobrenome").addClass("off");
        }
        //-----------------------------------------------
        if ($("#cpf").val() == "") {
            $("#lblCpf").removeClass();
            $("#lblCpf").addClass("ativo");
        }
        else {
            $("#lblCpf").removeClass();
            $("#lblCpf").addClass("off");
        }
        //-----------------------------------------------
        if ($("#peso").val() == "") {
            $("#lblPeso").removeClass();
            $("#lblPeso").addClass("ativo");
        }
        else {
            $("#lblPeso").removeClass();
            $("#lblPeso").addClass("off");
        }
        //-----------------------------------------------
        if ($("#altura").val() == "") {
            $("#lblAltura").removeClass();
            $("#lblAltura").addClass("ativo");
        }
        else {
            $("#lblAltura").removeClass();
            $("#lblAltura").addClass("off");
        }
        //-----------------------------------------------
        if ($("#email").val() == "") {
            $("#lblEmail").removeClass();
            $("#lblEmail").addClass("ativo");
        }
        else {
            $("#lblEmail").removeClass();
            $("#lblEmail").addClass("off");
        }
        //-----------------------------------------------
        if ($("#senha").val() == "") {
            $("#lblSenha").removeClass();
            $("#lblSenha").addClass("ativo");
        }
        else {
            $("#lblSenha").removeClass();
            $("#lblSenha").addClass("off");
        }
        //-----------------------------------------------
        if ($("#confirmarSenha").val() == "") {
            $("#lblConfirmarSenha").removeClass();
            $("#lblConfirmarSenha").addClass("ativo");
        }
        else {
            $("#lblConfirmarSenha").removeClass();
            $("#lblConfirmarSenha").addClass("off");
        }
        if ($("#ckbTermos").is(":checked") == false) {
            $("#lblTermos").removeClass();
            $("#lblTermos").addClass("ativo");
        }
        else {
            $("#lblTermos").removeClass();
            $("#lblTermos").addClass("off");
        }
        //---------------------------------------------
        if ($("#local").val() == "") {
            $("#lblLocal").removeClass();
            $("#lblLocal").addClass("ativo");
        }
        else {
            $("#lblLocal").removeClass();
            $("#lblLocal").addClass("off");
        }
        //-----------------------------------------------
        if ($("#numero").val() == "") {
            $("#lblNumero").removeClass();
            $("#lblNumero").addClass("ativo");
        }
        else {
            $("#lblNumero").removeClass();
            $("#lblNumero").addClass("off");
        }
        //-----------------------------------------------
        if ($("#cep").val() == "") {
            $("#lblCep").removeClass();
            $("#lblCep").addClass("ativo");
        }
        else {
            $("#lblCep").removeClass();
            $("#lblCep").addClass("off");
        }
        //-----------------------------------------------
        if ($("#telefone").val() == "") {
            $("#lblTelefone").removeClass();
            $("#lblTelefone").addClass("ativo");
        }
        else {
            $("#lblTelefone").removeClass();
            $("#lblTelefone").addClass("off");
        }
        //-----------------------------------------------
    }
    else {
        $("#lblNome").removeClass();
        $("#lblNome").addClass("off");
        $("#lblSobrenome").removeClass();
        $("#lblSobrenome").addClass("off");
        $("#lblCpf").removeClass();
        $("#lblCpf").addClass("off");
        $("#lblPeso").removeClass();
        $("#lblPeso").addClass("off");
        $("#lblAltura").removeClass();
        $("#lblAltura").addClass("off");
        $("#lblEmail").removeClass();
        $("#lblEmail").addClass("off");
        $("#lblSenha").removeClass();
        $("#lblSenha").addClass("off");
        $("#lblConfirmarSenha").removeClass();
        $("#lblConfirmarSenha").addClass("off");
        $("#lblLocal").removeClass();
        $("#lblLocal").addClass("off");
        $("#lblNumero").removeClass();
        $("#lblNumero").addClass("off");
        $("#lblCep").removeClass();
        $("#lblCep").addClass("off");
        $("#lblTelefone").removeClass();
        $("#lblTelefone").addClass("off");
        cadPaciente();
    }

}