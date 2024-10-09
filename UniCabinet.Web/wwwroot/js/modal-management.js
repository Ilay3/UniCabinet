
document.addEventListener('DOMContentLoaded', function () {
    var modals = document.querySelectorAll('.modal');

    modals.forEach(function (modal) {
        modal.addEventListener('shown.bs.modal', function () {
            var firstInput = modal.querySelector('input');
            if (firstInput) {
                firstInput.focus();
            }
        });
    });


    document.querySelectorAll('.open-user-detail').forEach(function (button) {
        button.addEventListener('click', function () {
            var userId = button.getAttribute('data-user-id');
            fetch(`/Admin/GetUserDetails?userId=${userId}`)
                .then(response => response.text())
                .then(html => {
                    var modalContainer = document.createElement('div');
                    modalContainer.innerHTML = html;
                    document.body.appendChild(modalContainer);

                    var userDetailModal = new bootstrap.Modal(document.getElementById('userDetailModal'));
                    userDetailModal.show();

                    // Event listener for saving changes
                    document.getElementById('saveUserDetails').addEventListener('click', function () {
                        saveUserDetails();
                    });
                })
                .catch(error => {
                    console.error('Error loading user details:', error);
                });
        });
    });
});

function saveUserDetails() {
    var form = document.getElementById('userDetailForm');
    var formData = new FormData(form);

    fetch('/Admin/UpdateUserDetails', {
        method: 'POST',
        body: formData
    })
        .then(response => {
            if (response.ok) {
                alert('Данные пользователя успешно обновлены.');
                location.reload(); // Перезагрузить страницу
            } else {
                alert('Произошла ошибка при обновлении данных.');
            }
        })
        .catch(error => {
            console.error('Error saving user details:', error);
        });
}
