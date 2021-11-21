
function soloNumeros(evt) {
    var theEvent = evt || window.event;

    // Handle paste
    if (theEvent.type === 'paste') {
        key = event.clipboardData.getData('text/plain');
    } else {
        // Handle key press
        var key = theEvent.keyCode || theEvent.which;
        key = String.fromCharCode(key);
    }
    var regex = /[0-9]|\./;
    if (!regex.test(key)) {
        theEvent.returnValue = false;
        if (theEvent.preventDefault) theEvent.preventDefault();
    }
}


function soloLetras(evt) {
    var theEvent = evt || window.event;

    // Handle paste
    if (theEvent.type === 'paste') {
        key = event.clipboardData.getData('text/plain');
    } else {
        // Handle key press
        var key = theEvent.keyCode || theEvent.which;
        key = String.fromCharCode(key);
    }
   // var regex = /[A-Z a-z]|\./;
    let regex = /^[A-Za-zÁÉÍÓÚáéíóúñÑ ]+$/g;

    if (!regex.test(key)) {
        theEvent.returnValue = false;
        if (theEvent.preventDefault) theEvent.preventDefault();
    }
}

///////////////////////////////////////////////////////

//$(function () {
//    $("form input").keypress(function (e) {
//        if ((e.which && e.which == 13) || (e.keyCode && e.keyCode == 13)) {
//            $('button[type=submit] .default').click();
//            return false;
//        } else {
//            return true;
//        }
//    });
//});

////////////////////   TELEFONOS   /////////////////////////////

$("#btnNuevo").click(function (eve) {

    $("#modal-content").load("/TelefonosEmpresas/Create");
});


$(".btnEditar").click(function (eve) {

    $("#modal-content").load("/TelefonosEmpresas/Edit/" + $(this).data("id"));
});


$(".btnDelete").click(function (eve) {

    $("#modal-content").load("/TelefonosEmpresas/Delete/" + $(this).data("id"));
});


////////////////////   REDES   /////////////////////////////

$("#btnNuevaRed").click(function (eve) {

    $("#modal-content").load("/RedesSocEmpresas/Create");
});


$(".btnEditarRed").click(function (eve) {

    $("#modal-content").load("/RedesSocEmpresas/Edit/" + $(this).data("id"));
});


$(".btnBorrarRed").click(function (eve) {

    $("#modal-content").load("/RedesSocEmpresas/Delete/" + $(this).data("id"));
});


////////////////////   MAILS   /////////////////////////////

$("#btnNuevoMail").click(function (eve) {

    $("#modal-content").load("/MailsEmpresas/Create");
});


$(".btnEditarMail").click(function (eve) {

    $("#modal-content").load("/MailsEmpresas/Edit/" + $(this).data("id"));
});


$(".btnBorrarMail").click(function (eve) {

    $("#modal-content").load("/MailsEmpresas/Delete/" + $(this).data("id"));
});

////////////////////   SORTEOS   /////////////////////////////

//$("#btnNuevoSorteo").click(function (eve) {

//    $("#modal-content").load("/SorteosEmpresas/Create");
//});


//$(".btnEditarSorteo").click(function (eve) {

//    $("#modal-content").load("/SorteosEmpresas/Edit/" + $(this).data("id"));
//});


//$(".btnBorrarSorteo").click(function (eve) {

//    $("#modal-content").load("/SorteosEmpresas/Delete/" + $(this).data("id"));
//});

/////////////////////////////////////////////////////////////////

$(".ultimoMensaje").animate({ scrollTop: $('.ultimoMensaje')[0].scrollHeight }, 1000);

//SI QUIERO QUE TENGA UNA ANIMACION DE 0 MILISEGUNDOS USAR:
//$(".ultimoMensaje").animate({ scrollTop: $('.ultimoMensaje')[0].scrollHeight },0);



//$('.counter').each(function () {
//    var $this = $(this),
//        countTo = $this.attr('data-count');

//    $({ countNum: $this.text() }).animate({
//        countNum: countTo
//    },
//        {
//            duration: 5000,
//            easing: 'linear',
//            step: function () {
//                $this.text(Math.floor(this.countNum));
//            },
//            complete: function () {
//                $this.text(this.countNum);
//                //alert('finished');
//            }

//        });

//});