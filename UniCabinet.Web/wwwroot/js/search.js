let suggestionIndex = -1;  // Индекс для навигации по списку предложений

// Функция для поиска пользователей с автопредложением
function searchUsers() {
    var query = document.getElementById('searchBox').value;
    var selectedRole = document.getElementById('roleFilter').value; // Получаем выбранную роль
    var pageSize = document.querySelector('input[name="pageSize"]').value; // Получаем размер страницы

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
                    listItem.setAttribute('data-user-id', user.id);  // Сохраняем ID пользователя
                    listItem.setAttribute('data-index', index);  // Сохраняем индекс пользователя для вычисления страницы
                    listItem.onclick = function () { selectUser(user.id, index, pageSize); };

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
function selectUser(userId, index, pageSize) {
    // Вычисляем номер страницы на основе индекса и размера страницы
    var pageNumber = Math.floor(index / pageSize) + 1;
    var selectedRole = document.getElementById('roleFilter').value; // Получаем текущую роль
    var query = document.getElementById('searchBox').value; // Получаем текущий запрос

    // Сохраняем ID пользователя в sessionStorage, чтобы после перехода подсветить его
    sessionStorage.setItem('selectedUserId', userId);

    // Переадресация на нужную страницу с учетом роли, номера страницы и запроса
    window.location.href = `/Admin/VerifiedUsers?pageNumber=${pageNumber}&role=${selectedRole}&query=${query}&pageSize=${pageSize}`;
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

// Функция для подсветки выбранного пользователя после перехода на нужную страницу
window.onload = function () {
    var selectedUserId = sessionStorage.getItem('selectedUserId');
    if (selectedUserId) {
        var selectedRow = document.querySelector(`tr[data-user-id="${selectedUserId}"]`);
        if (selectedRow) {
            selectedRow.scrollIntoView({ behavior: 'smooth', block: 'center' });  // Прокрутка к пользователю
            selectedRow.classList.add('table-info');  // Подсвечиваем выбранного пользователя

            // Убираем подсветку через 5 секунд
            setTimeout(function () {
                selectedRow.classList.remove('table-info');
                sessionStorage.removeItem('selectedUserId');  // Очищаем после использования
            }, 5000);  // 5000 миллисекунд = 5 секунд
        }
    }
}
