// Grab userID from hidden field (will be '0' if not logged in)        
var userID = document.getElementById('UserID').value;

// Enable only if user is logged in. 0 means not logged in. 
if (userID == 0) {
    $('.buttonAddCommentRate').attr('disabled', true);
    $('.span-buttonAddCommentRate').show();
}
else {
    $('.buttonAddCommentRate').attr('disabled', false);
    $('.span-buttonAddCommentRate').hide();
}

// Star Rating
var starCaptions = {
    0.5: 'hell (10)', 1: 'blackhole (20)', 1.5: 'crap (30)', 2: 'mostly bad (40)', 2.5: 'not happy (50)', 3.0: 'average (60)', 3.5: 'satisfied (70)', 4: 'terrific (80)', 4.5: 'love it (90)', 5: 'heavenly (100)'
}

$(".rating").rating({ starCaptions: starCaptions }, function () {
    var id = $(this).data('id');
    //$(this).rating({ starCaptions: starCaptions });

    console.log('this is ID: ' + id);
});

$('.rating').on('rating.change', function (event, value, caption) {
    $(".buttonSubmit").removeAttr("disabled").css({ "background-color": "#A2FACA", "width": "80px", "height": "35px" });
    $("#span-enable-message").hide();
});

$('.rating').on('rating.clear', function (event, value, caption) {
    $(".buttonSubmit").attr("disabled", "disabled").css({ "background-color": "#E8E8E8", "width": "60px", "height": "25px" });
    $("#span-enable-message").show();
});

// submit rating and/or comment
$(".buttonSubmit").click(function () {        
    var value = $(this).closest('.answer-rateIt').find('.rating').val();
    var answerCount = $(this).closest('.answer-container').find('.answer-count');
    var answerScoreFinal = $(this).closest('.answer-container').find('.span-answer-eusScore');
    var thankYou = $(this).closest('.answer-container').find('.answer-thankYou');
    var buttonAddCommentRate = $(this).closest('.answer-container').find('.buttonAddCommentRate');
    var comment = $(this).closest('.answer-commentRateBox').find('.answer-textArea').val(); // get comment text

    // Translate rating value to score value (e.g., value = 4 > score = 80)
    var score = eusCommon.rating(value);
    
    alert('value: ' + value + '| score: ' + score);

    // Create json array with UserID, AnswerID, and Rating
    var obj = {};
    obj["UserID"] = userID;
    obj["AnswerID"] = $(this).data('id');
    obj["Rating"] = score;
    obj["Comment"] = comment;

    // Create json string
    var data = JSON.stringify(obj);

    // Create json to send to API
    var jsonString = JSON.stringify({ "Filter": "SetRating", "FilterData": data });
    var url = "/eusVote/Topic/Topic";

    var result = eusAPI.post(url, jsonString);

    if (result.error == 'false') {
        alert('error is false');
        answerCount.text(result.count);
        answerScoreFinal.text(Math.round(result.score));
        $(".answer-commentRateBox").slideUp(300);
        thankYou.show(300);
        buttonAddCommentRate.text('Add comment/rate');
    }
    else {
        var error = {}; // Create object to hold error details
        error["user"] = "test";  // get userName
        error["detail"] = result.message;
        error["action"] = "trying to SetRating for topic for AnswerID: " + obj.AnswerID;
        error["errorLocation"] = "eusVote_TopicController > Index > script section (should later move to js file)";
        error["level"] = "high";

        //alert('error on callback for SetRating');

        // Create json string
        var errorData = JSON.stringify(error);

        // call function to handle/log errors.
        eusAPI.errorHandle(errorData);

        // send user a friendly error message !!!!!
    }



    //$.ajax({
    //    type: "post",                
    //    url: "/eusVote_Topic/Topic",
    //    data: jsonString,
    //    contentType: "application/json; charset=UTF-8",
    //    dataType: "json"
    //})
    //    .done(function (result) {                                       
    //        if (result.error == 'false') {
    //            // alert('error is false');                        
    //            answerCount.text(result.count);
    //            answerScoreFinal.text(Math.round(result.score));
    //            $(".answer-commentRateBox").slideUp(300);
    //            thankYou.show(300);                                                
    //            buttonAddCommentRate.text('Add comment/rate');
    //         }
    //        else {
    //            var error = {}; // Create object to hold error details
    //            error["user"] = "test";  // get userName
    //            error["detail"] = result.message;
    //            error["action"] = "trying to SetRating for topic for AnswerID: " + obj.AnswerID;
    //            error["errorLocation"] = "eusVote_TopicController > Index > script section (should later move to js file)";
    //            error["level"] = "high";

    //            alert('error on callback for SetRating');

    //            // Create json string
    //            var jsonErrorString = JSON.stringify(error);

    //            // call function to handle/log errors.
    //            eusCommon.errorHandle(jsonErroString);
    //        }
    //    })
    //});
});


$("#answer-rating-input").keyup(function () {
    $("#span-answer-yourScore").text($("#answer-rating-input").val());
});

//// Comments
//$(".buttonSeeComments").click(function () {
//    var id = $(this).data('id');
//    var displayArea = $(this).closest('.answer-container').find('.answer-displayComments');

//    if ($(this).text() == 'Best comments') {
//        displayArea.hide(300);
//        $(this).text('Hide comments');               
//    }
//    else {
//        $(this).text('Best comments');
//        displayArea.show(300);
//        displayArea.empty();
//    }
//});

// Add comment/rate. Open and close the comment/rate box.
$(".buttonAddCommentRate").click(function () {
    //var id = $(this).data('id');
    var crBox = $(this).closest('.answer-container').find('.answer-commentRateBox');
    //var crBox = $(this).closest('.answer-container').find('.test');
    var buttonText = $(this).text();

    if (buttonText == 'Add comment/rate') {
        crBox.slideDown(300);
        $(this).text('Cancel comment/rate');
    }
    else {
        crBox.slideUp(300);
        $(this).text('Add comment/rate');
    }

});


// Comment cancel
$(".buttonCancel").click(function () {
    var crBox = $(this).closest('.answer-container').find('.answer-commentRateBox'); // find comment/rate box
    var buttonAddCommentRate = $(this).closest('.answer-container').find('.buttonAddCommentRate');  // find button

    crBox.slideUp(300);
    buttonAddCommentRate.text('Add comment/rate');
});