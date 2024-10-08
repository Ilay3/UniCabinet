$(function () {
    var selectedRole = $('#roleFilter').data('selected-role');

    if (selectedRole && selectedRole !== '') {
        $('#roleFilter').val(selectedRole);
    } else {
        $('#roleFilter').val('Student');
    }

    $('#roleFilter').on('change', function () { 
        $('#roleFilterForm').trigger('submit');
    });
});