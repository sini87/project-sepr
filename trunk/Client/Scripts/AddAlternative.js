$(document).ready(function () {
    var iCnt = 0;
    // CREATE A "DIV" ELEMENT AND DESIGN IT USING JQUERY ".css()" CLASS.
    var container = $(document.createElement('div')).css({padding: '5px', margin: '20px', width: '170px'});

    $('#btAddAlt').click(function () {
        if (iCnt <= 19) {
            iCnt = iCnt + 1;
            // ADD TEXTBOX.
            $(container).append('<input type=text class="getAlternative" id=alternativeName' + iCnt + ' ' +
                        'value="Alternative Name ' + iCnt + '" />');
            $(container).append('<input type=text class="getAlternative" id=alternativeDesc' + iCnt + ' ' +
                        'value="Alternative Description' + iCnt + '" />');
            $(container).append('<input type=text class="getAlternative" id=alternativeReason' + iCnt + ' ' +
                        'value="Alternative Reason' + iCnt + '" />');

            $('#alternative').before(container);
        }
        else {      // AFTER REACHING THE SPECIFIED LIMIT, DISABLE THE "ADD" BUTTON. (20 IS THE LIMIT WE HAVE SET)
            $(container).append('<label>Reached the limit</label>');
            $('#btAddAlt').attr('class', 'alt-disable');
            $('#btAddAlt').attr('disabled', 'disabled');
        }
    });
    $('#btRemoveAlt').click(function () {   // REMOVE ELEMENTS ONE PER CLICK.
        if (iCnt != 0) { $('#alt' + iCnt).remove(); iCnt = iCnt - 1; }
        if (iCnt == 0) {
            $(container).empty();
            $(container).remove();
            $('#btAddAlt').removeAttr('disabled');
            $('#btAddAlt').attr('class', 'alt')
        }
    });
    $('#btRemoveAllAlt').click(function () {    // REMOVE ALL THE ELEMENTS IN THE CONTAINER.
        $(container).empty();
        $(container).remove();
        iCnt = 0;
        $('#btAddAlt').removeAttr('disabled');
        $('#btAddAlt').attr('class', 'alt');
    });
});