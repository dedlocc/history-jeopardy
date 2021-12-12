$(() => {
    'use strict';

    $('#game-grid td:not(.completed)').click(function () {
        $('#game-grid').hide();

        const answerHandler = answer => {
            $.post('/answer', {answer}, result => {
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

                $('#answer-input').hide();
                $('#answer .text').text(result.correctAnswer);
                $('#answer').show();
            }, 'json');
        };

        $('#answer-input form').unbind().submit(function () {
            const $answer = $(this).find('[name=answer]');
            answerHandler($answer.val());
            $answer.val('');

            return false;
        });

        $.post('/question', {questionId: $(this).data('question')}, question => {
            $('#question').html(question).show();

            /*if (question.picture) {
                $('#question .picture').attr('src', question.picture).show();
            } else {
                $('#question .picture').hide();
            }

            $('#question > button').show();
            $('#question').show();

            $('#question button.answer').unbind().click(() => {
                $('#question > button').hide();
                $('#answer-input').show();
            });

            $('#question button.skip').unbind().click(() => answerHandler(''));*/
        });
    });

    $('#answer button').unbind().click(() => {
        $('#answer').hide();
        $('#answer .status').hide();
        $('#game-grid').show();

        if ($('#game-grid td:not(.completed)').length === 0) {
            location.reload();
        }
    });
});
