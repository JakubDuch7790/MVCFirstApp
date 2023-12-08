var dataTable;

$(document).ready(function () { loadDataTable(); });

function loadDataTable() {

    dataTable = $('#tableData').DataTable({
        "ajax": { url: '/Admin/Company/Getall' },
        "columns": [
            { data: 'name', "width": "15%" },
            { data: 'streetAdress', "width": "5%" },
            { data: 'city', "width": "10%" },
            { data: 'country', "width": "15%" },
            { data: 'postalCode', "width": "10%" },
            { data: 'phoneNumber', "width": "5%" },

            {
                data: 'id',

                "render": function (data) {
                    return `<div class="w-auto btn-group" role="group">
                    <a href="/admin/company/upsert?id=${data}" class="btn btn-primary mx-auto">
                                    <i class="bi bi-pencil-square"> Edit</i>
                    </a>
                    <a Onclick=Delete('/admin/company/delete/${data}') class="btn btn-danger mx-auto">
                        <i class="bi bi-trash3-fill"> Delete</i>
                    </a>
                    </div>`
                },

                "width": "20%"
            },
        ]
        })
};

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
                $.ajax({
                    url: url,
                    type: 'DELETE',
                    success: function (data) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                })
        }
    });
}
