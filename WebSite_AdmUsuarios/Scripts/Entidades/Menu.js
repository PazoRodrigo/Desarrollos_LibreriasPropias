
class Menu {
    constructor() {
        this.IdEntidad = 0;
    }

    static async LimpiarMenu() {
        $('.modal-content').empty();
        $('#Modal-PopUpMenu').removeData('bs.modal');
    }
    static async ArmarPopUp(tipo, Id) {
        await Menu.LimpiarMenu();
        let control = '';
        let str = '';
        let MenuListaFormularios = [];
        let MenuListaRamas = [];
        let MenuListaAccesos = [];
        let nombreMenu = '';
        let objArmar;
        try {
            spinner();
            switch (tipo) {
                case 1:
                    // Rama
                    objArmar = await Rama.TraerUno(Id);
                    nombreMenu += 'Rama  ';
                    MenuListaRamas.push(objArmar);
                    break;
                case 2:
                    // Familia
                    objArmar = await Familia.TraerUno(Id);
                    nombreMenu += 'Familia  ';
                    MenuListaRamas = await objArmar.ListaRamas();
                    break;
                case 3:
                    // Perfil
                    objArmar = await Perfil.TraerUno(Id);
                    nombreMenu += 'Perfil  ';
                    MenuListaAccesos = await objArmar.ListaAccesos();
                    break;
                default:
                    throw 'Error en el Tipo d Menú a Armar';
            }
            nombreMenu += objArmar.Nombre;
            if (tipo <= 2) {
                // Rama o Familia
                if (MenuListaRamas.length > 0) {
                    for (let itemRama of MenuListaRamas) {
                        if (tipo > 1) {
                            str += '<ul><li class="UL_Seleccionable"><a href="#" class="btn btn-info btn-xs mibtn-seleccionMenuRama" data-Id="' + itemRama.IdEntidad + '">' + itemRama.Nombre + '</a></li>';
                        }
                        MenuListaFormularios = await itemRama.ListaFormularios();
                        for (let itemFormulario of MenuListaFormularios) {
                            str += '<div class="row">';
                            str += '    <div class="col-lg-6">';
                            str += '        <li class="LI_Seleccionable"><a href="#" class="btn btn-primary btn-xs mibtn-seleccionMenuFormulario" data-IdForm="' + itemFormulario.IdEntidad + '"><span class="textoMenu"> ' + itemFormulario.Nombre + '</span></a></li>';
                            str += '    </div>';
                            str += '    <div>';
                            str += await Acceso.ArmarStrAcciones(itemFormulario.AccionesPosibles);
                            str += '    </div>';
                            str += '</div>';
                        }
                        str += '</ul>';
                    }
                }
            } else {
                // Perfil
                let MenuIdFamilia = 0;
                let MenuIdRama = 0;
                let i = 0;
                while (i <= MenuListaAccesos.length) {
                    MenuIdFamilia = MenuListaAccesos[i].IdFamilia;
                    str += '<div class="row">';
                    str += '    <div class="col-lg-7">';
                    str += '        <ul><li class="UL_Seleccionable"><a href="#" class="btn btn-success btn-xs mibtn-seleccionMenuFamilia" data-IdFam="' + MenuListaAccesos[i].IdFamilia + '">' + (await MenuListaAccesos[i].ObjFamilia()).Nombre + '</a></li>';
                    str += '    </div>';
                    str += '</div>';
                    while ((i <= MenuListaAccesos.length) && (MenuListaAccesos[i].IdFamilia = MenuIdFamilia)) {
                        MenuIdRama = MenuListaAccesos[i].IdRama;
                        str += '<div class="row">';
                        str += '    <div class="col-lg-1"></div>';
                        str += '    <div class="col-lg-7">';
                        str += '        <ul><li class="UL_Seleccionable"><a href="#" class="btn btn-info btn-xs mibtn-seleccionMenuRama" data-IdRam="' + MenuListaAccesos[i].idRama + '">' + (await MenuListaAccesos[i].ObjRama()).Nombre + '</a></li>';
                        str += '    </div>';
                        str += '</div>';
                        while ((i <= MenuListaAccesos.length) && (MenuListaAccesos[i].IdFamilia = MenuIdFamilia) && (MenuListaAccesos[i].IdRama = MenuIdRama)) {
                            str += '<div class="row">';
                            str += '    <div class="col-lg-2"></div>';
                            str += '    <div class="col-lg-6">';
                            str += '        <li class="LI_Seleccionable"><a href="#" class="btn btn-primary btn-xs mibtn-seleccionMenuFormulario" data-IdForm="' + MenuListaAccesos[i].IdFormulario + '"><span class="textoMenu">' + (await MenuListaAccesos[i].ObjFormulario()).Nombre + '</span></a></li>';
                            str += '    </div>';
                            str += '    <div>';
                            str += await Acceso.ArmarStrAcciones(MenuListaAccesos[i].IdAccion);
                            str += '    </div>';
                            str += '</div>';
                            i++;
                        }
                    }
                }
            }
            spinnerClose();
        } catch (e) {
            spinnerClose();
        }
        if ($("#Modal-PopUpMenu").length == 0) {
            control += '<div id="Modal-PopUpMenu" class="modal fade" tabindex="-1" role="dialog" >';
            control += '    <div class="modal-dialog modal-lg">';
            control += '        <div class="modal-content">';
            control += '            <div class="modal-header HeaderPopUp">';
            control += '                <div class="row">';
            control += '                    <div class="col-lg-8 text-center"><h2 class="modal-title spanPopUpCabecera">' + nombreMenu + '</h2></div>';
            control += '                </div>';
            control += '            </div>';
            control += '            <div class="modal-body" style="overflow-y:scroll; height:410px;">';
            control += str;
            control += '            </div>';
            control += '            <div class="modal-footer">';
            control += '                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>';
            control += '            </div>';
            control += '        </div>';
            control += '    </div>';
            control += '</div>';
            $("body").append(control);
        }
        $('#Modal-PopUpMenu').modal({ show: true });
    }
}