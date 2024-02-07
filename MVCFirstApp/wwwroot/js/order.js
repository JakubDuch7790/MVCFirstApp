var dataTable;

$(document).ready(function () { loadDataTable(); });

function loadDataTable() {

    dataTable = $('#tableData').DataTable({
        "ajax": { url: '/Admin/Order/Getall' },
        "columns": [
            { data: 'id', "width": "15%" },
            { data: 'name', "width": "5%" },
            { data: 'phoneNumber', "width": "10%" },
            { data: 'applicationUser.email', "width": "15%" },
            { data: 'orderStatus', "width": "10%" },
            { data: 'orderTotal', "width": "5%" },

            {
                data: 'id',

                "render": function (data) {
                    return `<div class="w-auto btn-group" role="group">
                    <a href="/admin/product/upsert?id=${data}" class="btn btn-primary mx-auto">
                                    <i class="bi bi-pencil-square"> Edit</i>
                    </a>
                    </div>`
                },

                "width": "20%"
            },
        ]
        })
};
