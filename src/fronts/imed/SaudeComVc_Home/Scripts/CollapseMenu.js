//Stack menu when collapsed
$('#bs-example-navbar-collapse-1').on('show.bs.collapse', function () {
    $('.nav-pills').addClass('nav-stacked');
});

//Unstack menu when not collapsed
$('#bs-example-navbar-collapse-1').on('hide.bs.collapse', function () {
    $('.nav-pills').removeClass('nav-stacked');
});