function loadPage(pageNumber) {
    var role = $('#roleFilter').val();
    var query = $('#searchBox').val();
    var pageSize = $('input[name="pageSize"]').val();

    $.get('/Admin/VerifiedUsers', { pageNumber: pageNumber, role: role, query: query, pageSize: pageSize })
        .done(function (data) {
            $('#userTableContainer').html(data);
            adjustFooterPadding();
        })
        .fail(function () {
            alert('Ошибка при загрузке пользователей.');
        });
}

function adjustFooterPadding() {
    const footerHeight = document.querySelector('footer').offsetHeight;
    const paginationNav = document.querySelector('#paginationNav');
    const mainElement = document.querySelector('main');

    if (paginationNav) {
        const distanceToBottom = window.innerHeight - paginationNav.getBoundingClientRect().bottom;
        if (distanceToBottom < footerHeight) {
            mainElement.style.paddingBottom = (footerHeight + 30) + 'px';
        }
    }
}

// Вызываем корректировку после загрузки страницы
window.addEventListener('DOMContentLoaded', adjustFooterPadding);
window.addEventListener('resize', adjustFooterPadding);
