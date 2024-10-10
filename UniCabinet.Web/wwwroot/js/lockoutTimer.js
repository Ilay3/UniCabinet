
function startLockoutTimer(remainingTime) {
    function updateTimer() {
        // Преобразуем время в секунды и минуты
        var totalSeconds = Math.floor(remainingTime / 1000);
        var minutes = Math.floor(totalSeconds / 60);
        var seconds = totalSeconds % 60;

        // Форматируем время для отображения
        var formattedTime = "";
        if (minutes > 0) {
            formattedTime = minutes + " минут " + seconds + " секунд";
        } else {
            formattedTime = seconds + " секунд";
        }

        // Отображаем на странице
        document.getElementById("lockout-timer").textContent = formattedTime;

        // Уменьшаем время на 1 секунду (1000 мс)
        remainingTime -= 1000;

        // Если время закончилось, перезагружаем страницу
        if (remainingTime <= 0) {
            location.reload();
        } else {
            // Обновляем таймер каждую секунду
            setTimeout(updateTimer, 1000);
        }
    }

    // Инициализируем таймер при вызове функции
    updateTimer();
}
