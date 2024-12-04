$(document).ready(function () {
    // Обработчик изменения фильтров
    $('#filterCourse, #filterGroup, #filterSemester').on('change', function () {
        updateDetailsTable();
    });

    function updateDetailsTable() {
        var disciplineId = $('#disciplineId').val();
        var teacherId = $('#teacherId').val();
        var courseId = $('#filterCourse').val();
        var groupId = $('#filterGroup').val();
        var semesterId = $('#filterSemester').val();

        // Отображение индикатора загрузки
        showLoadingIndicator();

        // AJAX-запрос
        $.ajax({
            url: '/DisciplineDetails/TeacherDetails',
            type: 'GET',
            data: {
                disciplineId: disciplineId,
                teacherId: teacherId,
                courseId: courseId,
                groupId: groupId,
                semesterId: semesterId,
                isPartial: true
            },
            success: function (data) {
                $('#detailsTableContainer').html(data);
                hideLoadingIndicator();
            },
            error: function () {
                alert('Ошибка при загрузке данных.');
                hideLoadingIndicator();
            }
        });
    }

    function showLoadingIndicator() {
        // Можно добавить спиннер или затенение
        $('#detailsTableContainer').append('<div class="loading">Загрузка...</div>');
    }

    function hideLoadingIndicator() {
        // Удаляем индикатор загрузки
        $('#detailsTableContainer .loading').remove();
    }
});
