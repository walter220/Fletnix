console.log('ready for rating!');

var ui = {
    btn: '.js-rating-btn',
    add: '.js-rating-add',
    modify: '.js-btn-modify',
    delete: '.js-btn-delete',
    rating: '#rating',
    message: '.js-rating-message'
}

// var htmlTemplates = {
//     reactionBlock: "" +
//         "<div class='list-group-item'>" +
//             "<h6>Reactie van: " + email + " geeft een " + rating + "</h6>" +
//             "<p>" + comment + "</p>" +
//             "<br />" +
//             "<button class='btn btn-warning js-btn-modify'>Wijzigen</button>" +
//             "<button class='btn btn-danger js-btn-delete'>Verwijderen</button>" +
//         "</div>",
//     reactionForm: "" +
//         "<form asp-antiforgery='true'>" +
//             "<input name='movieId' type='hidden' value='@Model.movie_id'/>" +
//             "<input name='email' type='hidden' value='@ViewBag.user.Email' />" +
//             "<div class='form-group js-rating'>" +
//                 "<label for='rating' class='control-label'>Waardering</label>" +
//                 "<input id='rating' name='rating' class='rating js-rating-star' " +
//                        "data-min='0' data-max='10' data-step='1' data-size='xs' " +
//                        "data-show-clear='false' data-show-caption='false'>" +
//             "</div>" +
//             "<div class='form-group'>" +
//                 "<label for='comment'>Opmerking</label>" +
//                 "<textarea id='comment' name='comment' class='form-control'></textarea>" +
//             "</div>" +
//             "<div class='form-group'>" +
//                 "<button class='btn btn-success js-rating-btn js-rating-add' disabled>Plaats reactie</button>" +
//             "</div>" +
//         "</form>"
// }

$(ui.rating).on('rating.change', function(event, value, caption) {
    if (value <= 0) {
        $(ui.add).prop("disabled", true);
    } else {
        $(ui.add).prop("disabled", false);
    }
});

var updateUI = function (movieId, email, rating, comment) {
    $('.js-reactions').append(
        "<div class='list-group-item' data-item-email='" + email + "'>" +
            "<h6>Reactie van: " + email + " geeft een " + rating + "</h6>" +
            "<p>" + comment + "</p>" +
            "<br />" +

            "<form asp-antiforgery='true'>" +
                "<div class='form-group'" +
                     "data-item-movieId='" + movieId +"'" +
                     "data-item-email='" + email + "'" +
                     "data-item-rating='" + rating + "'" +
                     "data-item-comment='" + comment + "'>" +
                     "<button class='btn btn-warning js-btn-modify'>Wijzigen</button>" +
                     "<button class='btn btn-danger js-btn-delete'>Verwijderen</button>" +
                "</div>" +
            "</form>" +
        "</div>"
    );

    $('.js-add-reaction').hide();
}

var addReaction = function () {
    var form = {
        '__RequestVerificationToken': $('input[name=__RequestVerificationToken]').val(),
        'movieId' : $('input[name=movieId]').val(),
        'email': $('input[name=email]').val(),
        'rating': $(ui.rating).val(),
        'comment': $('textarea[name=comment]').val()
    }

    $.ajax({
        type: "post",
        url: '/api/feedback/create',
        data: form,

        success: function(result) {
            updateUI(form.movieId, form.email, form.rating, form.comment);
        },

        error: function(err) {
            console.log("error", err);
        }
    });
};

// var updateReaction = function () {
//     $.ajax({
//         headers: {
//             rating: 0,
//             comment: "",
//             user: ""
//         },
//
//         url: "../api/person/update",
//
//         beforeSend: function() {
//             console.log("lets do dis");
//         },
//
//         success: function(result) {
//             console.log("awyeah", result);
//         },
//
//         error: function() {
//             console.log("verdomme");
//         }
//     });
// };

var deleteReaction = function () {
    var form = {
        '__RequestVerificationToken': $('input[name=__RequestVerificationToken]').val(),
        'movieId' : $(ui.delete).parent().data('item-movieid'),
        'email': $(ui.delete).parent().data('item-email')
    }

    $.ajax({
        type: "post",
        url: '/api/feedback/delete',

        data: form,

        success: function(result) {
            $("div.list-group-item[data-item-email='" + form.email + "']").remove();
        },
        error: function(err) {
            console.log("err", err);
        }
    });
}


$(ui.add).on('click', function(e) {
    e.preventDefault();
    addReaction();
});

$(ui.modify).on('click', function(e) {
    e.preventDefault();
});

$(ui.delete).on('click', function(e) {
    e.preventDefault();
    deleteReaction();
});
