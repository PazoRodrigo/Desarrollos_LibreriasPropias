function validarVacio(busqueda) {
    var result = true;
    if (busqueda.length == 0) {
        result = false;
    }
    return result;
}
function jsSoloNumeros(e) {
    var keynum = window.event ? window.event.keyCode : e.which;
    if ((keynum == 8) || (keynum == 46))
        return true;
    return /\d/.test(String.fromCharCode(keynum));
}
function jsSoloNumerosSinPuntos(e) {
    var keynum = window.event ? window.event.keyCode : e.which;
    if (keynum == 8)
        return true;
    return /\d/.test(String.fromCharCode(keynum));
}
function validarCantidadCaracteres(busqueda, cantidad_caracteres) {
    var result = true;
    if (busqueda.length < cantidad_caracteres) {
        result = false;
    }
    return result;
}
function alertOk(mensaje) {
    swal("", mensaje, "success");
}
function alertInfo(mensaje) {
    swal("", mensaje, "info");
}
function alertAlerta(mensaje) {
    swal("", mensaje, "warning");
}
function alertConfirmarEliminar(objeto, tipo) {
    let str = '';
    var event;
    switch (tipo) {
        case 1:
            str = 'el Tipo Domicilio';
            event = new CustomEvent('TipoDomicilioEliminado', { detail: objeto });
            break;
        case 2:
            str = 'la zona';
            event = new CustomEvent('ZonaEliminado', { detail: objeto });
            break;
        case 3:
            str = 'el rubro';
            event = new CustomEvent('RubroEliminado', { detail: objeto });
            break;
        case 4:
            str = 'el Tipo Cliente';
            event = new CustomEvent('TipoClienteEliminado', { detail: objeto });
            break;
        case 5:
            str = 'el Tipo Proveedor';
            event = new CustomEvent('TipoProveedorEliminado', { detail: objeto });
            break;
        case 6:
            str = 'el Producto';
            event = new CustomEvent('ProductoEliminado', { detail: objeto });
            break;
        case 7:
            str = 'la Presentación';
            event = new CustomEvent('PresentacionEliminado', { detail: objeto });
            break;
        case 8:
            str = 'la Marca';
            event = new CustomEvent('MarcaEliminado', { detail: objeto });
            break;
        default:
    }
    swal({
        title: "Confirma que desea eliminar " + str +" ?",
        text: objeto.Denominacion,
        type: "warning",
        showCancelButton: true,
        cancelButtonText: "Cancelar",
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si, eliminar!",
        closeOnConfirm: true
    },
        function (isConfirm) {
            if (isConfirm) {
                document.dispatchEvent(event);
            }
        });
}
function TraerPeriodosActualMenos(cantMeses, desde) {
    // El desde debe ser aaaamm
    var result = new Array();
    var fecha = new Date();
    var ano = fecha.getFullYear();
    var mes = fecha.getMonth() + 1;
    var anoMes = '';
    if (desde == 0) {
        for (ind = 0; ind < cantMeses; ind++) {
            anoMes = ano + '' + Right("00" + mes, 2);
            result.push(anoMes);
            mes -= 1;
            if (mes == 0) {
                ano -= 1;
                mes = 12;
            }
        }
    }
    return result;
}
function validarEmail(email) {
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    var result = true;
    if (!filter.test(email)) {
        result = false;
    }
    return result;
}
function validarTelefono(nroIngresado1, nroIngresado2, nroIngresado3) {
    var result = true;
    if (nroIngresado1.length == 0 && nroIngresado1.length == 0 && nroIngresado3.length == 0) {
        result = false;
        alertAlerta('Ingrese un nro. de Teléfono Completo (DDN/Área/Número)');
    } else {
        if (nroIngresado1.length == 0) {
            result = false;
            alertAlerta('Ingrese el DDN del nro. de Teléfono');
        } else {
            if (nroIngresado2.length == 0) {
                result = false;
                alertAlerta('Ingrese el Área del nro. de Teléfono');
            } else {
                if (nroIngresado3.length == 0) {
                    result = false;
                    alertAlerta('Ingrese el número del nro. de Teléfono');
                }
            }
        }
    }
    return result;
}
function lum_TraerProvincias() {
    var wsTransfer;
    var data = {
    };
    $.ajax({
        url: "../WebServices/wsLUM.asmx/TraerProvincias",
        dataType: "json",
        type: "POST",
        data: JSON.stringify(data),
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            wsTransfer = data.d;
            if (wsTransfer.todoOk == true) {
            }
            else {
                alertAlerta(wsTransfer.mensaje);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {

            alertAlerta(thrownError);
        }
    });
    return wsTransfer.data;
}
function LongToDateString(lng) {
    let fecha = '';
    let str = lng.toString();
    if (str.length === 8) {
        let dia = str.substring(6);
        let mes = str.substring(4, 6);
        let anio = str.substring(0, 4);
        fecha = dia + "/" + mes + "/" + anio;
    }
    return fecha;
}
function LongToHourString(lng) {
    let hora = '';
    let str = lng.toString();
    if (str.length === 4) {
        let horas = str.substring(0, 2);
        let minutos = str.substring(2);
        hora = horas + ":" + minutos;
    }
    return hora;
}
function dateStringToLong(str) {
    let fecha = 0;
    if (str.length === 10) {
        let anio = parseInt(str.substring(6, 10));
        let mes = parseInt(str.substring(3, 5));
        let dia = parseInt(str.substring(0, 2));
        fecha = anio * 10000 + mes * 100 + dia;
    }
    return fecha;
}
function dateToLong(fecha) {
    return fecha.substr(6, 4) + '' + fecha.substr(3, 2) + '' + fecha.substr(0, 2);
}
if (!String.format) {
    String.format = function (format) {
        var args = Array.prototype.slice.call(arguments, 1);
        return format.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] !== 'undefined'
                ? args[number]
                : match
                ;
        });
    };
}
function Right(str, n) {
    if (n <= 0)
        return "";
    else if (n > String(str).length)
        return str;
    else {
        var iLen = String(str).length;
        return String(str).substring(iLen, iLen - n);
    }
}

function limpiarTextos() {
    $('input[type=text]').val('');
};
function limpiarCheckBoxs() {
    $('input[type=checkbox]').attr('checked', false);
};


// Para hacer
function LUM_ArmarPOP(div) {
}
function spinner() {
    var clases = ["spinner-loader", "throbber-loader", "refreshing-loader", "heartbeat-loader", "gauge-loader",
        "three-quarters-loader", "wobblebar-loader", "whirly-loader", "flower-loader", "dots-loader",
        "circles-loader", "plus-loader", "ball-loader", "hexdots-loader", "inner-circles-loader", "pong-loader",
        "pulse-loader", "spinning-pixels-loader"];
    var clase = clases[Math.floor(Math.random() * 17) + 1];
    $.blockUI({
        css: {
            border: 'none',
            padding: '15px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .5,
            color: '#fff'
        },
        baseZ: 3000,
        message: '<div class="' + clase + '" style="z-index: 200000"> </div><br/><br/><br/><h3>Espere un momento</h3>'
    });
}
function spinnerClose() {
    $.unblockUI();
}
