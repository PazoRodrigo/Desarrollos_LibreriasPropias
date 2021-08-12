function ejecutar(url, params) {
    var ret = undefined;
    var mess = "";
    $.ajax({
        url: url,
        dataType: "json",
        type: "POST",
        data: JSON.stringify(params),
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var wsTranfer = data.d;
            if (wsTranfer.todoOk == true) {
                ret = wsTranfer.data;
            }
            else {
                mess = wsTranfer.mensaje;
            }
        },
        error: function (xhr, textStatus, error) {
            console.log(xhr);
            console.log(textStatus);
            console.log(error);
            mess = "Se ha producido un error!!!";
        }
    });
    if (mess != "") {
        alert(mess);
    }
    else {
        return ret;
    }
};
async function ejecutarAsync(url, params) {
    var data = await ejecutarAjax(url, params);
    wsTranfer = data.d;
    if (wsTranfer.todoOk == true) {
        return wsTranfer.data;
    }
    else {
        throw (wsTranfer.mensaje);
    }
}
async function ejecutarAjax(url, params) {
    try {
        return await $.ajax({
            url: url,
            dataType: "json",
            type: "POST",
            data: JSON.stringify(params),
            contentType: "application/json; charset=utf-8",
            error: function (xhr, textStatus, error) {
                console.log(url);
                console.log(params);
                console.log(xhr);
                console.log(textStatus);
                //alertAlerta(xhr.responseJSON.Message);
            }

        });
    } catch (error) {
        console.error(error);
        $.unblockUI();
        throw "Se ha producido un error!!!";
    }
}
function ejecutarProm(url, params) {
    var ret = undefined;
    var mess = "";
    return $.ajax({
        url: url,
        dataType: "json",
        type: "POST",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        error: function (xhr, textStatus, error) {
            mess = "Se ha producido un error!!!";
        }
    });
}