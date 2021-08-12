var _ListaSeccionales;

class Seccional {
    constructor() {
        this.IdEntidad = 0;
        this.Nombre = '';
    }

    // Traer
    static async Todos() {
        if (_ListaSeccionales == undefined) {
            _ListaSeccionales = await Seccional.TraerTodas();
        }
        _ListaSeccionales.sort(SortXNombre);
        return _ListaSeccionales;
    }
    static async TraerTodos() {
        return await Seccional.Todos();
    }
    static async TraerTodas() {
        let lista = await ejecutarAsync(urlWsSeccional + "/TraerTodos");
        _ListaSeccionales = [];
        let result = [];
        $.each(lista, function (key, value) {
            result.push(llenarEntidadSeccional(value));
        });
        _ListaSeccionales = result;
        _ListaSeccionales.sort(SortXNombre);
        return _ListaSeccionales;
    }
    static async ArmarCheckBoxs(evento, div, estilo) {
        $('#' + div + '').html('');
        let str = '';
        str += '<div style="' + estilo + '">';
        await Seccional.Refresh();
        let lista = await Seccional.Todos();
        if (lista.length > 0) {
            for (let item of lista) {
                str += '<div class="col-lg-4"><input type="checkbox" class="micbx-Area" name="CkbList_Seccionales"  value="' + item.IdEntidad + '"   id="chk_' + item.IdEntidad + '" /><label for="chk_' + item.IdEntidad + '"> ' + item.Nombre + '</label></div>';
            }
        }
        str += '</div>';
        return $('#' + div + '').html(str);
    }
    static async Refresh() {
        _ListaSeccionales = await Seccional.TraerTodas();
    }
}
function llenarEntidadSeccional(entidad) {
    let m = new Seccional;
    m.IdEntidad = entidad.IdEntidad;
    m.Nombre = entidad.Nombre;
    return m;
}
