// Funciones
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
function Left(str, n) {
    if (n <= 0)
        return "";
    else if (n > String(str).length)
        return str;
    else {
        return String(str).substring(0, n);
    }
}
function limpiarTextos() {
    $('input[type=text]').val('');
};
function limpiarCheckBoxs() {
    $('input[type=checkbox]').attr('checked', false);
};
//function ObtenerNavegador() {
//    var ua = navigator.userAgent, tem,
//        M = ua.match(/(opera|chrome|safari|firefox|msie|trident(?=\/))\/?\s*(\d+)/i) || [];
//    if (/trident/i.test(M[1])) {
//        tem = /\brv[ :]+(\d+)/g.exec(ua) || [];
//        return 'IE ' + (tem[1] || '');
//    }
//    if (M[1] === 'Chrome') {
//        tem = ua.match(/\b(OPR|Edge)\/(\d+)/);
//        if (tem != null) return tem.slice(1).join(' ').replace('OPR', 'Opera');
//    }
//    M = M[2] ? [M[1], M[2]] : [navigator.appName, navigator.appVersion, '-?'];
//    if ((tem = ua.match(/version\/(\d+)/i)) != null) M.splice(1, 1, tem[1]);
//    return M.join(' ');
//}
//async function ValidarNavegador() {
//    let entra = false;
//    let navegador = ObtenerNavegador();
//    if (Left(navegador, 7) == 'Firefox') {
//        entra = true;
//    } else {
//        throw ('El navegador debe ser Firefox.');
//    }
//    return entra;
//}


// Validaciones
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
function validarVacio(busqueda) {
    var result = true;
    if (busqueda.length == 0) {
        result = false;
    }
    return result;
}
function validarCantidadCaracteres(busqueda, cantidad_caracteres) {
    var result = true;
    if (busqueda.length < cantidad_caracteres) {
        result = false;
    }
    return result;
}
async function validarEmail(email) {
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    var result = true;
    if (!filter.test(email)) {
        result = false;
    }
    return result;
}
async function BuscarTextoXCantCaracteres(Caracteres, TextoBuscado, ListaTodo, Lista) {
    let ListaResultado = [];
    if (TextoBuscado.length >= Caracteres) {
        let Resultado = $.grep(Lista, function (element, index) {
            return element.Nombre.toLowerCase().indexOf(TextoBuscado.toLowerCase()) != -1;
        });
        ListaResultado = Resultado;
    } else {
        ListaResultado = ListaTodo;
    }
    return ListaResultado;
};

// Alertas
function alertOk(mensaje) {
    swal("", mensaje, "success");
}
function alertInfo(mensaje) {
    swal("", mensaje, "info");
}
function alertAlerta(mensaje) {
    swal("", mensaje, "warning");
}
function alertError(mensaje) {
    swal("", mensaje, "error");
}
function PopUpConfirmar(tipoAlerta, objeto, titulo, texto, evento, strBtnSi, strBtnNo, colorBtnSi, colorBtnNo) {
    let event = new CustomEvent(evento, { detail: objeto });
    if (strBtnNo == undefined) {
        strBtnNo = 'Cancelar';
    }
    if (colorBtnSi == undefined) {
        colorBtnSi = "#DD6B55";
    }
    if (colorBtnNo == undefined) {
        colorBtnNo = '';
    }
    swal({
        title: titulo,
        text: texto,
        type: tipoAlerta,
        showCancelButton: true,
        cancelButtonColor: colorBtnNo,
        cancelButtonText: strBtnNo,
        confirmButtonColor: colorBtnSi,
        confirmButtonText: strBtnSi,
        closeOnConfirm: true
    },
        function (isConfirm) {
            if (isConfirm) {
                document.dispatchEvent(event);
            }
        });
}
//function alertConfirmarEliminar(objeto, tipo) {
//    let str = '';
//    var event;
//    switch (tipo) {
//        case 1:
//            str = 'el Tipo Domicilio';
//            event = new CustomEvent('TipoDomicilioEliminado', { detail: objeto });
//            break;
//        case 2:
//            str = 'la zona';
//            event = new CustomEvent('ZonaEliminado', { detail: objeto });
//            break;
//        case 3:
//            str = 'el rubro';
//            event = new CustomEvent('RubroEliminado', { detail: objeto });
//            break;
//        case 4:
//            str = 'el Tipo Cliente';
//            event = new CustomEvent('TipoClienteEliminado', { detail: objeto });
//            break;
//        case 5:
//            str = 'el Tipo Proveedor';
//            event = new CustomEvent('TipoProveedorEliminado', { detail: objeto });
//            break;
//        case 6:
//            str = 'el Producto';
//            event = new CustomEvent('ProductoEliminado', { detail: objeto });
//            break;
//        case 7:
//            str = 'la Presentación';
//            event = new CustomEvent('PresentacionEliminado', { detail: objeto });
//            break;
//        case 8:
//            str = 'la Marca';
//            event = new CustomEvent('MarcaEliminado', { detail: objeto });
//            break;
//        default:
//    }
//    swal({
//        title: "Confirma que desea eliminar " + str + " ?",
//        text: objeto.Denominacion,
//        type: "warning",
//        showCancelButton: true,
//        cancelButtonText: "Cancelar",
//        confirmButtonColor: "#DD6B55",
//        confirmButtonText: "Si, eliminar!",
//        closeOnConfirm: true
//    },
//        function (isConfirm) {
//            if (isConfirm) {
//                document.dispatchEvent(event);
//            }
//        });
//}

// Fechas
function fechaHoy() {
    let fecha = new Date();
    fecha.setDate(fecha.getDate());
    let hoy = Right("00" + fecha.getDate(), 2) + '/' + Right("00" + (fecha.getMonth() + 1), 2) + '/' + fecha.getFullYear();
    return hoy;
}
function FechaHoyLng() {
    let FechaHoy = new Date()
    let result = FechaHoy.getFullYear() + '' + Right('00' + FechaHoy.getMonth(), 2) + '' + Right('00' + FechaHoy.getDay(), 2)
    return result;
}
function Date_LongToDate(lng) {
    let fecha = '';
    if (lng != '') {
        if (lng > 0) {
            let str = lng.toString();
            if (str.length === 8) {
                let ano = (str.substring(0, 4));
                let mes = (str.substring(4, 6));
                let dia = (str.substring(6));
                fecha = ano + '/' + Right('00' + mes, 2) + '/' + Right('00' + dia, 2);
            }
        }
    }
    return new Date(fecha);
}
function LongToDateString(lng) {
    let fecha = '';
    if (lng != '') {
        let str = lng.toString();
        if (str.length === 8) {
            let dia = str.substring(6);
            let mes = str.substring(4, 6);
            let anio = str.substring(0, 4);
            fecha = dia + "/" + mes + "/" + anio;
        }
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

// Ordenar
function SortXNombre(a, b) {
    var aNombre = a.Nombre.toLowerCase();
    var bNombre = b.Nombre.toLowerCase();
    return ((aNombre < bNombre) ? -1 : ((aNombre > bNombre) ? 1 : 0));
}
function OrdenarLista(a, b) {
    if (a.IdOrdenEnLista < b.IdOrdenEnLista) {
        return -1;
    }
    if (a.IdOrdenEnLista > b.IdOrdenEnLista) {
        return 1;
    }
    return 0;
}

// Para hacer
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