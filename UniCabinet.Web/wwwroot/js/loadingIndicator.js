// Функция для отображения индикатора загрузки
function showLoadingIndicator() {
    var loadingIndicator = document.getElementById('loadingIndicator');
    if (loadingIndicator) {
        loadingIndicator.style.display = 'block';
    }
}

function hideLoadingIndicator() {
    var loadingIndicator = document.getElementById('loadingIndicator');
    if (loadingIndicator) {
        loadingIndicator.style.display = 'none';
    }
}
// Автоматическое управление индикатором загрузки для всех AJAX-запросов
$(document).ajaxStart(function () {
    showLoadingIndicator();
}).ajaxStop(function () {
    hideLoadingIndicator();
});
