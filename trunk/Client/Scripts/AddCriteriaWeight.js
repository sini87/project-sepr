$(document).ready(function () {
    var iCnt = 0;
    // CREATE A "DIV" ELEMENT AND DESIGN IT USING JQUERY ".css()" CLASS.
    var container = $(document.createElement('div')).css({
        padding: '5px', margin: '20px', width: '170px', border: '1px dashed',
        borderTopColor: '#999', borderBottomColor: '#999',
        borderLeftColor: '#999', borderRightColor: '#999'
    });

    $('#btAddCriteriaWeight').click(function () {
        if (iCnt <= 19) {
            iCnt = iCnt + 1;
            // ADD TEXTBOX.
            $(container).append('<input type=text class="getCriteriaWeight" id=criteriaWeight' + iCnt + ' ' +
                        'value="CriteriaWeight ' + iCnt + '" />');

            $('#criteriaWeight').before(container);
        }
        else {      // AFTER REACHING THE SPECIFIED LIMIT, DISABLE THE "ADD" BUTTON. (20 IS THE LIMIT WE HAVE SET)
            $(container).append('<label>Reached the limit</label>');
            $('#btAddCriteriaWeight').attr('class', 'critWeight-disable');
            $('#btAddCriteriaWeight').attr('disabled', 'disabled');
        }
    });
    $('#btRemoveCriteriaWeight').click(function () {   // REMOVE ELEMENTS ONE PER CLICK.
        if (iCnt != 0) { $('#critWeight' + iCnt).remove(); iCnt = iCnt - 1; }
        if (iCnt == 0) {
            $(container).empty();
            $(container).remove();
            $('#btSubmit').remove();
            $('#btAddCriteriaWeight').removeAttr('disabled');
            $('#btAddCriteriaWeight').attr('class', 'critWeight')
        }
    });
    $('#btRemoveAllCriteriaWeight').click(function () {    // REMOVE ALL THE ELEMENTS IN THE CONTAINER.
        $(container).empty();
        $(container).remove();
        $('#btSubmit').remove(); iCnt = 0;
        $('#btAddCriteriaWeight').removeAttr('disabled');
        $('#btAddCriteriaWeight').attr('class', 'critWeight');
    });
});