$(document).ready(function () {
    select2Dropdown('make-hdn', 'make', 'Search for make(s)', 'SearchMake', 'GetMake', true);
});
 
function select2Dropdown(hiddenID, valueID, ph, listAction, getAction, isMultiple) {
    var sid = '#' + hiddenID;
    $(sid).select2({
        placeholder: ph,
        minimumInputLength: 2,
        allowClear: true,
        multiple: isMultiple,
        ajax: {
            url: "/api/CommonApi/" + listAction,
            dataType: 'json',
            data: function (term, page) {
                return {
                    id: term // search term
                };
            },
            results: function (data) {
                return { results: data };
            }
        },
        initSelection: function (element, callback) {
            // the input tag has a value attribute preloaded that points to a preselected make's id
            // this function resolves that id attribute to an object that select2 can render
            // using its formatResult renderer - that way the make text is shown preselected
            var id = $('#' + valueID).val();
            if (id !== null && id.length > 0) {
                $.ajax("/api/CommonApi/" + getAction + "/" + id, {
                    dataType: "json"
                }).done(function (data) { callback(data); });
            }
        },
        formatResult: s2FormatResult,
        formatSelection: s2FormatSelection
    });
 
    $(document.body).on("change", sid, function (ev) {
        var choice;
        var values = ev.val;
        // This is assuming the value will be an array of strings.
        // Convert to a comma-delimited string to set the value.
        if (values !== null && values.length > 0) {
            for (var i = 0; i < values.length; i++) {
                if (typeof choice !== 'undefined') {
                    choice += ",";
                    choice += values[i];
                }
                else {
                    choice = values[i];
                }
            }
        }
 
        // Set the value so that MVC will load the form values in the postback.
        $('#' + valueID).val(choice);
    });
}
 
function s2FormatResult(item) {
    return item.text;
}
 
function s2FormatSelection(item) {
    return item.text;
}


Read more: http://www.intertech.com/Blog/selecting-multiple-items-using-select2-in-mvc-5/#ixzz4BpR02vkq 
    Follow us: @IntertechInc on Twitter | Intertech on Facebook