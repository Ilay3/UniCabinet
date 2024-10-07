document.addEventListener('DOMContentLoaded', function () {
    // Логика для управления модальными окнами
    var modals = document.querySelectorAll('.modal');

    modals.forEach(function (modal) {
        modal.addEventListener('shown.bs.modal', function () {
            var selectElement = modal.querySelector('select');
            if (selectElement) {
                selectElement.focus();
            }
        });
    });
});
