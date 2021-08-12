var _ListaRepresentados;
var _TipoBusquedaRepresentado = 0;

class Representado {
    constructor() {
        this.NroDocumento = 0;
        this.TipoDocumento = '';
        this.ApellidoNombre = '';
        this.FechaNacimiento = 0;
        this.CUIL = 0;
        this.IdSexo = 0;
        this.FechaIngreso = 0;
        this.NroSindical = 0;
        this.IdAfiliado = 0;
        this.Observaciones = '';
        this.IdSeccional = 0;
        this.IdTipoAfiliado = 0;
        this.StrCuilConGuiones = '';
        this.Edad = 0;
        this.Sexo = '';
        this.FechaBaja = 0;

        this._ObjTipoRepresentado;
        this._ObjTipoDocumento;
        this._ObjSeccional;
    }

    async ObjTipoRepresentado() {
        try {
            if (this._ObjTipoRepresentado == undefined) {
                this._ObjTipoRepresentado = await TipoRepresentado.TraerUno(this.IdTipoAfiliado);
            }
            return this._ObjTipoRepresentado;
        } catch (e) {
            return new TipoRepresentado();
        }
    }
    async ObjTipoDocumento() {
        try {
            if (this._ObjTipoDocumento == undefined) {
                this._ObjTipoDocumento = await TipoDocumento.TraerUnoXSigla(this.TipoDocumento);
            }
            return this._ObjTipoDocumento;
        } catch (e) {
            let devuelveNada = new TipoDocumento();
            devuelveNada.IdEntidad = 0;
            devuelveNada.Sigla = '--';
            devuelveNada.Nombre = 'No Encontrado';
            return devuelveNada;

        }
    }
    async ObjSeccional() {
        try {
            if (this._ObjSeccional == undefined) {
                this._ObjSeccional = await Seccional.TraerUno(this.IdSeccional);
            }
            return this._ObjSeccional;
        } catch (e) {
            return new Seccional();
        }
    }
    // Traer
    static async TraerTodosXApellidoNombre(nombre) {
        _ListaRepresentados = [];
        let data = {
            'ApellidoNombre': nombre
        };
        let lista = await ejecutarAsync(urlWsRepresentado + "/TraerTodosXApellidoNombre", data);
        let result = [];
        $.each(lista, function (key, value) {
            result.push(llenarEntidadRepresentado(value));
        });
        _ListaRepresentados = result;
        return result;
    }
    static async TraerUnoXCUIL(CUIL) {
        _ListaRepresentados = [];
        let data = {
            'CUIL': CUIL
        };
        let lista = await ejecutarAsync(urlWsRepresentado + "/TraerUnoXCUIL", data);
        let result = [];
        $.each(lista, function (key, value) {
            result.push(llenarEntidadRepresentado(value));
        });
        _ListaRepresentados = result;
        return result;
    }
    static async TraerUnoXNroDocumento(NroDocumento) {
        _ListaRepresentados = [];
        let data = {
            'NroDocumento': NroDocumento
        };
        let lista = await ejecutarAsync(urlWsRepresentado + "/TraerUnoXNroDocumento", data);
        let result = [];
        $.each(lista, function (key, value) {
            result.push(llenarEntidadRepresentado(value));
        });
        _ListaRepresentados = result;
        return result;
    }
    static async TraerUnoXNroSindical(NroSindical) {
        _ListaRepresentados = [];
        let data = {
            'NroSindical': NroSindical
        };
        let lista = await ejecutarAsync(urlWsRepresentado + "/TraerUnoXNroSindical", data);
        let result = [];
        $.each(lista, function (key, value) {
            result.push(llenarEntidadRepresentado(value));
        });
        _ListaRepresentados = result;
        return result;
    }
    // Herramientas
    static ArmarUCBuscarRepresentado() {
        $("#grilla").html('');
        $("#divCuerpoUCTitular").html('');
        if ($("#Modal-PopUpRepresentado").length == 0) {
            let control = '';
            control += '<div id="Modal-PopUpRepresentado" class="modal" tabindex="-1" role="dialog" >';
            control += '    <div class="modal-dialog modal-lg">';
            control += '        <div class="modal-content">';
            control += '            <div class="modal-header">';
            control += '                <h2 class="modal-title">Buscar Representado</h2>';
            control += '            </div>';
            control += '            <div class="modal-body">';
            control += '                <div class="row">';
            control += '                    <div class="col-md-3"><h5>Apellido </h5></div>';
            control += '                    <div class="col-md-4"><input class="form-control input-sm TxtBuscarRepresentado" id="TxtBuscarNombreApellido" type="text" placeholder="Apellido" autocomplete="off"/></div>';
            control += '                    <div class="col-md-4 text-right"><a class="btn btn-primary btn-sm" id="BtnBuscarRepresentado" href="#">Buscar Representado</a><br></div>';
            control += '                </div>';
            control += '                <div class="row">';
            control += '                    <div class="col-md-3"><h5> CUIL</h5></div>';
            control += '                    <div class="col-md-4"><input class="form-control input-sm TxtBuscarRepresentado text-center" style="width:110px" id="TxtBuscarCUIL" type="text" placeholder="(11 números)" onkeypress="return jsSoloNumeros(event);" maxlength="11" autocomplete="off"/></div>';
            control += '                </div>';
            control += '                <div class="row">';
            control += '                    <div class="col-md-3"><h5> Nro. Documento</h5></div>';
            control += '                    <div class="col-md-4"><input class="form-control input-sm TxtBuscarRepresentado text-center" style="width:90px" id="TxtBuscarNroDocumento" type="text" placeholder="(8 números)" onkeypress="return jsSoloNumeros(event);" maxlength="8" autocomplete="off"/></div>';
            control += '                </div>';
            control += '                <div class="row">';
            control += '                    <div class="col-md-3"><h5> Nro. Sincical</h5></div>';
            control += '                    <div class="col-md-4"><input class="form-control input-sm TxtBuscarRepresentado text-center " style="width:90px" id="TxtBuscarNroSindical" type="text" placeholder="(7 números)" onkeypress="return jsSoloNumeros(event);" maxlength="7" autocomplete="off"/></div>';
            control += '                </div>';
            control += '                <br />';
            control += '                <div id="grilla"></div>';
            control += '            </div>';
            control += '            <div class="modal-footer">';
            control += '                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>';
            control += '            </div>';
            control += '        </div>';
            control += '    </div>';
            control += '</div>';
            $("body").append(control);
        }
        $('#Modal-PopUpRepresentado').modal({ show: true });
    }
    static ArmarUCFamiliares() {
    }

    static async ArmarGrillaRepresentados(div, estilo) {
        $('#' + div + '').html('');
        let lista = [];
        switch (_TipoBusquedaRepresentado) {
            case 1:
                lista = await Representado.TraerTodosXApellidoNombre($('#TxtBuscarNombreApellido').val());
                break;
            case 2:
                lista = await Representado.Traer($('#TxtBuscarCUIL').val());
                break;
            case 3:
                lista = await Representado.TraerUnoXNroDocumento($('#TxtBuscarNroDocumento').val());
                break;
            case 4:
                lista = await Representado.TraerTodosXApellidoNombre($('#TxtBuscarNroSindical').val());
                break;
            default:
                throw alertAlerta('Error de Tipo Busqueda');
        }
        let str = '';
        if (lista.length > 0) {
            str += '<div style="' + estilo + '">';
            str += '    <table class="table table-bordered" style="width: 100%;">';
            str += '        <thead>';
            str += '            <tr>';
            str += '                <th style="width:5%; text-align: center;"></th>';
            str += '                <th style="width:20%; text-align: center;">Nro. Documento</th>';
            str += '                <th style="text-align: center;">Apellido y Nombre</th>';
            str += '                <th style="text-align: center;">CUIL</th>';
            str += '                <th style="text-align: center;">Tipo Representado</th>';
            str += '                <th style="width:5%; text-align: center;"></th>';
            str += '            </tr>';
            str += '        </thead>';
            str += '        <tbody>';
            for (let item of lista) {
                let btnSeleccion = '<a href="#" class="btn btn-primary btn-xs mibtn-seleccionRepresentado glyphicon glyphicon-search" data-Id="' + item.IdAfiliado + '"></a></td>';
                let btnImpresion = '<a href="#" class="btn btn-info btn-xs mibtn-seleccionRepresentado glyphicon glyphicon-print" data-Id="' + item.IdAfiliado + '"></a></td>';
                let ColorLinea = 'black';
                if (item.FechaBaja > 0) {
                    ColorLinea = 'red';
                }
                str += String.format('<tr style="color: ' + ColorLinea + '"><td align="center" valign="middle">{0}</td><td align="right">{1}</td><td>{2}</td><td align="center">{3}</td><td>{4}</td><td align="center" valign="middle">{5}</td></tr>', btnSeleccion, item.NroDocumento, item.ApellidoNombre, item.CUIL, (await item.ObjTipoRepresentado()).Nombre, btnImpresion);
            }
            str += '        </tbody>';
            str += '    </table>';
            str += '</div>';
        }
        return $('#' + div + '').html(str);
    }
    // Validaciones
    static async ValidarBusqueda() {
        if ($('#TxtBuscarNombreApellido').val().length > 0) {
            _TipoBusquedaRepresentado = 1;
        }
        if ($('#TxtBuscarCUIL').val().length > 0) {
            _TipoBusquedaRepresentado = 2;
        }
        if ($('#TxtBuscarNroDocumento').val().length > 0) {
            _TipoBusquedaRepresentado = 3;
        }
        if ($('#TxtBuscarNroSindical').val().length > 0) {
            _TipoBusquedaRepresentado = 4;
        }
        if (_TipoBusquedaRepresentado == 0) {
            throw 'Seleccione un Tipo de Búsqueda';
        }
    }
}
function llenarEntidadRepresentado(entidad) {
    let m = new Representado;
    m.NroDocumento = entidad.NroDocumento;
    m.TipoDocumento = entidad.TipoDocumento;
    m.ApellidoNombre = entidad.ApellidoNombre;
    m.FechaNacimiento = entidad.FechaNacimiento;
    m.CUIL = entidad.CUIL;
    m.IdSexo = entidad.IdSexo;
    m.FechaIngreso = entidad.FechaIngreso;
    m.NroSindical = entidad.NroSindical;
    m.IdAfiliado = entidad.IdAfiliado;
    m.Observaciones = entidad.Observaciones;
    m.IdSeccional = entidad.IdSeccional;
    m.IdTipoAfiliado = entidad.IdTipoAfiliado;
    m.StrCuilConGuiones = entidad.StrCuilConGuiones;
    m.Edad = entidad.Edad;
    m.Sexo = entidad.Sexo;
    m.FechaBaja = entidad.FechaBaja;
    return m;
}
// Botones
$('body').on('click', '.TxtBuscarRepresentado', function () {
    $(".TxtBuscarRepresentado").css({ "background-color": "Gainsboro" });
    $(".TxtBuscarRepresentado").val('');
    let $this = this;
    $($this).css({ "background-color": "white" });
    $($this).focus();
});
$('body').on('click', '#BtnBuscarRepresentado', async function () {
    try {
        await Representado.ValidarBusqueda();
        await Representado.ArmarGrillaRepresentados('grilla', 'max-height: 300px;overflow-y: scroll;');
    } catch (e) {
        alertAlerta(e);
    }

});
$('body').on('keyup', '.TxtBuscarRepresentado', async function (e) {
    try {
        if (e.keyCode == 13) {
            await Representado.ValidarBusqueda();
            await Representado.ArmarGrillaRepresentados('grilla', 'max-height: 300px;overflow-y: scroll;');
        }
    } catch (e) {
        alertAlerta(e);
    }
});
$('body').on('click', ".mibtn-seleccionRepresentado", async function () {
    try {
        $this = $(this);
        let buscado = $.grep(_ListaRepresentados, function (entidad, index) {
            return entidad.IdAfiliado == $this.attr("data-Id");
        });
        let Seleccionado = buscado[0];
        let event = new CustomEvent('RepresentadoSeleccionado', { detail: Seleccionado });
        $("#Modal-PopUpRepresentado").modal("hide");
        document.dispatchEvent(event);
    } catch (e) {
        alertAlerta(e);
    }
});
