$(document).ready(function () {
    // Логика для управления модальными окнами
    $('.modal').on('shown.bs.modal', function () {
        $(this).find('select').focus();
    });
});