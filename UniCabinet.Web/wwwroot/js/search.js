// wwwroot/js/search.js

// Функция для поиска пользователей с автопредложением
function searchUsers() {
    var query = document.getElementById('searchBox').value;
    var selectedRole = document.getElementById('roleFilter').value; // Получаем выбранную роль

    if (query.length >= 2) {  // Минимум 2 символа для запуска поиска
        fetch('/Admin/SearchUsers?query=' + query + '&role=' + selectedRole)  // Передаем также выбранную роль
            .then(response => response.json())
            .then(data => {
                var suggestionsList = document.getElementById('suggestionsList');
                suggestionsList.innerHTML = '';  // Очистка старых предложений
                data.forEach(user => {
                    var listItem = document.createElement('li');
                    listItem.className = 'list-group-item';
                    listItem.textContent = user.fullName + ' (' + user.email + ')';
                    listItem.onclick = function () { selectUser(user.id); };
                    suggestionsList.appendChild(listItem);
                });
            });
    } else {
        document.getElementById('suggestionsList').innerHTML = '';  // Очистка списка при удалении текста
    }
}

// Функция для выделения выбранного пользователя в основном списке
function selectUser(userId) {
    // Прокручиваем страницу к пользователю
    document.getElementById('searchBox').value = '';
    document.getElementById('suggestionsList').innerHTML = '';  // Очистка списка предложений
    var rows = document.querySelectorAll('.user-row');
    rows.forEach(row => {
        row.classList.remove('table-info');  // Убираем подсветку
    });
    var selectedRow = document.querySelector(`tr[data-user-id="${userId}"]`);
    if (selectedRow) {
        selectedRow.scrollIntoView({ behavior: 'smooth', block: 'center' });  // Прокрутка к пользователю
        selectedRow.classList.add('table-info');  // Подсвечиваем выбранного пользователя

        // Убираем подсветку через 5 секунд
        setTimeout(function () {
            selectedRow.classList.remove('table-info');
        }, 5000);  // 5000 миллисекунд = 5 секунд
    }
}
