$(function (){
    $('#SelectService').change(function () {
        $('#SelectDoctor').empty();

        $.ajax({
            url:'/Appointment/ShowJ_Doctor',
            dataType:'json',
            data: {id:$("#SelectService").val()},
            success: function(data) {
                $("#SelectDoctor").append("<option value='-1'> Select Doctor </option>")

                $.each(data,(index,deyer) => {
                    $('#SelectDoctor').append(`<option value="${deyer.value}">${deyer.text}</option>`)
                });
            }
        })
    })
})


