$(document).ready(function () {
    /* alert('Hellow');*/
    GetAlldata();
});
    function GetAlldata() {
        debugger;
        $.ajax({
            url: '/Stored/GetAlldata',
            type: 'Get',
            dataType: 'json',
            success: OnSuccess
            /*error: function () { alert("error"); }*/


        })


    }
    function OnSuccess(response) {
        debugger;
        $('#mydatatable').DataTable({
            bProcessing: true,
            bLengthChange: true,
            lengthMenu: [[5, 10, 25, -1], [5, 10, 25, "All"]],
            bfilter: true,
            bSort: true,
            bPaginate: true,
            data: response,
            columns: [
                {
                    data: 'Id',
                    render: function (data, type, row, meta) {
                        return row.id
                    }
                },
                {
                    data: 'Name',
                    render: function (data, type, row, meta) {
                        return row.name
                    }
                },
                {
                    data: 'Gender',
                    render: function (data, type, row, meta) {
                        return row.gender
                    }
                },
                {
                    data: 'City',
                    render: function (data, type, row, meta) {
                        return row.city
                    }
                },
                {
                    data: 'Pincode',
                    render: function (data, type, row, meta) {
                        return row.pincode
                    }
                },
                {
                    "title": "",
                    "data": "id",
                    "searchable": false,
                    "sortable": false,
                    "render": function (data, type, full, meta) {
                        var buffer = '<a href="/Stored/Edit/' + data + '" class="btn btn-sm btn-primary js-action"><i class="far fa-edit"></i></a>&nbsp;'

                        buffer += '<a href="/Stored/Delete/' + data + '" class="btn btn-sm btn-danger js-action"onclick="return confirm(`Are you sure you want to delete ?`)"><i class="far fa-trash-alt"></i></a>';

                        return buffer;
                    }
                }

            ]
        })

    }



    //$('#mydatatable').DataTable({

    //})

