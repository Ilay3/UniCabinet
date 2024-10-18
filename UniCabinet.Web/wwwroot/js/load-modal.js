function openModal(modalUrl, modalId, id = null) {
     let urlWithId = modalUrl;

    // Проверяем, передан ли id
    if (id !== null) {
        urlWithId = `${modalUrl}?id=${id}`; // Добавляем id к URL, если он есть
    }

    fetch(urlWithId)
        .then(response => response.text())
        .then(html => {
            document.getElementById("modalContainer").innerHTML = html;
            var modal = new bootstrap.Modal(document.getElementById(modalId));
            modal.show();
        })
        .catch(error => {
            console.error('Ошибка при открытии модального окна:', error);
        });
}
