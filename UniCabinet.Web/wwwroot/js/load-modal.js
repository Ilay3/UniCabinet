function openModal(modalUrl, modalId, id = null) {
    let urlWithId = modalUrl;

    // Если передан id, добавляем его к URL
    if (id !== null) {
        urlWithId = `${modalUrl}?id=${id}`;
    }

    fetch(urlWithId)
        .then(response => response.text())
        .then(html => {
            // Вставляем полученный HTML в контейнер
            document.getElementById("modalContainer").innerHTML = html;

            // Инициализируем и показываем модальное окно
            const modal = new bootstrap.Modal(document.getElementById(modalId));
            modal.show();

            // Удаляем модальное окно из DOM после закрытия
            document.getElementById(modalId).addEventListener('hidden.bs.modal', () => {
                document.getElementById("modalContainer").innerHTML = '';
            });
        })
        .catch(error => {
            console.error('Ошибка при загрузке модального окна:', error);
        });
}
