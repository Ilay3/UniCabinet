// wwwroot/js/search.js

let suggestionIndex = -1;  // Индекс для навигации по списку предложений

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
                suggestionIndex = -1;  // Сбрасываем индекс при новом поиске

                data.forEach((user, index) => {
                    var listItem = document.createElement('li');
                    listItem.className = 'list-group-item';
                    listItem.textContent = user.fullName + ' (' + user.email + ')';
                    listItem.setAttribute('data-index', index);  // Устанавливаем индекс для каждого элемента
                    listItem.onclick = function () { selectUser(user.id); };

                    // Добавляем обработчик для навигации по предложению
                    listItem.onmouseenter = function () {
                        clearSuggestionsHighlight();  // Очищаем предыдущие подсветки
                        listItem.classList.add('highlighted');  // Подсвечиваем текущий элемент
                        suggestionIndex = index;  // Обновляем индекс
                    };

                    suggestionsList.appendChild(listItem);
                });
            });
    } else {
        document.getElementById('suggestionsList').innerHTML = '';  // Очистка списка при удалении текста
    }
}

// Функция для выбора пользователя из списка
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

// Обработчик нажатий клавиш для списка предложений
document.getElementById('searchBox').addEventListener('keydown', function (event) {
    var suggestionsList = document.getElementById('suggestionsList');
    var suggestions = suggestionsList.querySelectorAll('.list-group-item');

    if (suggestions.length > 0) {
        if (event.key === 'ArrowDown') {
            // Перемещение вниз по списку
            suggestionIndex = (suggestionIndex + 1) % suggestions.length;
            clearSuggestionsHighlight();
            suggestions[suggestionIndex].classList.add('highlighted');
            suggestions[suggestionIndex].scrollIntoView({ behavior: 'smooth', block: 'nearest' });
        } else if (event.key === 'ArrowUp') {
            // Перемещение вверх по списку
            suggestionIndex = (suggestionIndex - 1 + suggestions.length) % suggestions.length;
            clearSuggestionsHighlight();
            suggestions[suggestionIndex].classList.add('highlighted');
            suggestions[suggestionIndex].scrollIntoView({ behavior: 'smooth', block: 'nearest' });
        } else if (event.key === 'Enter' && suggestionIndex >= 0) {
            // Выбор пользователя по клавише Enter
            suggestions[suggestionIndex].click();
        }
    }
});

// Функция для очистки подсветки предложений
function clearSuggestionsHighlight() {
    var suggestionsList = document.getElementById('suggestionsList');
    var suggestions = suggestionsList.querySelectorAll('.list-group-item');
    suggestions.forEach(suggestion => suggestion.classList.remove('highlighted'));
}
