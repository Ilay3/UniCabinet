$(function () {
    // Обработчик клика по элементам с атрибутом data-url
    $('body').on('click', '[data-url]', function (e) {
        e.preventDefault();

        var $trigger = $(this);
        var url = $trigger.data('url');
        var dataToSend = $trigger.data();

        // Удаляем ненужные данные из dataToSend
        delete dataToSend.url;

        // Показываем индикатор загрузки перед началом AJAX-запроса
        showLoadingIndicator();

        $.get(url, dataToSend, function (data) {
            // Скрываем индикатор загрузки после завершения AJAX-запроса
            hideLoadingIndicator();

            // Добавляем полученное модальное окно в конец body
            var $modal = $(data);

            // Добавляем модальное окно в DOM
            $('body').append($modal);

            // Показываем модальное окно
            $modal.modal('show');

            // При закрытии модального окна удаляем его из DOM
            $modal.on('hidden.bs.modal', function () {
                $modal.remove();
            });
        }).fail(function () {
            // Скрываем индикатор загрузки в случае ошибки
            hideLoadingIndicator();
            alert('Произошла ошибка при загрузке данных.');
        });
    });

    // Обработка отправки формы внутри модального окна
    $('body').on('submit', '.modal form', function (e) {
        e.preventDefault();

        var $form = $(this);
        var url = $form.attr('action');

        // Показываем индикатор загрузки перед началом AJAX-запроса
        showLoadingIndicator();

        $.post(url, $form.serialize(), function (data) {
            // Скрываем индикатор загрузки после завершения AJAX-запроса
            hideLoadingIndicator();

            if (data.success) {
                // Закрываем модальное окно
                $form.closest('.modal').modal('hide');
                // Перезагружаем страницу или обновляем данные
                location.reload();
            } else {
                // Обновляем содержимое модального окна с ошибками
                $form.closest('.modal-content').html(data);
            }
        }).fail(function (xhr) {
            // Скрываем индикатор загрузки в случае ошибки
            hideLoadingIndicator();
            // Обработка ошибок сервера
            $form.closest('.modal-content').html(xhr.responseText);
        });
    });
});
