var _ObjFormulario;

$(document).ready(async function () {
    await Inicio();
});
async function Inicio() {
    await LimpiarEntidad();
    _ListaFormularios = await Formulario.TraerTodos();
    await LLenarGrillaBuscador(_ListaFormularios);
}
async function LimpiarEntidad() {
    _ObjFormulario = new Formulario();
    $(".DatoFormulario").text('');
    $(".DatoFormulario").val('');
    $('#TxtNombre').focus();
    LimpiarAcciones();
    await ArmarRadiosFamilia();
    LimpiarRadiosRama();
}
async function LLenarGrillaBuscador(lista) {
    await Formulario.ArmarGrilla(lista, 'seleccionFormulario', 'DivGrillaLateralContenido', 'overflow-y:scroll; height:310px;');
}
async function LLenarFormulario() {
    $("#IdEntidad").text(_ObjFormulario.IdEntidad);
    $("#TxtNombre").val(_ObjFormulario.Nombre);
    $("#TxtURL").val(_ObjFormulario.URL);
    let ObjRama = await _ObjFormulario.ObjRama();
    let ObjFamilia = await ObjRama.ObjFamilia();
    await ArmarRadiosFamilia();
    await ArmarRadiosRama(await ObjFamilia.ListaRamas());
    await MarcarAcciones(_ObjFormulario.AccionesPosibles);
    $("input[name='rblFamilia'][value='" + ObjFamilia.IdEntidad + "']").prop('checked', true);
    $("input[name='rblRama'][value='" + ObjRama.IdEntidad + "']").prop('checked', true);
    _ObjFormulario.IdRamaAnterior = _ObjFormulario.IdRama;
    $('#TxtNombre').focus();
}
function LimpiarRadiosFamilia() {
    $('#DivGrillaContenido1').text('');
}
async function ArmarRadiosFamilia() {
    LimpiarRadiosFamilia();
    await Familia.ArmarRadios('seleccionFamilia', 'DivGrillaContenido1', 'width:100%; overflow-y:scroll; height:250px;');
}
function LimpiarRadiosRama() {
    $('#DivGrillaContenido2').text('');
}
async function ArmarRadiosRama(lista) {
    await Rama.ArmarRadios(lista, 'seleccionRama', 'DivGrillaContenido2', 'width:100%; overflow-y:scroll; height:250px;');
}
function LimpiarAcciones() {
    $('input[name="Accion"]').each(function () {
        this.checked = false;
    });
}
async function ObtenerAcciones() {
    let result = 0;
    let lista = $('input[name="Accion"]');
    for (let item of lista) {
        if (item.id == 'cbx_Acciones_Visualizar') {
            if (item.checked == true) {
                result += 8;
            }
        }
        if (item.id == 'cbx_Acciones_Agregar') {
            if (item.checked == true) {
                result += 1;
            }
        }
        if (item.id == 'cbx_Acciones_Modificar') {
            if (item.checked == true) {
                result += 2;
            }
        }
        if (item.id == 'cbx_Acciones_Eliminar') {
            if (item.checked == true) {
                result += 4;
            }
        }
    }
    return result;
}
async function MarcarAcciones(IntAcciones) {
    LimpiarAcciones();
    if (IntAcciones >= 8) {
        $('#cbx_Acciones_Visualizar').prop('checked', true);
        IntAcciones -= 8;
    }
    if (IntAcciones >= 4) {
        $('#cbx_Acciones_Eliminar').prop('checked', true);
        IntAcciones -= 4;
    }
    if (IntAcciones >= 2) {
        $('#cbx_Acciones_Modificar').prop('checked', true);
        IntAcciones -= 2;
    }
    if (IntAcciones == 1) {
        $('#cbx_Acciones_Agregar').prop('checked', true);
        IntAcciones -= 1;
    }
}
async function ArmarBotoneraSuperior() {
    let str = '';
    str += '<div class="row separadorRow30 DatoFormulario">';
    str += '    <div class="col-lg-11"><input id="BtnEliminar" type="button" value="Eliminar" class="btn btn-block btn-xs btn-danger" /></div>';
    str += '</div>';
    $("#DivBtnBotoneraSuperior").html(str);
}
async function ArmarStrPerfiles(ListaAccesos) {
    let str = '';
    let listaPerfiles = await _ObjFormulario.ListaPerfiles();
    if (listaPerfiles.length > 0) {
        let anterior = 0;
        for (let objPerf of listaPerfiles) {
            if (anterior > 0) {
                str += ", ";
            }
            str += objPerf.Nombre;
            anterior = 1;
        }
    }
    return str;
}
async function ArmarStrUsuarios() {
    let str = '';
    let Lista = await _ObjFormulario.ListaUsuarios();
    console.log(Lista);
    //let listaP = [];
    //let objPerfil;
    //let buscado;
    //for (let objAc of ListaAccesos) {
    //    objPerfil = await Perfil.TraerUno(objAc.IdPerfil);
    //    if (listaP.length == 0) {
    //        listaP.push(objPerfil);
    //    } else {
    //        buscado = $.grep(listaP, function (entidad, index) {
    //            return entidad.IdEntidad == objPerfil.IdEntidad;
    //        });
    //        if (buscado.length == 0) {
    //            listaP.push(objPerfil);
    //        }
    //    }
    //}
    //if (listaP.length > 0) {
    //    let anterior = 0;
    //    for (let objPerf of listaP) {
    //        console.log(objPerf);
    //        console.log(anterior);
    //        if (anterior > 0) {
    //            str += ", ";
    //        }
    //        str += objPerf.Nombre;
    //        anterior = 1;
    //    }
    //}
    return str;
}
// Buscador
$("body").on("keyup", "#TxtBuscadorNombre", async function () {
    let Resultado = [];
    Resultado = await BuscarTextoXCantCaracteres(2, $("#TxtBuscadorNombre").val(), await Formulario.TraerTodas(), _ListaFormularios);
    __ListaFormularios = Resultado;
    await LLenarGrillaBuscador(Resultado);
});
// Botones
$("body").on("click", "#BtnBuscar", async function () {
    _ObjFormulario = new Formulario();
    alertInfo('En Construcción');
});
$("body").on("click", "#BtnNuevo", async function () {
    _ObjFormulario = new Formulario();
    LimpiarEntidad();
    await ArmarRadiosFamilia(0);
});
$("body").on("click", "#BtnGuardar", async function () {
    try {
        spinner();
        _ObjFormulario.Nombre = $("#TxtNombre").val();
        _ObjFormulario.URL = $("#TxtURL").val();
        _ObjFormulario.AccionesPosibles = await ObtenerAcciones();
        if (_ObjFormulario.IdEntidad == 0) {
            // Alta
            _ObjFormulario.Orden = await Formulario.NuevoOrden(_ObjFormulario.IdRama);
            await _ObjFormulario.Alta();
        } else {
            // Modifica
            let objModifica = new Formulario();
            objModifica.IdEntidad = _ObjFormulario.IdEntidad;
            objModifica.IdRama = _ObjFormulario.IdRama;
            objModifica.Orden = _ObjFormulario.Orden;
            objModifica.Nombre = _ObjFormulario.Nombre;
            objModifica.URL = _ObjFormulario.URL;
            objModifica.AccionesPosibles = _ObjFormulario.AccionesPosibles;
            objModifica.IdEstado = _ObjFormulario.IdEstado;
            await objModifica.Modifica(_ObjFormulario.IdRamaAnterior);
        }
        await Inicio();
        spinnerClose();
        alertOk('El Formulario se ha guardado correctamente.');
    } catch (e) {
        spinnerClose();
        alertAlerta('El Formulario no se ha guardado. \n\n ' + e);
    }
});
$("body").on("click", "#BtnEliminar", async function () {
    try {
        spinner();
        let texto = _ObjFormulario.Nombre;
        texto += '\n\n Rama: ' + (await _ObjFormulario.ObjRama()).Nombre;
        let tempObjRama = await _ObjFormulario.ObjRama();
        texto += '\n Familia: ' + (await tempObjRama.ObjFamilia()).Nombre;
        let ListaAccesos = await _ObjFormulario.ListaAccesos();
        texto += '\n Perfil: ' + await ArmarStrPerfiles(ListaAccesos);
        texto += '\n Usuarios: ' + await ArmarStrUsuarios();
        spinnerClose();
        PopUpConfirmar('warning', _ObjFormulario, 'Desea eliminar el Formulario?', texto, 'eventoEliminarFormulario', 'Si, Eliminar!', 'Cancelar', '#DD6B55');
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }

});
// Escuchadores
document.addEventListener('seleccionFormulario', async function (e) {
    try {
        let objSeleccionado = e.detail;
        _ObjFormulario = objSeleccionado;
        await LLenarFormulario();
        if (_ObjFormulario.IdEstado == 0) {
            await ArmarBotoneraSuperior();
        } else {
            $("#DivBtnBotoneraSuperior").html('');
        }
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('seleccionFamilia', async function (e) {
    try {
        let objSeleccionado = e.detail;
        await ArmarRadiosRama(await objSeleccionado.ListaRamas());
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('seleccionRama', async function (e) {
    try {
        let objSeleccionado = e.detail;
        _ObjFormulario.IdRama = objSeleccionado.IdEntidad;
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('eventoEliminarFormulario', async function (e) {
    try {
        spinner();
        await _ObjFormulario.Baja();
        await Inicio();
        spinnerClose();
        alertOk('El Formulario se ha eliminado correctamente. \n\n ');
    } catch (e) {
        spinnerClose();
        alertAlerta('El Formulario no se ha eliminado. \n\n ' + e);
    }
}, false);