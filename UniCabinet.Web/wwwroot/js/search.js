// search.js

let suggestionIndex = -1;  // Index for navigating the suggestion list

// Function to search users with autocomplete
function searchUsers() {
    var query = document.getElementById('searchBox').value.trim();
    var selectedRole = document.getElementById('roleFilter').value;
    var pageSize = document.querySelector('input[name="pageSize"]').value;

    if (query.length >= 2) {
        fetch(`/Admin/SearchUsers?query=${encodeURIComponent(query)}&role=${selectedRole}`)
            .then(response => response.json())
            .then(data => {
                var suggestionsList = document.getElementById('suggestionsList');
                suggestionsList.innerHTML = '';
                suggestionIndex = -1;

                if (data.length > 0) {
                    data.forEach((user, index) => {
                        var listItem = document.createElement('li');
                        listItem.className = 'list-group-item';
                        listItem.textContent = `${user.fullName} (${user.email})`;
                        listItem.setAttribute('data-user-id', user.id);
                        listItem.setAttribute('data-index', index);

                        listItem.onclick = function () { selectUser(user.id, index, pageSize); };
                        listItem.onmouseenter = function () {
                            clearSuggestionsHighlight();
                            listItem.classList.add('highlighted');
                            suggestionIndex = index;
                        };

                        suggestionsList.appendChild(listItem);
                    });
                } else {
                    // Если нет предложений, очищаем список предложений
                    suggestionsList.innerHTML = '';
                    // Можно добавить сообщение пользователю (по желанию)
                    // alert('Нет пользователей, удовлетворяющих условиям поиска.');
                    // Оставляем таблицу без изменений
                }
            });
    } else {
        // Очищаем список предложений, если длина запроса менее 2 символов
        document.getElementById('suggestionsList').innerHTML = '';
    }
}

// Function to clear previous highlights
function clearSuggestionsHighlight() {
    var suggestions = document.querySelectorAll('#suggestionsList .list-group-item');
    suggestions.forEach(function (item) {
        item.classList.remove('highlighted');
    });
}

// Function to select user from suggestions
function selectUser(userId, index, pageSize) {
    var pageNumber = Math.floor(index / pageSize) + 1;
    var selectedRole = document.getElementById('roleFilter').value;
    var query = document.getElementById('searchBox').value;

    // Save user ID to highlight after navigation
    sessionStorage.setItem('selectedUserId', userId);

    // Redirect to appropriate page
    window.location.href = `/Admin/VerifiedUsers?pageNumber=${pageNumber}&role=${selectedRole}&query=${encodeURIComponent(query)}&pageSize=${pageSize}`;
}

// Highlight selected user after page load
window.onload = function () {
    var selectedUserId = sessionStorage.getItem('selectedUserId');
    if (selectedUserId) {
        var selectedRow = document.querySelector(`tr[data-user-id="${selectedUserId}"]`);
        if (selectedRow) {
            selectedRow.scrollIntoView({ behavior: 'smooth', block: 'center' });
            selectedRow.classList.add('table-info');

            // Remove highlight after 5 seconds
            setTimeout(function () {
                selectedRow.classList.remove('table-info');
                sessionStorage.removeItem('selectedUserId');
            }, 5000);
        }
    }
}

function resetSearch() {
    var selectedRole = document.getElementById('roleFilter').value; // Получаем выбранную роль
    var pageSize = document.querySelector('input[name="pageSize"]').value; // Получаем размер страницы

    // Очищаем поле поиска
    document.getElementById('searchBox').value = '';

    // Перенаправляем на страницу без параметра `query`
    window.location.href = `/Admin/VerifiedUsers?role=${selectedRole}&pageSize=${pageSize}`;
}
