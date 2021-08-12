var _ListaEmpresas;
var _TipoBusquedaEmpresa = 0;

class Empresa {
    constructor() {
        this.IdEntidad = 0;
        this.CodEmpresa = 0;
        this.Denominacion = '';
        this.DenominacionCarnet = '';
        this.Dependencia = 0;
        this.CUIT = 0;
        this.IdSeccional = 0;
        this.Direccion = '';
        this.IdProvincia = 0;
        this.TipoEntidad = '';
        this.Contacto_DDN = 0;
        this.Contacto_Telefono = '';
        this.Localidad = '';
        this.CodPostal = 0;
        this.Email = '';
        this.Procedencia = '';
        this.DireccionAlternativa = '';

        this._ObjSeccional;
    }
    StrCodigoEmpresa5() {
        return Right('00000' + this.CodEmpresa, 5);
    }
    StrCodigoEmpresa11() {
        return Right('00000000000' + this.CodEmpresa, 11);
    }
    StrDependencia2() {
        return Right('00' + this.Dependencia, 2);
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
    static async TraerTodosXCodEmpresa(CodEmpresa) {
        _ListaEmpresas = [];
        let data = {
            'CodEmpresa': CodEmpresa
        };
        let lista = await ejecutarAsync(urlWsEmpresa + "/TraerTodosXCodEmpresa", data);
        let result = [];
        $.each(lista, function (key, value) {
            result.push(llenarEntidadEmpresa(value));
        });
        _ListaEmpresas = result;
        return result;
    }
    static async TraerTodosXCuit(CUIT) {
        _ListaEmpresas = [];
        let data = {
            'CUIT': CUIT
        };
        let lista = await ejecutarAsync(urlWsEmpresa + "/TraerTodosXCuit", data);
        let result = [];
        $.each(lista, function (key, value) {
            result.push(llenarEntidadEmpresa(value));
        });
        _ListaEmpresas = result;
        return result;
    }
    static async TraerTodosXDenominacion(Denominacion) {
        _ListaEmpresas = [];
        let data = {
            'Denominacion': Denominacion
        };
        let lista = await ejecutarAsync(urlWsEmpresa + "/TraerTodosXDenominacion", data);
        let result = [];
        $.each(lista, function (key, value) {
            result.push(llenarEntidadEmpresa(value));
        });
        _ListaEmpresas = result;
        return result;
    }
    // Herramientas
    static ArmarUCBuscarEmpresa() {
        $("#grilla").html('');
        $("#divCuerpoUCTitular").html('');
        if ($("#Modal-PopUpEmpresa").length == 0) {
            let control = '';
            control += '<div id="Modal-PopUpEmpresa" class="modal" tabindex="-1" role="dialog" >';
            control += '    <div class="modal-dialog modal-lg">';
            control += '        <div class="modal-content">';
            control += '            <div class="modal-header">';
            control += '                <h2 class="modal-title">Buscar Empresa</h2>';
            control += '            </div>';
            control += '            <div class="modal-body">';
            control += '                <div class="row">';
            control += '                    <div class="col-md-3"><h5>Apellido </h5></div>';
            control += '                    <div class="col-md-4"><input class="form-control input-sm TxtBuscarEmpresa" id="TxtBuscarDenominacion" type="text" placeholder="Denominacion" autocomplete="off"/></div>';
            control += '                    <div class="col-md-4 text-right"><a class="btn btn-primary btn-sm" id="BtnBuscarEmpresa" href="#">Buscar Empresa</a><br></div>';
            control += '                </div>';
            control += '                <div class="row">';
            control += '                    <div class="col-md-3"><h5>Código</h5></div>';
            control += '                    <div class="col-md-4"><input class="form-control input-sm TxtBuscarEmpresa" style="width:130px" id="TxtBuscarCodigo" type="text" placeholder="(5 a 11 números)" onkeypress="return jsSoloNumeros(event);" maxlength="11" autocomplete="off"/></div>';
            control += '                </div>';
            control += '                <div class="row">';
            control += '                    <div class="col-md-3"><h5> CUIT</h5></div>';
            control += '                    <div class="col-md-4"><input class="form-control input-sm TxtBuscarEmpresa" style="width:130px" id="TxtBuscarCUIT" type="text" placeholder="(11 números)" onkeypress="return jsSoloNumeros(event);" maxlength="11" autocomplete="off"/></div>';
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
        $('#Modal-PopUpEmpresa').modal({ show: true });
    }
    static async ArmarGrilla(div, estilo) {
        $('#' + div + '').html('');
        let lista = [];
        switch (_TipoBusquedaEmpresa) {
            case 1:
                lista = await Empresa.TraerTodosXDenominacion($('#TxtBuscarDenominacion').val());
                break;
            case 2:
                lista = await Empresa.TraerTodosXCodEmpresa($('#TxtBuscarCodigo').val());
                break;
            case 3:
                lista = await Empresa.TraerTodosXCuit($('#TxtBuscarCUIT').val());
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
            str += '                <th style="width:10%; text-align: center;">Código</th>';
            str += '                <th style="text-align: center;">Denominación</th>';
            str += '                <th style="width:5%; text-align: center;">Dep</th>';
            str += '                <th style="width:10%; text-align: center;">CUIT</th>';
            str += '                <th style="width:15%; text-align: center;">Seccional</th>';
            str += '            </tr>';
            str += '        </thead>';
            str += '        <tbody>';
            for (let item of lista) {
                let btnSeleccion = '<a href="#" class="btn btn-primary btn-xs mibtn-seleccionEmpresa glyphicon glyphicon-search" data-Id="' + item.IdEntidad + '"></a></td>';
                str += String.format('<tr><td align="center" valign="middle">{0}</td><td align="center">{1}</td><td align="left">{2}</td><td align="center">{3}</td><td align="center">{4}</td><td align="center">{5}</td></tr>', btnSeleccion, item.StrCodigoEmpresa5(), item.Denominacion, item.StrDependencia2(), item.CUIT, (await item.ObjSeccional()).Nombre);
            }
            str += '        </tbody>';
            str += '    </table>';
            str += '</div>';
        }
        return $('#' + div + '').html(str);
    }

    // Validaciones
    static async ValidarBusqueda() {
        if ($('#TxtBuscarDenominacion').val().length > 0) {
            _TipoBusquedaEmpresa = 1;
        }
        if ($('#TxtBuscarCodigo').val().length > 0) {
            _TipoBusquedaEmpresa = 2;
        }
        if ($('#TxtBuscarCUIT').val().length > 0) {
            _TipoBusquedaEmpresa = 3;
        }
        if (_TipoBusquedaEmpresa == 0) {
            throw 'Seleccione un Tipo de Búsqueda';
        }
    }
}
function llenarEntidadEmpresa(entidad) {
    let m = new Empresa;
    m.IdEntidad = entidad.IdEntidad;
    m.CodEmpresa = entidad.CodEmpresa;
    m.Denominacion = entidad.Denominacion;
    m.DenominacionCarnet = entidad.DenominacionCarnet;
    m.Dependencia = entidad.Dependencia;
    m.CUIT = entidad.CUIT;
    m.IdSeccional = entidad.IdSeccional;
    m.Direccion = entidad.Direccion;
    m.IdProvincia = entidad.IdProvincia;
    m.TipoEntidad = entidad.TipoEntidad;
    m.Contacto_DDN = entidad.Contacto_DDN;
    m.Contacto_Telefono = entidad.Contacto_Telefono;
    m.Localidad = entidad.Localidad;
    m.CodPostal = entidad.CodPostal;
    m.Email = entidad.Email;
    m.Procedencia = entidad.Procedencia;
    m.DireccionAlternativa = entidad.DireccionAlternativa;
    return m;
}
$('body').on('click', '.TxtBuscarEmpresa', function () {
    $(".TxtBuscarEmpresa").css({ "background-color": "Gainsboro" });
    $(".TxtBuscarEmpresa").val('');
    let $this = this;
    $($this).css({ "background-color": "white" });
    $($this).focus();
});
$('body').on('click', '#BtnBuscarEmpresa', async function () {
    try {
        await Empresa.ValidarBusqueda();
        await Empresa.ArmarGrilla('grilla', 'max-height: 300px;overflow-y: scroll;');
    } catch (e) {
        alertAlerta(e);
    }
});
$('body').on('keyup', '.TxtBuscarEmpresa', async function (e) {
    try {
        if (e.keyCode == 13) {
            await Empresa.ValidarBusqueda();
            await Empresa.ArmarGrilla('grilla', 'max-height: 300px;overflow-y: scroll;');
        }
    } catch (e) {
        alertAlerta(e);
    }
});
$('body').on('click', ".mibtn-seleccionEmpresa", async function () {
    try {
        $this = $(this);
        let buscado = $.grep(_ListaEmpresas, function (entidad, index) {
            return entidad.IdEntidad == $this.attr("data-Id");
        });
        let Seleccionado = buscado[0];
        let event = new CustomEvent('EmpresaSeleccionada', { detail: Seleccionado });
        $("#Modal-PopUpEmpresa").modal("hide");
        document.dispatchEvent(event);
    } catch (e) {
        alertAlerta(e);
    }
});

