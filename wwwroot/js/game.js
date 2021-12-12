$(() => {
    'use strict';

    $('#game-grid td:not(.completed)').click(function () {
        $('#game-grid').hide();

        const answerHandler = answer => {
            $.post('/answer', { answer }, result => {

                $('.score').text(result.playerPoints);

                $(this).addClass('completed').unbind();
                $('#question').hide();

                if (answer !== '') {
                    if (result.isCorrect) {
                        $('#answer .correct').show();
                    } else {
                        $('#answer .wrong').show();
                    }
                }

                $('#answer .text').text(result.correctAnswer);
                $('#answer').show();
            }, 'json');
        };

        $.post('/question', { questionId: $(this).data('question') }, text => {
            $('#question .text').text(text);
            $('#question').show();

            $('#question button.answer').unbind().click(() => answerHandler(prompt(text)));
            $('#question button.skip').unbind().click(() => answerHandler(''));
        });
    });

    $('#answer button').click(() => {
        $('#answer').hide();
        $('#answer .status').hide();
        $('#game-grid').show();

        if ($('#game-grid td:not(.completed)').length === 0) {
            location.reload();
        }
    });
});
