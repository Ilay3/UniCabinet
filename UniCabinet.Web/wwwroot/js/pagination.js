function loadPage(pageNumber) {
    var role = $('#roleFilter').val();
    var query = $('#searchBox').val();
    var pageSize = $('input[name="pageSize"]').val();


    // Выполняем AJAX-запрос для получения данных для указанной страницы
    $.get('/Admin/VerifiedUsers', { pageNumber: pageNumber, role: role, query: query, pageSize: pageSize })
        .done(function (data) {
            // Обновляем содержимое таблицы пользователей
            $('#userTableContainer').html(data);
        })
        .fail(function () {
            alert('Ошибка при загрузке пользователей.');
        });
}
