
<script>
    /*Función permite exportar la tabla  para poder crear el archivo*/
    function ExportToTable() {
        $("#PopUp").show();
    const delayInMilliseconds = 1000; //1 second

    setTimeout(function () {
        $("#PopUp").hide();
        }, delayInMilliseconds);
    const regex = /^([a-zA-Z0-9\s_\\.\-:])+(.txt)$/;



    /*Valida un archivo txt válido*/
    if (regex.test($("#txtFile").val().toLowerCase())) {
        let txtflag = false; /*Bandera de error en caso de que no sea el formato correcto .txt*/
            if ($("#txtFile").val().toLowerCase().indexOf(".txt") > 0) {
        txtflag = true;
            }

    /*Válida que el navegador permita HTML5*/
    if (typeof (FileReader) != "undefined") {
        $("#insertar_btn").prop("disabled", false);
                //let reader = new FileReader();
                //reader.onload = function (e) {
        //    let dataset = e.target.result;                        
        //    /*Convierte los datos del txt en un objeto*/                    
        //    CrearObjeto(dataset, txtflag);
        //}

        //if (txtflag) {/*Si el excel cargado es .xlsx crea un array*/
        //        reader.readAsText($("#txtFile")[0].files[0]);
        //}
    }
    else {
        //alert("¡Perdón! Intenta abrir está aplicación desde Google Chrome, Mozilla o Firefox");
        swal({
            title: "¡Perdón!",
            text: "Intenta abrir está aplicación desde Google Chrome, Mozilla o Firefox",
            icon: "error",
        });
            }
        }
    else {
        $("#infoListo").css("color", "red");
    //alert("Debes cargar un archivo Excel correcto!");
    swal({
        title: "¡Cuidado!",
    text: "Debes cargar un archivo Txt correcto!",
    icon: "error",
    showCancelButton: false,
    confirmButtonColor: '#3085d6',
    cancelButtonColor: '#d33',
    confirmButtonText: 'Entendido'
            }).then(() => {
        location.reload();
            });
        }       



    }

    function ReadTxt(txtflag){
        let reader = new FileReader();
    reader.onload = function (e) {
        let dataset = e.target.result;
    /*Convierte los datos del txt en un objeto*/
    CrearObjeto(dataset, txtflag);
            }

    if (txtflag) {/*Si el excel cargado es .xlsx crea un array*/
        reader.readAsText($("#txtFile")[0].files[0]);
            }
    }

    function CrearObjeto(dataset, txtflag){
            if (txtflag) {

        //Obtengo un array con las filas
        let lines = dataset.split('\n');
    //obtengo los encabezados
    let headings = lines[5].split('|');

    for (let h = 0; h < headings.length; h++){
        $('<td class="text-center" ><strong> ' + headings[h] + '</strong></td> ').appendTo("#theadTxt");
                }

    //Almacenar un array de objetos:
    let params1 = [];



    //inserto cada fila dentro de el array de objetos:
    for (let i = 6; i < lines.length - 3; i++) {
        let rowData = [];


                    // obtiene un array de cada valor en la fila (celdas)

                    if (lines[i].length > 5) {
        rowData = lines[i].split('|');

    //console.log(rowData);
    // crea un objeto
    let obj = { };

    $('<tr> <td class="text-center">' + rowData[0] + '</td> <td">' + '</td> <td class="text-center">' + rowData[1] + '</td> <td class="text-center">' + rowData[2] + ' </td> '
    + '<td class="text-center">' + rowData[3] + '</td > <td class="text-center">' + rowData[4] + '</td> </tr >').appendTo("#tBodyTxt");

obj['CodigoCuenta'] = rowData[0];
obj['NombreCuenta'] = rowData[1];
obj['SaldoAlDia'] = rowData[2];
obj['SaldoDiaAnterior'] = rowData[3];
obj['Variacion'] = rowData[4];

params1.push(obj);
                    }
                            
                }
let listadoFocrede = params1;
let moneda = $("#monedaReporte").val();

if (moneda == 0) {
    swal({
        title: "Atención!",
        text: "Primero debe seleccionar la moneda del reporte cargado.",
        icon: "warning",
        button: true,
        dangerMode: true,
    });

    LimpiarTablaReporte();


    return false;
}

let params = {
    Moneda: moneda,
    listadoFocrede

}

//console.log(params);



swal({
    title: 'Consulta',
    text: "¿Está seguro de realizar la carga?",
    icon: 'info',
    buttons: {
        cancel: true,
        confirm: true,
    }
}).then((result) => {
    if (result) {
        $.ajax({
            url: "/CargaArchivos/Create",
            type: "POST",
            data: params,
            datatype: "html",
            success:
                //si es correcto
                function (data) { //inicio de no borrar

                    if (data == "Error500" || data == "Error401" || data == "Error404") {
                        //si hay algun error en la solicitud o red
                        swal({
                            icon: 'error',
                            title: 'Error Transacción Cod.502',
                            text: 'La Solicitud no pudo ser procesada!',
                            footer: 'Verifique que se encuentra en Red <br>y que los datos estén correctos'
                        });

                        if (data == "Error500") {
                            setTimeout(GoToError("Error500"), 3000);
                        }

                        if (data == "Error401") {
                            setTimeout(GoToError("Error401"), 3000);
                        }

                        if (data == "Error404") {
                            setTimeout(GoToError("Error404"), 3000);
                        }

                        HidePopup();
                        return false;

                    } else if (data.includes("Incorrecto")) {
                        //si hay algun error en la solicitud o red
                        swal({
                            icon: 'warning',
                            title: 'Algunos datos no cumplen con lo requerido',
                            text: data,
                            footer: "Revise los errores generados"
                        });
                        HidePopup();
                        return false;
                    }
                    else if (data.includes("Cliente")) {
                        //si hay algun error en la solicitud o red
                        swal({
                            icon: 'warning',
                            title: 'Error en creación de Cliente nuevo',
                            text: data,
                            footer: "Revise los errores generados"
                        });
                        HidePopup();
                        return false;
                    } else {
                        //si todo está correcto
                        //LimpiarDatosWizard(); //PROCESO LIMPIA COMPLETAMENTE EL REGISTRO
                        swal({
                            title: 'Registro Exitoso',
                            text: 'El reporte fue insertado en la BD',
                            icon: 'success',
                            timer: 1500,
                            buttons: false
                        });
                        $("#ModalDatosReporteTxt").modal("show");
                    }
                },
            error: function () {

                swal(
                    '¡Error en Registro! (⊙_◎)',
                    'La gestión no fue procesada correctamente',
                    'error'
                );
                return false;
            }
        });
    } else {
        swal.close;
        window.location.href = '/CargaArchivos/Index';
    }

});


              

                               
            }
    }

function CerrarModalReporteTxt() {
    $("#ModalDatosReporteTxt").modal("hide");
    window.location.href = '/CargaArchivos/Index';
}

function LimpiarTablaReporte() {
    $("#theadTxt td").each(function () {
        $(this).remove(); //Se elimina el tr
    });

    $("#tBodyTxt tr").each(function () {
        $(this).remove(); //Se elimina el tr
    });
}

</script >