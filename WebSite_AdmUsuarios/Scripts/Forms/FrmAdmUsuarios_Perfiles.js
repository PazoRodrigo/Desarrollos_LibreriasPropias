var _ObjPerfil;
var _ListaAccesos;
var _ObjNuevoAcceso;

$(document).ready(async function () {
    await Inicio();
});
async function Inicio() {
    _ObjPerfil = new Perfil();
    LimpiarPerfil();
    LimpiarRadiosRama();
    LimpiarFormulariosAcciones();
    _ListaAccesos = [];
    _ListaPerfiles = await Perfil.TraerTodos();
    console.log(_ListaPerfiles);
    await LLenarGrillaBuscadorPerfil(_ListaPerfiles);
    //await Rol.ArmarRadios('seleccionRol', 'DivRadioButtons', '');
    let listaRoles = await Rol.TraerTodos();
    await ArmarRadiosFamilia();
    await Rol.ArmarCombo(await Rol.TraerTodos(), 'seleccionRol', 'DivRadioButtons');

}
function LimpiarGrillaBuscador() {
    $('#DivGrillaLateralContenido').text('');
}
async function LLenarGrillaBuscadorPerfil(lista) {
    LimpiarGrillaBuscador();
    await Perfil.ArmarGrilla(lista, 'seleccionPerfil', 'DivGrillaLateralContenido', 'overflow-y:scroll; height:310px;');
}
function LimpiarPerfil() {
    $(".DatoFormulario").text('');
    $(".DatoFormulario").val('');
    $('#TxtNombre').focus();
    LimpiarRoles();
}
async function LLenarPerfil() {
    LimpiarPerfil();
    LimpiarRoles();
    $("#IdEntidad").text(_ObjPerfil.IdEntidad);
    $("#TxtNombre").val(_ObjPerfil.Nombre);
    await MarcarRol();
    await LlenarGrillaContenido();
    $('#TxtNombre').focus();
}
function LimpiarRadiosFamilia() {
    $('#DivGrillaContenido1').text('');
}
async function ArmarRadiosFamilia() {
    LimpiarRadiosFamilia();
    await Familia.ArmarRadios('seleccionFamilia', 'DivGrillaContenido1', 'width:100%; overflow-y:scroll; height:290px;');
}
function LimpiarRadiosRama() {
    $('#DivGrillaContenido2').text('');
}
async function ArmarRadiosRama(lista) {
    LimpiarRadiosRama();
    await Rama.ArmarRadios(lista, 'seleccionRama', 'DivGrillaContenido2', 'width:100%; overflow-y:scroll; height:290px;');
}
function LimpiarFormulariosAcciones() {
    $('#DivGrillaContenido3').text('');
}
async function ArmarFormulariosAcciones(lista) {
    LimpiarFormulariosAcciones();
    await Formulario.ArmarGrillaAcciones(lista, 'seleccionAcceso', 'DivGrillaContenido3', 'width:100%; overflow-y:scroll; height:250px;', true);

}
function LimpiarRoles() {
    $("input[name='rblRol']").prop('checked', false);
}
async function MarcarRol() {
    await Rol.ArmarCombo(await Rol.TraerTodos(), 'seleccionRol', 'DivRadioButtons');
    let objRol = await _ObjPerfil.ObjRol();
    $("#selectorRolCombo").text(objRol.Nombre);
}
function LimpiarGrillaContenido() {
    $('#DivGrillaContenido').text('');
}
async function LlenarGrillaContenido() {
    LimpiarGrillaContenido();
    console.log('sasasasas');
    await _ObjPerfil.ArmarGrillaContenido('DivGrillaContenido', 'padding-top:10px; overflow-y:scroll; height:370px;', true);
}
async function AgregarEliminarAcceso(str) {
    let objAcceso = new Acceso();
    objAcceso.IdAccion = str.charAt(0);
    objAcceso.IdFormulario = str.slice(2);

    if (_ListaAccesos.length == 0) {
        _ListaAccesos.push(objAcceso);
    }
    else {
        let buscado = $.grep(_ListaAccesos, function (entidad, index) {
            return entidad.IdFormulario == objAcceso.IdFormulario && entidad.IdAccion == objAcceso.IdAccion;
        });
        let Encontrado = buscado[0];
        if (Encontrado == undefined) {
            // No está, se agrega
            _ListaAccesos.push(objAcceso);
        }
        else {
            // Está, se elimina
            let buscado = $.grep(_ListaAccesos, function (entidad, index) {
                return !(entidad.IdFormulario == objAcceso.IdFormulario && entidad.IdAccion == objAcceso.IdAccion);
            });
            _ListaAccesos = buscado;
        }
    }
}
async function ArmarAccesos() {
    let listaResult = [];
    if (_ListaAccesos.length > 0) {
        for (let item of _ListaAccesos) {
            let buscado = $.grep(listaResult, function (entidad, index) {
                return entidad.IdFormulario == item.IdFormulario;
            });
            let Encontrado = buscado[0];
            let temp = 0;
            if (Encontrado == undefined) {
                listaResult.push(item);
            } else {
                temp = parseInt(Encontrado.IdAccion) + parseInt(item.IdAccion);
                let buscado2 = $.grep(listaResult, function (entidad, index) {
                    return !(entidad.IdFormulario == item.IdFormulario);
                });
                listaResult = buscado2;
                let objNuevo = new Acceso();
                objNuevo.IdFormulario = item.IdFormulario;
                objNuevo.IdAccion = temp;
                listaResult.push(objNuevo);
            }
        }
    }
    return listaResult;
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
async function ArmarStrUsuarios() {
    let str = '';
    let Lista = await _ObjPerfil.ListaUsuarios();
    return str;
}
// Buscador
$("body").on("keyup", "#TxtBuscadorNombre", async function () {
    let Resultado = [];
    Resultado = await BuscarTextoXCantCaracteres(2, $("#TxtBuscadorNombre").val(), await Perfil.TraerTodas(), _ListaPerfiles);
    __ListaPerfiles = Resultado;
    await LLenarGrillaBuscadorPerfil(Resultado);
});
// Botones
$("body").on("click", "#BtnBuscar", async function () {
    _ObjPerfil = new Perfil();
    alertInfo('En Construcción');
});
$("body").on("click", "#BtnNuevo", async function () {
    await Inicio();
});
$("body").on("click", "#BtnGuardar", async function () {
    try {
        spinner();
        _ObjPerfil.Nombre = $("#TxtNombre").val();
        _ObjPerfil.ListaAccesos = await ArmarAccesos();
        if (_ObjPerfil.IdEntidad == 0) {
            // Alta
            await _ObjPerfil.Alta();
        } else {
            // Modifica
             await _ObjPerfil.Modifica();
        }
        //await LimpiarPerfil();
        //_ListaPerfiles = await Perfil.TraerTodos();
        //await LLenarGrillaBuscadorPerfil(_ListaPerfiles);
        await Inicio();
        spinnerClose();
        alertOk('La Perfil se ha guardado correctamente.');
    } catch (e) {
        spinnerClose();
        alertAlerta('La Perfil no se ha guardado. \n\n ' + e);
    }
});
$("body").on("click", "#BtnEliminar", async function () {
    spinner();
    let texto = _ObjPerfil.Nombre + '\n';
    texto += '\n Usuarios: ' + await ArmarStrUsuarios();
    spinnerClose();
    PopUpConfirmar('warning', _ObjPerfil, 'Desea eliminar el Perfil?', texto, 'eventoEliminarPerfil', 'Si, Eliminar!', 'Cancelar', '#DD6B55');
});
$("body").on("click", "#BtnMenu", async function () {
    try {
        $('#Modal-PopUpMenu').remove();
        await Menu.ArmarPopUp(3, _ObjPerfil.IdEntidad);
    } catch (e) {
        alertAlerta('Error en Construcción de Menú \n\n ' + e);
    }
});
// Escuchadores
document.addEventListener('seleccionPerfil', async function (e) {
    try {
        let objSeleccionado = e.detail;
        _ObjPerfil = objSeleccionado;
        await LLenarPerfil();
        if (_ObjPerfil.IdEstado == 0) {
            await ArmarBotoneraSuperior();
        } else {
            $("#DivBtnBotoneraSuperior").html('');
        }
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('seleccionRol', async function (e) {
    try {
        let objSeleccionado = e.detail;
        _ObjPerfil.IdRol = objSeleccionado.IdEntidad;
        $("#selectorRolCombo").text(objSeleccionado.Nombre);
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('seleccionFamilia', async function (e) {
    try {
        let objSeleccionado = e.detail;
        LimpiarFormulariosAcciones();
        await ArmarRadiosRama(await objSeleccionado.ListaRamas());
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
document.addEventListener('seleccionAcceso', async function (e) {
    try {
        let objSeleccionado = e.detail;
        await AgregarEliminarAcceso(objSeleccionado);
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('eventoEliminarPerfil', async function (e) {
    try {
        spinner();
        await _ObjPerfil.Baja();
        spinnerClose();
        await Inicio();
        alertOk('El Perfil se ha eliminado correctamente.');
    } catch (e) {
        spinnerClose();
        alertAlerta('El Perfil no se ha eliminado. \n\n ' + e);
    }
}, false);
document.addEventListener('eventoModificarAcceso', async function (e) {
    try {
        $('#Modal-PopUpAcceso').remove();
        let objSeleccionado = e.detail;
        await Acceso.ArmarPopUp(_ObjPerfil, objSeleccionado);
        let lista = [];
        lista.push(await objSeleccionado.ObjFormulario());
        await Formulario.ArmarGrillaAcciones(lista, 'seleccionAcceso', 'divAccesoAccesos', 'width:100%;', false);
        await objSeleccionado.MarcarAcciones();
    } catch (e) {
        alertAlerta(e);
    }
}, false);
// PopUp Agregar Acceso
$("body").on("click", "#BtnAgregarAcceso", async function () {
    try {
        $('#Modal-PopUpAcceso').remove();
        await Acceso.ArmarPopUp(_ObjPerfil);
        await Familia.ArmarCombo(await Familia.Todos(), 'nuevoAccesoFamilia', 'divNuevoAccesoFamilia');
    } catch (e) {
        alertAlerta('Error en Construcción de Agregar Acceso \n\n ' + e);
    }
});
$("body").on("click", "#BtnGuardarAcceso", async function () {
    try {
        spinner();
        _ObjNuevoAcceso.IdAccion = await obtenerNuevoAcceso();
        $('#Modal-PopUpAcceso').remove();
        _ObjNuevoAcceso.IdPerfil = _ObjPerfil.IdEntidad;
        console.log(_ObjNuevoAcceso);
        await _ObjNuevoAcceso.Alta();
        _ObjPerfil = _ObjNuevoAcceso;
        await LLenarPerfil();
        if (_ObjPerfil.IdEstado == 0) {
            await ArmarBotoneraSuperior();
        } else {
            $("#DivBtnBotoneraSuperior").html('');
        }
        spinnerClose();
        alertOk('El Acceso se ha agregado correctamente.');
    } catch (e) {
        spinnerClose();
        alertAlerta('El Acceso no se ha agregado. \n\n ' + e);
    }
});
async function obtenerNuevoAcceso() {
    let IntAcciones = 0;
    if ($("#8_" + _ObjNuevoAcceso.IdFormulario + ":checked").length == 1) {
        IntAcciones += 8;
    }
    if ($("#4_" + _ObjNuevoAcceso.IdFormulario + ":checked").length == 1) {
        IntAcciones += 4;
    }
    if ($("#2_" + _ObjNuevoAcceso.IdFormulario + ":checked").length == 1) {
        IntAcciones += 2;
    }
    if ($("#1_" + _ObjNuevoAcceso.IdFormulario + ":checked").length == 1) {
        IntAcciones += 1;
    }
    return IntAcciones;
}
// Escuchadores PopUp
document.addEventListener('nuevoAccesoFamilia', async function (e) {
    try {
        _ObjNuevoAcceso = new Acceso();
        let objSeleccionado = e.detail;
        _ObjNuevoAcceso.IdFamilia = objSeleccionado.IdEntidad;
        $("#selectorFamiliaCombo").text(objSeleccionado.Nombre);
        $('#divNuevoAccesoRama').html('');
        $('#divNuevoAccesoFormulario').html('');
        $('#divNuevoAccesoAcceso').html('');
        await Rama.ArmarCombo(await objSeleccionado.ListaRamas(), 'nuevoAccesoRama', 'divNuevoAccesoRama');
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('nuevoAccesoRama', async function (e) {
    try {
        let objSeleccionado = e.detail;
        _ObjNuevoAcceso.IdRama = objSeleccionado.IdEntidad;
        $("#selectorRamaCombo").text(objSeleccionado.Nombre);
        $('#divNuevoAccesoFormulario').html('');
        $('#divNuevoAccesoAcceso').html('');
        await Formulario.ArmarCombo(await objSeleccionado.ListaFormularios(), 'nuevoAccesoFormulario', 'divNuevoAccesoFormulario');
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('nuevoAccesoFormulario', async function (e) {
    try {
        let objSeleccionado = e.detail;
        $("#selectorFormularioCombo").text(objSeleccionado.Nombre);
        $('#divNuevoAccesoAcceso').html('');
        let lista = [];
        lista.push(objSeleccionado);
        _ObjNuevoAcceso.IdFormulario = objSeleccionado.IdEntidad;
        await Formulario.ArmarGrillaAcciones(lista, 'eventoNuevoAcceso', 'divNuevoAccesoAccesos', '', false);
    } catch (e) {
        alertAlerta(e);
    }
}, false);
// PopUp Clonar
$("body").on("click", "#BtnClonarPerfil", async function () {
    try {
        $('#Modal-PopUpClonar').remove();
        await _ObjPerfil.ArmarPopUpClonar();
        $('#TxtNombreClonado').focus();

    } catch (e) {
        alertAlerta('Error en Construcción de Clonar Perfil \n\n ' + e);
    }
});
$("body").on("click", "#BtnClonar", async function () {
    try {
        spinner();
        let objClonado = new Perfil();
        objClonado.Nombre = $("#TxtNombreClonado").val();
        objClonado.IdRol = _ObjPerfil.IdRol;
        objClonado.ListaAccesos = await _ObjPerfil.ListaAccesos();
        await objClonado.Alta();
        await LimpiarPerfil();
        _ListaPerfiles = await Perfil.TraerTodos();
        await LLenarGrillaBuscadorPerfil(_ListaPerfiles);
        spinnerClose();
        alertOk('La Perfil se ha Clonado correctamente.');
    } catch (e) {
        spinnerClose();
        alertAlerta('La Perfil no se ha guardado. \n\n ' + e);
    }

});