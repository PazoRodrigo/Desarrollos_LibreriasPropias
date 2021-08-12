var _ListaUsuarios;

class Usuario {
    constructor() {
        this.IdEntidad = 0;
        this.UserLogin = "";
        this.Password = "";
        this.Nombre = "";
        this.CorreoElectronico = "";
        this.IdPerfil = 0;
        this.IdRol = 0;
        this.IdArea = 0;
        this.Roles = [];
    }

    static async Todos() {
        if (_ListaUsuarios == undefined) {
            _ListaUsuarios = await Usuario.TraerTodas();
        }
        return _ListaUsuarios;
    }
    // Traer
    static async TraerTodos() {
        return await Usuario.Todos();
    }
    static async TraerTodas() {
        let lista = await ejecutarAsync(urlWsUsuario + "/TraerTodos");
        let result = [];
        $.each(lista, function (key, value) {
            if (value.Nombre.length > 0) {
                result.push(LlenarEntidadUsuario(value));
            }
        });
        _ListaUsuarios = lista;
        return result;
    }

    // Casos de Uso
    static async HaciendoLogIN(usuario, password) {
        let data = {
            'a': usuario,
            'b': password
        };
        let ListaResult = await ejecutarAsync(urlWsUsuario + "/HaciendoLogIN", data);
        //_Lista = ListaResult;
        //console.log(ListaResult);
        let _Listaa = [];
        _Listaa = ListaResult;

        //let ListaRoles = [];
        let Result = [];
        Result.IdEntidad = ListaResult.IdEntidad;
        Result.Nombre = ListaResult.Nombre;
        //console.log(Result);
        //$.each(Result.ListaRoles, function (key, value) {
        //    result.push(LlenarEntidadRol(value));
        //});

        //console.log(result);
        //$.each(ObjUsuario.ListaRoles, function (key, value) {
        //    ListaRoles.push(LlenarEntidadRol(value));
        //});
        //console.log(ListaRoles);

        //result.ListaRoles = ListaRoles;
        return _Listaa;
    }

    //static async HaciendoLogOUT() {
    //    //let a = urlWsUsuario + "/HaciendoLogOut";
    //    //alert(a);
    //    await ejecutarAsync(urlWsUsuario + "/HaciendoLogOut");
    //}

    static async TraerUno(IdEntidad) {
        _ListaUsuarios = await Usuario.TraerTodos();
        let buscado = $.grep(_ListaUsuarios, function (entidad, index) {
            return entidad.IdEntidad == IdEntidad;
        });
        let Encontrado = buscado[0];
        return Encontrado;
    }
    // Herramientas
    static async ArmarCombo(div, evento) {
        let lista = await Usuario.Todos();
        let cbo = "";
        cbo += '<div id="CboSeccional" class="dropdown">';
        cbo += '<button id="selectorSeccional" class="btn btn-primary dropdown-toggle btn-md btn-block" type="button" data-toggle="dropdown">';
        cbo += 'Delegación<span class="caret"></span>';
        cbo += '</button>';
        cbo += '<ul class="dropdown-menu">';
        $(lista).each(function () {
            cbo += '<li><a href="#" class="mibtn-seleccionSeccional" data-Id="' + this.IdEntidad + '" data-Nombre="' + this.Nombre + '" data-Evento="' + evento + '" > ' + this.Codigo() + ' - ' + this.Nombre + '</a></li>';
        });
        cbo += '</ul>';
        cbo += '</div>';
        return $('#' + div + '').html(cbo);
    }
    static async ArmarGrilla(div, evento, estilo) {
        let ListaSeccionales = await Seccional.Todos();
        $('#' + div + '').html('');
        let str = '';
        if (ListaSeccionales.length > 0) {
            str += '<div style="' + estilo + '">';
            str += '    <table class="table table-bordered" style="width: 100%;">';
            str += '        <thead>';
            str += '            <tr>';
            str += '                <th style="width:5%; text-align: center;"></th>';
            str += '                <th style="text-align: center;">Seccional</th>';
            str += '            </tr>';
            str += '        </thead>';
            str += '        <tbody>';
            for (let item of ListaSeccionales) {
                let colorLupa = 'primary';
                //if (listaAfiliado.length > 0) {
                //    let buscado = $.grep(listaAfiliado, function (entidad, index) {
                //        return entidad.IdSeccional == item.IdEntidad;
                //    });
                //    if (buscado[0]) {
                //        colorLupa = 'warning';
                //    }
                //}
                let btnSeleccion = '<a href="#" class="btn btn-' + colorLupa + ' btn-xs mibtn-seleccionSeccional glyphicon glyphicon-search" data-Id="' + item.IdEntidad + '" data-evento="' + evento + '"></a></td>';
                str += String.format('<tr><td align="center" valign="middle">{0}</td><td align="center">{1}</td></tr>', btnSeleccion, item.Nombre);
            }
            str += '        </tbody>';
            str += '    </table>';
            str += '</div>';
        }
        return $('#' + div + '').html(str);
    }
}
function LlenarEntidadUsuario(entidad) {
    //console.log(entidad);
    let m = new Usuario;
    m.IdEntidad = entidad.IdEntidad;
    m.UserLogin = entidad.UserLogin;
    m.Password = entidad.Password;
    m.Nombre = entidad.Nombre;
    m.CorreoElectronico = entidad.CorreoElectronico;
    //m.Roles = entidad.ListaRoles;
    //console.log(entidad);
    //console.log(m);

    return m;
}

class Rol {
    constructor() {
        this.IdEntidad = 0;
        this.Nombre = "";
    }
}

function LlenarEntidadRol(entidad) {
    let r = new Rol;
    r.IdEntidad = entidad.IdEntidad;
    r.Nombre = entidad.Nombre;
    return r;
}

$('body').on('click', ".mibtn-seleccionUsuario", async function () {
    try {
        $this = $(this);
        let buscado = $.grep(_ListaUsuarios, function (entidad, index) {
            return entidad.IdEntidad == $this.attr("data-Id");
        });
        let Seleccionado = buscado[0];
        let evento = $this.attr("data-Evento");
        let event = new CustomEvent(evento, { detail: Seleccionado });
        document.dispatchEvent(event);
    } catch (e) {
        alertAlerta(e);
    }
});

