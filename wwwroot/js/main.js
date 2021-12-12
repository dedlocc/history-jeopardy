/*function loadPage(page = '/') {
    $.post(page, {}, response => {
        $('main').html(response);
    });
}*/

$(() => {
    'use strict';

    for (const action of ['create', 'join']) {
        $(`#${action}-btn`).click(function () {
            $.ajax(`/${action}`, {
                method: 'GET',
                data: {
                    name: $('[name=username]').val(),
                    inviteCode: $('[name=invite-code]').val(),
                },
                success: inviteCode => location.assign('/room/' + inviteCode),
                error: () => alert('лох'),
            });
            return false;
        });
    }
});
