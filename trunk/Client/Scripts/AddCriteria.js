$(document).ready(function() {
    var iCnt = 0;
    // CREATE A "DIV" ELEMENT AND DESIGN IT USING JQUERY ".css()" CLASS.
    var container = $(document.createElement('div')).css({
        padding: '5px', margin: '20px', width: '170px'});

    $('#btAddCriteria').click(function() {
        if (iCnt <= 19) {
            iCnt = iCnt + 1;
            // ADD TEXTBOX.
            $(container).append('<input type=text class="getCriteria" id=criteria' + iCnt + ' ' +
                        'value="Criteria ' + iCnt + '" />');

            $('#criteria').before(container);
        }
        else {      // AFTER REACHING THE SPECIFIED LIMIT, DISABLE THE "ADD" BUTTON. (20 IS THE LIMIT WE HAVE SET)
            $(container).append('<label>Reached the limit</label>'); 
            $('#btAddCriteria').attr('class', 'crit-disable'); 
            $('#btAddCriteria').attr('disabled', 'disabled');
        }
    });
    $('#btRemoveCriteria').click(function() {   // REMOVE ELEMENTS ONE PER CLICK.
        if (iCnt != 0) { $('#crit' + iCnt).remove(); iCnt = iCnt - 1; }
        if (iCnt == 0) { $(container).empty(); 
            $(container).remove(); 
            $('#btSubmit').remove(); 
            $('#btAddCriteria').removeAttr('disabled'); 
            $('#btAddCriteria').attr('class', 'crit') 
        }
    });
    $('#btRemoveAllCriteria').click(function() {    // REMOVE ALL THE ELEMENTS IN THE CONTAINER.
        $(container).empty(); 
        $(container).remove(); 
        $('#btSubmit').remove(); iCnt = 0; 
        $('#btAddCriteria').removeAttr('disabled'); 
        $('#btAddCriteria').attr('class', 'crit');
    });
});

// PICK THE VALUES FROM EACH TEXTBOX WHEN "SUBMIT" BUTTON IS CLICKED.
var divValue, values = '';
function GetTextValue() {
    $(divValue).empty(); 
    $(divValue).remove(); values = '';
    $('.input').each(function() {
        divValue = $(document.createElement('div')).css({
            padding:'5px', width:'200px'
        });
        values += this.value + '<br />'
    });
    $(divValue).append('<p><b>Your selected values</b></p>' + values);
    $('body').append(divValue);
}