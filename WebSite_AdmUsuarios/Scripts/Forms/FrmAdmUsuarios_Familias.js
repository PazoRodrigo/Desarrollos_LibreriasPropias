var _ObjFamilia;

$(document).ready(async function () {
    await Inicio();
});
async function Inicio() {
    _ObjFamilia = new Familia();
    LimpiarEntidad();
    _ListaFamilias = await Familia.TraerTodas();
    await LlenarGrillaBuscador(_ListaFamilias);
}
function LimpiarGrillaBuscador() {
    $('#DivGrillaLateralContenido').text('');
}
async function LlenarGrillaBuscador(lista) {
    LimpiarGrillaBuscador();
    await Familia.ArmarGrilla(lista, 'seleccionFamilia', 'DivGrillaLateralContenido', 'overflow-y:scroll; height:310px;');
}
function LimpiarEntidad() {
    $(".DatoFormulario").text('');
    $(".DatoFormulario").val('');
    $('#TxtNombre').focus();
}
async function LLenarEntidad() {
    $("#IdEntidad").text(_ObjFamilia.IdEntidad);
    $("#TxtNombre").val(_ObjFamilia.Nombre);
    await LlenarGrillaContenido();
    $('#TxtNombre').focus();
}

function LimpiarContenido() {
    $('#DivGrillaContenido1').text('');
    $('#DivGrillaContenido2').text('');
}
async function LlenarContenidoRamas() {
    LimpiarContenido();
    await Rama.ArmarRadios(await _ObjFamilia.ListaRamas(), 'seleccionRama', 'DivGrillaContenido1', 'width:100%; overflow-y:scroll; height:250px;');
}
function LimpiarFormulariosAcciones() {
    $('#DivGrillaContenido2').text('');
}
async function ArmarFormulariosAcciones(lista) {
    LimpiarFormulariosAcciones();
    await Formulario.ArmarGrillaSinAccionesSinEvento(lista, 'DivGrillaContenido2', 'width:100%; overflow-y:scroll; height:250px;');
}
function LimpiarGrillaContenido() {
    $('#DivGrillaContenido').text('');
}
async function LlenarGrillaContenido() {
    LimpiarGrillaContenido();
    await _ObjFamilia.ArmarGrillaContenido('DivGrillaContenido', 'overflow-y: scroll; max-height: 250px;');
    //await _ObjFamilia.ArmarGrillaContenido('DivGrillaConteno', 'padding-top:10px; overflow-y:scroll; max-height:250px;');
}
async function ArmarBotoneraSuperior() {
    let str = '';
    str += '<div class="row separadorRow30 DatoFormulario">';
    str += '    <div class="col-lg-11"><input id="BtnEliminar" type="button" value="Eliminar" class="btn btn-block btn-xs btn-danger" /></div>';
    str += '</div>';
    str += '<div class="row separadorRow30 DatoFormulario">';
    str += '    <div class="col-lg-11"><input id="BtnMenu" type="button" value="Ver Menu" class="btn btn-block btn-md btn-info" /></div>';
    str += '</div>';
    $("#DivBtnBotoneraSuperior").html(str);
}
// Buscador
$("body").on("keyup", "#TxtBuscadorNombre", async function () {
    let Resultado = [];
    Resultado = await BuscarTextoXCantCaracteres(2, $("#TxtBuscadorNombre").val(), await Familia.TraerTodas(), _ListaFamilias);
    _ListaFamilias = Resultado;
    await LlenarGrillaBuscador(Resultado);
});
// Botones
$("body").on("click", "#BtnBuscar", async function () {
    _ObjFamilia = new Familia();
    alertInfo('En Construcción');
});
$("body").on("click", "#BtnNuevo", async function () {
    await Inicio();
});
$("body").on("click", "#BtnGuardar", async function () {
    try {
        spinner();
        _ObjFamilia.Nombre = $("#TxtNombre").val();
        if (_ObjFamilia.IdEntidad == 0) {
            // Alta
            await _ObjFamilia.Alta();
        } else {
            // Modifica
            await _ObjFamilia.Modifica();
        }
        await Inicio();
        spinnerClose();
        alertOk('La Familia se ha guardado correctamente.');
    } catch (e) {
        spinnerClose();
        alertAlerta('La Familia no se ha guardado. \n\n ' + e);
    }
});
$("body").on("click", "#BtnEliminar", async function () {
    PopUpConfirmar('warning', _ObjFamilia, 'Desea eliminar el Familia?', _ObjFamilia.Nombre, 'eventoEliminarFamilia', 'Si, Eliminar!', 'Cancelar', '#DD6B55');
});
$("body").on("click", "#BtnMenu", async function () {
    try {
        $('#Modal-PopUpMenu').remove();
        await Menu.ArmarPopUp(2, _ObjFamilia.IdEntidad);
    } catch (e) {
        alertAlerta('Error en Construcción de Menú \n\n ' + e);
    }
});
// Escuchadores
document.addEventListener('seleccionFamilia', async function (e) {
    try {
        let objSeleccionado = e.detail;
        _ObjFamilia = objSeleccionado;
        await LLenarEntidad();
        if (_ObjFamilia.IdEstado == 0) {
            await ArmarBotoneraSuperior();
        } else {
            $("#DivBtnBotoneraSuperior").html('');
        }
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('seleccionRama', async function (e) {
    try {
        let objSeleccionado = e.detail;
        LimpiarFormulariosAcciones();
        await ArmarFormulariosAcciones(await objSeleccionado.ListaFormularios());
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('eventoEliminarFamilia', async function (e) {
    try {
        spinner();
        await _ObjFamilia.Baja();
        await Inicio();
        spinnerClose();
        alertOk('La Familia se ha eliminado correctamente.');
    } catch (e) {
        spinnerClose();
        alertAlerta('La Familia no se ha eliminado. \n\n ' + e);
    }
}, false);