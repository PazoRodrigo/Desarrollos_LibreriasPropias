var _ObjRol;

$(document).ready(async function () {
    await Inicio();
});
async function Inicio() {
    _ObjRol = new Rol();
    LimpiarRol();
    _ListaRoles = await Rol.TraerTodos();
    console.log(_ListaRoles);
    await LLenarGrillaBuscadorRol(_ListaRoles);
}
function LimpiarGrillaBuscador() {
    $('#DivGrillaLateralContenido').text('');
}
async function LLenarGrillaBuscadorRol(lista) {
    LimpiarGrillaBuscador();
    await Rol.ArmarGrilla(lista, 'seleccionRol', 'DivGrillaLateralContenido', 'overflow-y:scroll; height:310px;');
}
function LimpiarRol() {
    _ObjRol = new Rol();
    $(".DatoFormulario").text('');
    $(".DatoFormulario").val('');
    $('#TxtNombre').focus();
}
async function LLenarRol() {
    $("#IdEntidad").text(_ObjRol.IdEntidad);
    $("#TxtNombre").val(_ObjRol.Nombre);
    await LlenarContenido();
    await ArmarBotoneraSuperior();
    $('#TxtNombre').focus();
}
function LimpiarContenido() {
    $('#DivContenidoMedio').text('');
}
async function LlenarContenido() {
    LimpiarContenido();
    let lista = await Perfil.TraerTodosXRol(_ObjRol.IdEntidad);
    let listaActivos = [];
    if (lista.length > 0) {
        for (let obj of lista) {
            if (obj.idEstado == 0) {
                listaActivos.push(obj);
            }
        }
    }
    await Perfil.ArmarLista(lista, 'DivContenidoMedio', 'width:100%; overflow-y:scroll; height:250px;');
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
    Resultado = await BuscarTextoXCantCaracteres(2, $("#TxtBuscadorNombre").val(), await Rol.TraerTodas(), _ListaRoles);
    _ListaRoles = Resultado;
    await LLenarGrillaBuscadorRol(Resultado);
});
// Botones
$("body").on("click", "#BtnBuscar", async function () {
    _ObjRol = new Rol();
    alertInfo('En Construcción');
});
$("body").on("click", "#BtnNuevo", async function () {
    _ObjRol = new Rol();
    LimpiarRol();
});
$("body").on("click", "#BtnGuardar", async function () {
    try {
        spinner();
        _ObjRol.Nombre = $("#TxtNombre").val();
        if (_ObjRol.IdEntidad === 0) {
            // Alta
            await _ObjRol.Alta();
        } else {
            // Modifica
            await _ObjRol.Modifica();
        }
        await Inicio();
        spinnerClose();
        alertOk('La Rol se ha guardado correctamente.');
    } catch (e) {
        spinnerClose();
        alertAlerta('La Rol no se ha guardado. \n\n ' + e);
    }
});
$("body").on("click", "#BtnEliminar", async function () {
    PopUpConfirmar('warning', _ObjRol, 'Desea eliminar el Rol?', _ObjRol.Nombre, 'eventoEliminarRol', 'Si, Eliminar!', 'Cancelar', '#DD6B55');
});
// Escuchadores
document.addEventListener('seleccionRol', async function (e) {
    try {
        let objSeleccionado = e.detail;
        _ObjRol = objSeleccionado;
        await LLenarRol();
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('eventoEliminarRol', async function (e) {
    try {
        spinner();
        await _ObjRol.Baja();
        await Inicio();
        spinnerClose();
        alertOk('El Rol se ha eliminado correctamente.');
    } catch (e) {
        spinnerClose();
        alertAlerta('La Rol no se ha eliminado. \n\n ' + e);
    }
}, false);