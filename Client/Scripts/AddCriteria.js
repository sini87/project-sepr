﻿$(document).ready(function() {
    var iCnt = 0;
    // CREATE A "DIV" ELEMENT AND DESIGN IT USING JQUERY ".css()" CLASS.
    var container = $(document.createElement('div')).css({padding: '5px', margin: '20px', width: '170px'});

    $('#btAddCriteria').click(function() {
        if (iCnt <= 19) {
            iCnt = iCnt + 1;
            // ADD TEXTBOX.
            $(container).append('<input type=text class="getCriteria" id=criteriaName' + iCnt + ' ' +
                        'value="Criteria Name ' + iCnt + '" />');
            $(container).append('<input type=text class="getCriteria" id=criteriaDesc' + iCnt + ' ' +
                        'value="Criteria Description' + iCnt + '" />');
            
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
            $('#btAddCriteria').removeAttr('disabled'); 
            $('#btAddCriteria').attr('class', 'crit') 
        }
    });
    $('#btRemoveAllCriteria').click(function() {    // REMOVE ALL THE ELEMENTS IN THE CONTAINER.
        $(container).empty(); 
        $(container).remove(); 
        iCnt = 0; 
        $('#btAddCriteria').removeAttr('disabled'); 
        $('#btAddCriteria').attr('class', 'crit');
    });
});
