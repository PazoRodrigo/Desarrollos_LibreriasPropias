var _ObjArea;

$(document).ready(async function () {
    await Inicio();
});
async function Inicio() {
    LimpiarArea();
    _ListaAreas = await Area.TraerTodos();
    await LLenarGrillaBuscadorArea(_ListaAreas);
}
function LimpiarGrillaBuscador() {
    $('#DivGrillaLateralContenido').text('');
}
async function LLenarGrillaBuscadorArea(lista) {
    LimpiarGrillaBuscador();
    await Area.ArmarGrilla(lista, 'seleccionArea', 'DivGrillaLateralContenido', 'overflow-y:scroll; height:310px;');
}
function LimpiarArea() {
    _ObjArea = new Area();
    $(".DatoFormulario").text('');
    $(".DatoFormulario").val('');
    $('#TxtNombre').focus();
}
async function LLenarArea() {
    $("#IdEntidad").text(_ObjArea.IdEntidad);
    $("#TxtNombre").val(_ObjArea.Nombre);
    await LlenarContenido();
    await ArmarBotoneraSuperior();
    $('#TxtNombre').focus();
}
function LimpiarContenido() {
    $('#DivGrillaContenido').text('');
}
async function LlenarContenido() {
    LimpiarContenido();
    let lista = await _ObjArea.ListaUsuarios();
    if (lista.length > 0) {
        for (let obj of lista) {
            if (obj.idEstado == 0) {
                listaActivos.push(obj);
            }
        }
    }
    await Usuario.ArmarLista(lista, 'GrillaContenido', 'width:100%; overflow-y:scroll; height:250px;');
}
async function ArmarBotoneraSuperior() {
    let str = '';
    str += '<div class="row separadorRow30 DatoFormulario">';
    str += '    <div class="col-lg-11"><input id="BtnEliminar" type="button" value="Eliminar" class="btn btn-block btn-xs btn-danger" /></div>';
    str += '</div>';
    $("#DivBtnBotoneraSuperior").html(str);
}
// Buscador
$("body").on("keyup", "#TxtBuscadorNombre", async function () {
    let Resultado = [];
    Resultado = await BuscarTextoXCantCaracteres(2, $("#TxtBuscadorNombre").val(), await Area.TraerTodas(), _ListaAreas);
    _ListaAreas = Resultado;
    await LLenarGrillaBuscadorArea(Resultado);
});
// Botones
$("body").on("click", "#BtnBuscar", async function () {
    _ObjArea = new Area();
    alertInfo('En Construcción');
});
$("body").on("click", "#BtnNuevo", async function () {
    LimpiarArea();
});
$("body").on("click", "#BtnGuardar", async function () {
    try {
        spinner();
        _ObjArea.Nombre = $("#TxtNombre").val();
        if (_ObjArea.IdEntidad == 0) {
            // Alta
            await _ObjArea.Alta();
        } else {
            // Modifica
            await _ObjArea.Modifica();
        }
        LimpiarArea();
        await Inicio();
        spinnerClose();
        alertOk('El Área se ha guardado correctamente.');
    } catch (e) {
        spinnerClose();
        alertAlerta('El Área no se ha guardado. \n\n ' + e);
    }
});
$("body").on("click", "#BtnEliminar", async function () {
    PopUpConfirmar('warning', _ObjArea, 'Desea eliminar el Área?', _ObjArea.Nombre, 'eventoEliminarArea', 'Si, Eliminar!', 'Cancelar', '#DD6B55');
});
// Escuchadores
document.addEventListener('seleccionArea', async function (e) {
    try {
        let objSeleccionado = e.detail;
        _ObjArea = objSeleccionado;
        await LLenarArea();
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('eventoEliminarArea', async function (e) {
    try {
        spinner();
        await _ObjArea.Baja();
        await Inicio();
        spinnerClose();
        alertOk('El Área se ha eliminado correctamente.');
    } catch (e) {
        spinnerClose();
        alertAlerta('El Área no se ha eliminado. \n\n ' + e);
    }
}, false);
