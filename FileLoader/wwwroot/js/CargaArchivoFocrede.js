
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
            const strMoneda = $("#txtFile").val().toLowerCase().substring(20, 24);

            switch (strMoneda) {
                case ' crc':
                    $("#monedaReporte").val(1);
                    break;
                case ' usd':
                    $("#monedaReporte").val(2);
                    break;
                case ' eur':
                    $("#monedaReporte").val(3);
                    break;
                case ' udes':
                    $("#monedaReporte").val(4);
                    break;
                default:
                    $("#monedaReporte").val(0);
                    break;
            };

        }

        /*Válida que el navegador permita HTML5*/
        if (typeof (FileReader) != "undefined") {
            $("#insertar_btn").prop("disabled", false);

        }
        else {
            //alert("¡Perdón! Intenta abrir está aplicación desde Google Chrome, Mozilla o Firefox");
            swal({
                title: "¡Perdón!",
                text: "Intenta abrir está aplicación desde Google Chrome, Mozilla o Firefox",
                icon: "error",
            });
        }
        $("#PopUp").hide();
    }
    else {
        $("#infoListo").css("color", "red");
        $("#PopUp").hide();
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

function ReadTxt(txtflag) {
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

function CrearObjeto(dataset, txtflag) {
    $("#PopUp").show();
    if (txtflag) {

        //Obtengo un array con las filas
        let lines = dataset.split('\n');
        //obtengo los encabezados
        let headings = lines[5].split('|');

        for (let h = 0; h < headings.length; h++) {
            $('<td class="text-center" ><strong> ' + headings[h] + '</strong></td> ').appendTo("#theadTxt");
        }

        const idReporte = $("#idReporte").val();

        const fechaReporteStr = headings[2].substring(7);
        const fechaReporteArray = fechaReporteStr.split('/');
        const dia = fechaReporteArray[0];
        const mes = fechaReporteArray[1];
        const anio = fechaReporteArray[2];
        const fechaReporte = anio + '-' + mes + '-' + dia;
        //console.log(fechaReporte);

        //Almacenar un array de objetos:
        let params1 = [];

        let moneda = $("#monedaReporte").val();


        //inserto cada fila dentro de el array de objetos:
        for (let i = 6; i < lines.length - 3; i++) {
            let rowData = [];
            let codigoCuenta;
            let nombreCuenta;
            let saldoHoy;
            let saldoAnterior;
            let variacion;

            // obtiene un array de cada valor en la fila (celdas)

            if (lines[i].length > 5) {
                rowData = lines[i].split('|');

                //console.log(rowData);
                // crea un objeto
                let obj = {};

                $('<tr> <td class="text-center">' + rowData[0] + '</td> <td">' + '</td> <td class="text-center">' + rowData[1] + '</td> <td class="text-center">' + rowData[2] + ' </td> '
                    + '<td class="text-center">' + rowData[3] + '</td > <td class="text-center">' + rowData[4] + '</td> </tr >').appendTo("#tBodyTxt");

                //Se hace esta validación para no enviar nulos a la base de datos
                if (rowData[0] == null || rowData[0] == '' || rowData[0] == 0 || rowData[0] == undefined) {
                    codigoCuenta = '--'
                } else {
                    codigoCuenta = rowData[0];
                }
                if (rowData[1] == null || rowData[1] == '' || rowData[1] == 0 || rowData[1] == undefined) {
                    nombreCuenta = '--'
                } else {
                    nombreCuenta = rowData[1];
                }

                if (rowData[2] == null || rowData[2] == '' || rowData[2] == 0 || rowData[2] == undefined) {
                    saldoHoy = 0
                } else {
                    saldoHoy = parseFloat(rowData[2]);
                }
                if (rowData[3] == null || rowData[3] == '' || rowData[3] == 0 || rowData[3] == undefined) {
                    saldoAnterior = 0
                } else {
                    saldoAnterior = parseFloat(rowData[3]);
                }
                if (rowData[4] == null || rowData[4] == '' || rowData[4] == 0 || rowData[4] == undefined) {
                    variacion = 0
                } else {
                    variacion = parseFloat(rowData[4]);
                }

                obj['Id_Moneda'] = moneda;
                obj['CodigoCuenta'] = codigoCuenta;
                obj['NombreCuenta'] = nombreCuenta;
                obj['SaldoAlDia'] = saldoHoy;
                obj['SaldoDiaAnterior'] = saldoAnterior;
                obj['Variacion'] = variacion;

                params1.push(obj);
            }

        }
        let listadoFocrede = params1;


        if (moneda == 0) {
            swal({
                title: "Atención!",
                text: "Primero debe seleccionar la moneda del reporte cargado.",
                icon: "warning",
                button: true,
                dangerMode: true,
            });

            LimpiarTablaReporte();
            $("#PopUp").hide();

            return false;
        }

        let params = {
            Indice: idReporte,
            CodigoReporte: 1, //FOCREDE
            Fecha_Reporte: fechaReporte,
            Active: true,
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
                    url: "/Cliente/CargaArchivos/Create",
                    type: "POST",
                    data: params,
                    datatype: "html",
                    success:
                        //si es correcto
                        function (data) { //inicio de no borrar

                            if (data == "Error500" || data == "Error401" || data == "Error404") {
                                //si hay algun error en la solicitud o red
                                $("#PopUp").hide();
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
                                window.location.href = '/Cliente/CargaArchivos/Create';
                                return false;

                            } else if (data.includes("Incorrecto")) {
                                //si hay algun error en la solicitud o red
                                $("#PopUp").hide();
                                swal({
                                    icon: 'warning',
                                    title: 'Algunos datos no cumplen con lo requerido',
                                    text: data,
                                    footer: "Revise los errores generados"
                                });
                                return false;
                            }
                            else if (data.includes("Cliente")) {
                                //si hay algun error en la solicitud o red
                                $("#PopUp").hide();
                                swal({
                                    icon: 'warning',
                                    title: 'Error en creación de Cliente nuevo',
                                    text: data,
                                    footer: "Revise los errores generados"
                                });
                                return false;
                            } else {
                                //si todo está correcto
                                //LimpiarDatosWizard(); //PROCESO LIMPIA COMPLETAMENTE EL REGISTRO
                                $("#PopUp").hide();
                                swal({
                                    title: 'Registro Exitoso',
                                    text: 'El reporte fue insertado en la BD. ID-' + data,
                                    icon: 'success',
                                    timer: 1500,
                                    buttons: false
                                });
                                $("#idReporte").val(data);
                                $("#ModalDatosReporteTxt").modal("show");
                            }
                        },
                    error: function () {
                        $("#PopUp").hide();
                        swal(
                            '¡Error en Registro! (⊙_◎)',
                            'La gestión no fue procesada correctamente',
                            'error'
                        );
                        window.location.href = '/Cliente/CargaArchivos/Create';
                        return false;
                    }
                });
            } else {
                $("#PopUp").hide();
                swal.close;
                window.location.href = '/Cliente/CargaArchivos/Create';
            }

        });

    }
}


function CerrarModalReporteTxt(isAdicion) {
    $("#ModalDatosReporteTxt").modal("hide");
    if (!isAdicion) {
        window.location.href = '/Cliente/CargaArchivos/Index';
    } else {
        LimpiarTablaReporte();
        $("#monedaReporte").val(0);
        $("#txtFile").val("");
    };
}

function LimpiarTablaReporte() {
    $("#theadTxt td").each(function () {
        $(this).remove(); //Se elimina el tr
    });

    $("#tBodyTxt tr").each(function () {
        $(this).remove(); //Se elimina el tr
    });
}
