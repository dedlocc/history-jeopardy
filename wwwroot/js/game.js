$(() => {

    $('#game-grid td:not(.completed)').click(function () {
        $('#game-grid').hide();

        const answerHandler = answer => {
            $.post('/answer', {answer}, result => {
                const prefix = answer ? (result.isCorrect ? 'Правильно' : 'Нет') + '. ' : '';

                $('.score').text(result.playerPoints);

                $(this).addClass('completed').unbind();
                $('#question').hide();

                $('#answer .text').text(prefix + result.correctAnswer);
                $('#answer').show();
            }, 'json');
        }

        const questionId = $(this).data('question');

        $.post('/question', {questionId}, text => {
            $('#question .text').text(text);
            $('#question').show();

            $('#question button.answer').unbind().click(() => answerHandler(prompt(text)));
            $('#question button.skip').unbind().click(() => answerHandler(null));
        });
    });
    
    $('#answer button').click(function () {
        $('#answer').hide();
        $('#game-grid').show();

        if ($('#game-grid td:not(.completed)').length === 0) {
            location.reload();
        }
    });
});
