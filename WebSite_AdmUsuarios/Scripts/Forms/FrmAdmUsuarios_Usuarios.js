var _ObjUsuario;
var _RolesPosibles;
var _ListaChkRol = [];
var _ListaPerfilesMenu;

$(document).ready(async function () {
    await Inicio();
});
async function Inicio() {
    await LimpiarUsuario();
    _ListaUsuarios = await Usuario.TraerTodos();
    await LLenarGrillaBuscadorUsuario(_ListaUsuarios);
    await LLenarPerfiles();
}
async function LimpiarUsuario() {
    _ObjUsuario = new Usuario();
    $(".DatoFormulario").text('');
    $(".DatoFormulario").val('');
    await Area.ArmarCheckBoxs('seleccionArea', 'Tab_Areas', 'overflow-y:scroll; max-height:310px;');
    await Seccional.ArmarCheckBoxs('seleccionSeccional', 'Tab_Seccionales', 'overflow-y:scroll; max-height:310px;');
    _ListaPerfilesMenu = [];
    $('#DB_NroDocumento_CUIT').focus();
    //await Rol.ArmarCheckBoxs('seleccionRol', 'Tab_Perfiles_Roles_Contenido', 'disabled');
    //let listaRoles = $('input[name=CkbList_Roles');
    //for (let item of listaRoles) {
    //    let obj = [];
    //    obj.IdRol = item.value;
    //    obj.cantidad = 0;
    //    _ListaChkRol.push(obj);
    //}
}
async function LLenarUsuario() {
    $("#IdEntidad").text(_ObjUsuario.IdEntidad);
    $("#DB_NroDocumento_CUIT").val(_ObjUsuario.Documento_CUIT);
    $("#DB_NombreApellido_RazonSocial").val(_ObjUsuario.Nombre);
    $("#DB_UserLogin").val(_ObjUsuario.Login);
    $("#DB_Telefono").val(_ObjUsuario.Telefono);
    $("#DB_CorreoElectronico").val(_ObjUsuario.CorreoElectronico);
    $('#DB_NroDocumento_CUIT').focus();
    await LLenarPerfiles();
}
async function LLenarPerfiles() {
    console.log(await _ObjUsuario.ListaPerfiles());
    let listaResult = [];
    let ListaPerfilesPosibles = await Perfil.TraerTodos();
    console.log(ListaPerfilesPosibles);

    if (ListaPerfilesPosibles.length > 0) {
        for (let objRol of _RolesPosibles) {
            let buscado = $.grep(ListaPerfilesPosibles, function (entidad, index) {
                return entidad.IdRol == objRol.IdEntidad;
            });
            if (buscado.length > 0) {
                for (let objPerfil of buscado) {
                    listaResult.push(objPerfil);
                }
            }
        }
    }
    console.log(listaResult);
    await Perfil.ArmarCheckBoxs(listaResult, 'seleccionPerfil', 'Tab_Perfiles_Perfiles_Contenido', '');


    //let listaResult = [];
    //let ListaPerfilesPosibles = await Perfil.TraerTodos();
    //if (ListaPerfilesPosibles.length > 0) {
    //    for (let objRol of _RolesPosibles) {
    //        let buscado = $.grep(ListaPerfilesPosibles, function (entidad, index) {
    //            return entidad.IdRol == objRol.IdEntidad;
    //        });
    //        if (buscado.length > 0) {
    //            for (let objPerfil of buscado) {
    //                listaResult.push(objPerfil);
    //            }
    //        }
    //    }
    //}
    //await Perfil.ArmarCheckBoxs(listaResult, 'seleccionPerfil', 'Tab_Perfiles_Perfiles_Contenido', '');
}
function LimpiarGrillaBuscador() {
    $('#DivGrillaLateralContenido').text('');
}
async function LLenarGrillaBuscadorUsuario(lista) {
    LimpiarGrillaBuscador();
    await Usuario.ArmarGrilla(lista, 'seleccionUsuario', 'DivGrillaLateralContenido', 'overflow-y:scroll; height:310px;');
}
//async function EstadoCheck(IdRol, marcado) {
//    try {
//        let buscado = $.grep(_ListaChkRol, function (entidad, index) {
//            return entidad.IdRol == IdRol;
//        });
//        let Encontrado = buscado[0];
//        if (marcado == true) {
//            Encontrado.cantidad++;
//        } else {
//            Encontrado.cantidad--;
//        }
//        let cantidad = Encontrado.cantidad;
//        let check = false;
//        if (cantidad > 0)
//            check = true;
//        return check;
//    } catch (e) {
//        alertAlerta(e);
//    }
//};
async function ListaPerfilesMenu(objPerfil, marcado) {
    if (_ListaPerfilesMenu.length == 0) {
        _ListaPerfilesMenu.push(objPerfil);
    } else {
        let buscado1 = $.grep(_ListaPerfilesMenu, function (entidad, index) {
            return entidad.IdEntidad == objPerfil.IdEntidad;
        });
        if (buscado1[0] == undefined) {
            _ListaPerfilesMenu.push(objPerfil);
        } else {
            let buscado2 = $.grep(_ListaPerfilesMenu, function (entidad, index) {
                return entidad.IdEntidad != objPerfil.IdEntidad;
            });
            _ListaPerfilesMenu = buscado2;
        }
    }
    await Acceso.ArmarLista('Tab_Accesos', _ListaPerfilesMenu, 'overflow-y:scroll; max-height:300px;');
}
// Buscador
$("body").on("keyup", "#TxtBuscadorNombre", async function () {
    let Resultado = [];
    Resultado = await BuscarTextoXCantCaracteres(2, $("#TxtBuscadorNombre").val(), await Usuario.TraerTodas(), _ListaUsuarios);
    __ListaUsuarios = Resultado;
    await LLenarGrillaBuscadorUsuario(Resultado);
});
// Botones
$("body").on("click", "#BtnBuscar", async function () {
    _ObjUsuario = new Usuario();
    alertInfo('En Construcción');
});
$("body").on("click", "#BtnNuevo", async function () {
    try {
        _ObjUsuario = new Usuario();
        await LimpiarUsuario();
    } catch (e) {

    }
});
$("body").on("click", "#BtnGuardar", async function () {
    try {
        spinner();
        let listaPerfiles = $('input[name=CkbList_Perfiles');
        let listaRoles = $('input[name=CkbList_Roles');
        let listaAreas = $('input[name=CkbList_Areas');
        let listaSeccionales = $('input[name=CkbList_Seccionales');
        let listaUsuarioAreas = [];
        for (let item of listaAreas) {
            if (item.checked == true) {
                let objArea = new Area();
                objArea.IdEntidad = item.value;
                listaUsuarioAreas.push(objArea);
            }
        }
        let listaUsuarioSeccionales = [];
        for (let item of listaSeccionales) {
            if (item.checked == true) {
                let objSeccional = new Seccional();
                objSeccional.IdEntidad = item.value;
                listaUsuarioSeccionales.push(objSeccional);
            }
        }
        let listaUsuarioPerfiles = [];
        for (let item of listaPerfiles) {
            if (item.checked == true) {
                let objPerfil = new Perfil();
                objPerfil.IdEntidad = item.value;
                listaUsuarioPerfiles.push(objPerfil);
            }
        }
        _ObjUsuario.listaAreas = listaUsuarioAreas;
        _ObjUsuario.listaSeccionales = listaUsuarioSeccionales;
        _ObjUsuario.listaPerfiles = listaUsuarioPerfiles;

        _ObjUsuario.Nombre = $("#DB_NroDocumento_CUIT").val();
        _ObjUsuario.Documento_CUIT = $("#DB_NombreApellido_RazonSocial").val();
        _ObjUsuario.Login = $("#DB_UserLogin").val();
        _ObjUsuario.Telefono = $("#DB_Telefono").val();
        _ObjUsuario.CorreoElectronico = $("#DB_CorreoElectronico").val();
        let msgOk = '';
        if (_ObjUsuario.IdEntidad == 0) {
            // Alta
            await _ObjUsuario.Alta();
            msgOk = 'El Usuario y Contraseña serán enviadas vía Correo Electrónico.';
        } else {
            // Modifica
            // await _ObjUsuario.Modifica();
            alertAlerta('Modificación En Construcción');
        }

        LimpiarUsuario();
        await LLenarGrillaBuscadorUsuario();
        spinnerClose();
        alertOk('La Usuario se ha guardado correctamente. \n\n' + msgOk);
    } catch (e) {
        spinnerClose();
        alertAlerta('La Usuario no se ha guardado. \n\n ' + e);
    }
});
$("body").on("click", "#BtnValidarUsuario", async function () {
    try {
        let validador = $("#DB_NroDocumento_CUIT").val();
        _RolesPosibles = [];
        let objRol;
        let temp = '';
        if (validador.length != 7 && validador.length != 8 && validador.length != 11) {
            throw ("Para validar deben ser 7,8 o 11 dígitos");
        } else {
            if (validador.length == 11) {
                objRol = new Rol();
                objRol.IdEntidad = 1;
                _RolesPosibles.push(objRol);
            } else {
                objRol = new Rol();
                objRol.IdEntidad = 2;
                _RolesPosibles.push(objRol);
                objRol = new Rol();
                objRol.IdEntidad = 3;
                _RolesPosibles.push(objRol);
            }
        }
        await LLenarPerfiles();
    } catch (e) {
        alertAlerta(e);
    }
});
$("body").on("click", "#Btn_GenerarNotificacion", async function () {
    try {
        _ObjUsuario = new Usuario();
        alertInfo('En Construcción \n\n Genera la notificación para ser impresa');
    } catch (e) {
        alertAlerta('' + e);
    }
});
// Escuchadores
document.addEventListener('seleccionUsuario', async function (e) {
    try {
        let objSeleccionado = e.detail;
        _ObjUsuario = objSeleccionado;
        await LLenarUsuario();
    } catch (e) {
        alertAlerta(e);
    }
}, false);
