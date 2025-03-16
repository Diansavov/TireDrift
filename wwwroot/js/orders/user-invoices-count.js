
$( document ).ready(function() {
    $.ajax({
        url: '/Tires/GetUserHotelTiresCount',
        method: 'GET', // Explicitly define the HTTP method
        dataSrc: '',
        success: function(data) {
            document.getElementById('user-hotel-tires').innerHTML = data; // Handle the successful response
        },
        error: function(){
            document.getElementById('user-hotel-tires').innerHTML = 0; // Handle the successful response
        }
    });
})
