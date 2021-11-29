function loadPage(page)
{
    // TODO
}

$(() => {
    $('#player-form').submit(function() {
        $.post('/auth', {
            name: $(this).find('#username-input').val()
        }, () => {
            alert('PeepoHappy');
        });
        return false;
    });
});
