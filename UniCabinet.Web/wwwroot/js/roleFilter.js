$(document).ready(function () {
    var selectedRole = $('#roleFilter').data('selected-role');

    if (selectedRole && selectedRole !== '') {
        $('#roleFilter').val(selectedRole);
    } else {
        $('#roleFilter').val('Student');
    }

    $('#roleFilter').change(function () {
        $('#roleFilterForm').submit();
    });
});
