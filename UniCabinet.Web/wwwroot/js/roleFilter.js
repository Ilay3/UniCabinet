$(function () {
    var selectedRole = $('#roleFilter').data('selected-role');

    if (selectedRole && selectedRole !== '') {
        $('#roleFilter').val(selectedRole);
    } else {
        $('#roleFilter').val('Student');
    }

    $('#roleFilter').on('change', function () {
        var role = $(this).val();
        var query = $('#searchBox').val();
        var pageNumber = 1;
        var pageSize = $('input[name="pageSize"]').val();

        // Отправка AJAX-запроса для получения отфильтрованных пользователей
        $.get('/Admin/VerifiedUsers', { role: role, query: query, pageNumber: pageNumber, pageSize: pageSize })
            .done(function (data) {
                // Обновляем содержимое таблицы
                $('#userTableContainer').html(data);
            })
            .fail(function () {
                alert('Ошибка при загрузке списка пользователей.');
            });
    });
});
