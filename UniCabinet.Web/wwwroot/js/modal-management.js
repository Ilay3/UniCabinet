// Современный подход к инициализации
$(function () {
    // Ваш код инициализации
    $('.btn-info').on('click', function () {
        var targetModal = $(this).data('bs-target');
        $(targetModal).modal('show');
    });

    $('.btn-secondary').on('click', function () {
        var targetModal = $(this).data('bs-target');
        $(targetModal).modal('show');
    });
});
