$(document).ready(function () {


    var baseUrl = 'https://localhost:7228/api/'; // Local 
    var loginPath = 'Authentication/login';
    var registerPath = '';
    var resetPasswordPath = '';
    var forgetPath = '';
    var getBroker = 'Broker/get-all';


    



    $('.btn-login').on('click', function (event) {
        event.preventDefault();

        const email = $('.email-login').val();
        const password = $('.password-login').val();
        const rememberMe = $('.remeber-login').is(':checked');

        alert(rememberMe);

        $.ajax({
            url: baseUrl + loginPath,
            type: 'POST',
            contentType: 'application/json',
            xhrFields: { withCredentials: true },// IMPORTANT for cookies
            data: JSON.stringify({ email, password, rememberMe}),
            success: function (res) {
                console.log(res);
                if(res.responseCode == 200)
                {
                    localStorage.setItem('accessToken', res.data.accessToken);
                    window.location.href = '/home.html';
                }
                else {
                    alert(res.responseMessage);
                }
            },
            error: function (error) {
                alert(error);
            }
        });

    });






    //.  Home Page
    

});