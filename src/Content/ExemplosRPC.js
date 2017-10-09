var BASE_URL = $('base').attr('href');

$('#btn-hello').click(function () {
    var req = {
        id: (new Date()).getTime().toString(),
        method: 'Home.Hello',
        params: [
            'Olá, bem vindo ao RPC'
        ]
    };

    $.ajax({
        type: "POST",
        url: BASE_URL + 'rpc',
        data: JSON.stringify(req),
        contentType: 'application/json',
        success: function (result) {
            console.log(result);
        },
    });
    
});