var _ObjRama;

$(document).ready(async function () {
    await Inicio();
});
async function Inicio() {
    await LimpiarRama();
    _ListaRamas = await Rama.TraerTodos();
    await LLenarGrillaBuscadorRama(_ListaRamas);
}
function LimpiarGrillaBuscador() {
    $('#DivGrillaLateralContenido').text('');
}
async function LLenarGrillaBuscadorRama(lista) {
    LimpiarGrillaBuscador();
    await Rama.ArmarGrilla(lista, 'seleccionRama', 'DivGrillaLateralContenido', 'overflow-y:scroll; height:310px;', false);
}
function LimpiarComboFamilias() {
    $('#DivGrillaSuperiorDerecha').text('');
}
async function LlenarComboFamilias() {
    LimpiarComboFamilias();
    await Familia.ArmarCombo(await Familia.TraerTodos(), 'seleccionFamilia', 'DivGrillaSuperiorDerecha', 'height:100px;');
}
async function LimpiarRama() {
    _ObjRama = new Rama();
    $(".DatoFormulario").text('');
    $(".DatoFormulario").val('');
    $('#TxtNombre').focus();
    await LlenarComboFamilias();
}
async function LLenarRama() {
    $("#IdEntidad").text(_ObjRama.IdEntidad);
    $("#TxtNombre").val(_ObjRama.Nombre);
    await LlenarComboFamilias();
    $("#selectorFamiliaCombo").text((await _ObjRama.ObjFamilia()).Nombre);
    await LlenarContenido();
    $('#TxtNombre').focus();
}
function LimpiarContenido() {
    $('#DivGrillaContenido').text('');
}
async function LlenarContenido() {
    LimpiarContenido();
    await Formulario.ArmarGrillaConAccionesOrden(_ObjRama, 'DivGrillaContenido', 'ordenar', 'overflow-y:scroll; height:230px;');
}
async function ArmarBotoneraSuperior() {
    let str = '';
    str += '<div class="row separadorRow30 DatoFormulario">';
    str += '    <div class="col-lg-11"><input id="BtnEliminar" type="button" value="Eliminar" class="btn btn-block btn-xs btn-danger" /></div>';
    str += '</div>';
    str += '<div class="row separadorRow30 DatoFormulario">';
    str += '    <div class="col-lg-11"><input id="BtnMenu" type="button" value="Ver Menu" class="btn btn-block btn-xs btn-info" /></div>';
    str += '</div>';
    $("#DivBtnBotoneraSuperior").html(str);
}
// Buscador
$("body").on("keyup", "#TxtBuscadorNombre", async function () {
    let Resultado = [];
    Resultado = await BuscarTextoXCantCaracteres(2, $("#TxtBuscadorNombre").val(), await Rama.TraerTodas(), _ListaRamas);
    _ListaRamas = Resultado;
    await LLenarGrillaBuscadorRama(Resultado);
});
// Botones
$("body").on("click", "#BtnBuscar", async function () {
    alertInfo('En Construcción');
});
$("body").on("click", "#BtnNuevo", async function () {
    LimpiarRama();
});
$("body").on("click", "#BtnGuardar", async function () {
    try {
        //spinner();
        _ObjRama.Nombre = $("#TxtNombre").val();
        if (_ObjRama.IdEntidad == 0) {
            // Alta
            await _ObjRama.Alta();
        } else {
            // Modifica
            //await _ObjRama.Modifica();
            let objModifica = new Rama();
            objModifica.IdEntidad = _ObjRama.IdEntidad;
            objModifica.Nombre = _ObjRama.Nombre;
            objModifica.IdFamilia = _ObjRama.IdFamilia;
            objModifica.Orden = _ObjRama.Orden;
            objModifica.IdEstado = _ObjRama.IdEstado;
            await objModifica.Modifica();
        }
        await Inicio();
        //spinnerClose();
        alertOk('La Rama se ha guardado correctamente.');
    } catch (e) {
        //spinnerClose();
        alertAlerta('La Rama no se ha guardado. \n\n ' + e);
    }
});
$("body").on("click", "#BtnEliminar", async function () {
    PopUpConfirmar('warning', _ObjRama, 'Desea eliminar la Rama?', _ObjRama.Nombre, 'eventoEliminarRama', 'Si, Eliminar!', 'Cancelar', '#DD6B55');
});
$("body").on("click", "#BtnMenu", async function () {
    try {
        $('#Modal-PopUpMenu').remove();
        await Menu.ArmarPopUp(1, _ObjRama.IdEntidad);
    } catch (e) {
        alertAlerta('Error en Construcción de Menú \n\n ' + e);
    }
});
// Escuchadores
document.addEventListener('seleccionRama', async function (e) {
    try {
        let objSeleccionado = e.detail;
        _ObjRama = objSeleccionado;
        await LLenarRama();
        if (_ObjRama.IdEstado == 0) {
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
        _ObjRama.IdFamilia = objSeleccionado.IdEntidad;
        $("#selectorFamiliaCombo").text(objSeleccionado.Nombre);
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('ordenar', async function (e) {
    try {
        let obj = e.detail;
        await obj.Ordena();
        await LlenarContenido();
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('eventoEliminarRama', async function (e) {
    try {
        spinner();
        await _ObjRama.Baja();
        await Inicio();
        spinnerClose();
        alertOk('La Rama se ha eliminado correctamente.');
    } catch (e) {
        spinnerClose();
        alertAlerta('La Rama no se ha eliminado. \n\n ' + e);
    }
}, false);