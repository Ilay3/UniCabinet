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

        // Определяем URL в зависимости от IsTeacherView
        var isTeacherView = $('#isTeacherView').val() === 'true';
        var url = isTeacherView
            ? '/DisciplineDetails/TeacherDetails'
            : '/DisciplineDetails/TeacherDetailsWithUserId';

        // Отображение индикатора загрузки
        showLoadingIndicator();

        // AJAX-запрос
        $.ajax({
            url: url,
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
        $('#detailsTableContainer').append('<div class="loading">Загрузка...</div>');
    }

    function hideLoadingIndicator() {
        $('#detailsTableContainer .loading').remove();
    }
});
