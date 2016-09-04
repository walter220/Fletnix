console.log("Movies");

var domUl = '.js-persons';
var loader = '.js-loading';
var persons;

$('.js-get-persons').click(function(e) {
    e.preventDefault();
    $(domUl).empty();
    $.ajax({
        headers: {
            name: $("#person").val()
        },

        url: "../api/persons-by-name",

        beforeSend: function() {
            console.log("lets do dis", $("#person").val());
            $(loader).toggleClass('hidden');
        },

        success: function(result) {
            updatePersons(result);
            $(loader).toggleClass('hidden');
        },

        error: function() {
            console.log("verdomme");
            $(loader).toggleClass('hidden');
        }
    });
});

var updatePersons = function (persons) {
    persons.forEach(function(person) {
        $(domUl).append(
            "<li class='list-group-item'><div class='result-name'>" +
            person.firstname + " " + person.lastname +
            "</div>" +
            "<a class='btn btn-default btn-select-director js-select-director'>" +
            "Add" +
            "</a>" +
            "<i class='fa fa-check hidden' aria-hidden='true'></i>" +
            "</li>"
        );
    });

    $('.js-select-director').click(function(e) {
        $(e.target).parent().find('.fa-check').toggleClass('hidden');
        $(e.target).parent().prependTo($(e.target).parent().parent());
    });
};

// "<button type='button' class='btn btn-default'>" +
// "Cast" +
// "</button>" +
// "As: " +
// "<input type='text' value=''/>" +
