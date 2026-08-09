$(function() {

    // Get the form.
    var form = $('#contact-form');

    // Get the messages div.
    var formMessages = $('.ajax-response');

    // Set up an event listener for the contact form.
    $(form).submit(function(e) {
        // Stop the browser from submitting the form.
        e.preventDefault();

        // Serialize the form data.
        var formData = $(form).serialize();

        // Submit the form using AJAX.
        $.ajax({
            type: 'POST',
            url: $(form).attr('action'),
            data: formData,

            beforeSend: function() {
                // Show waiting message before sending
                $(formMessages).removeClass('success error');
                $(formMessages).addClass('info');
                $(formMessages).text('Please wait, mail is sending...');
            }
        })
        .done(function(response) {
            // Success
            $(formMessages).removeClass('error info');
            $(formMessages).addClass('success');
            $(formMessages).text(response);

            // Clear the form
            $('#contact-form input,#contact-form textarea').val('');
        })
        .fail(function(data) {
            // Error
            $(formMessages).removeClass('success info');
            $(formMessages).addClass('error');

            if (data.responseText !== '') {
                $(formMessages).text(data.responseText);
            } else {
                $(formMessages).text('Oops! An error occured and your message could not be sent.');
            }
        });
    });

});
