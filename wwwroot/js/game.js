$(() => {
    'use strict';

    $('#game-grid td:not(.completed)').click(function () {
        $('#game-grid').hide();

        const answerHandler = answer => {
            $.post('/answer', {answer}, result => {
                $('.score').text(result.playerPoints);

                $(this).addClass('completed').unbind();
                $('#question').html('');

                if (answer !== '') {
                    if (result.isCorrect) {
                        $('#answer-result .correct').show();
                    } else {
                        $('#answer-result .wrong').show();
                    }
                }

                $('#answer-result .text').text(result.correctAnswer);
                $('#answer-result').show();
            }, 'json');
        };

        $.post('/question', {questionId: $(this).data('question')}, question => {
            $('#question').html(question).show();

            $('#question button.answer').click(() => {
                $('#question > button').hide();

                $('#answer-input').submit(function () {
                    const $answer = $(this).find('[name=answer]');
                    if ($answer.prop('type') === 'text') {
                        answerHandler($answer.val());
                    } else {
                        answerHandler($answer.filter(':checked').map(function () {
                            return $(this).next().text();
                        }).toArray().join('|'));
                    }
                    return false;
                }).show();
            });

            $('#question button.skip').click(() => answerHandler(''));
        });
    });

    $('#answer-result button').unbind().click(() => {
        $('#answer-result').hide();
        $('#answer-result .status').hide();
        $('#game-grid').show();

        if ($('#game-grid td:not(.completed)').length === 0) {
            location.reload();
        }
    });
});
