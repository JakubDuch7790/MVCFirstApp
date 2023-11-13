$(document).ready(function () { loadDataTable(); });

function loadDataTable() {

    dataTable = $('#tableData').DataTable({
        "ajax": { url: '/Admin/Product/Getall' },
        "columns": [
            { data: 'brand', "width": "15%" },
            { data: 'price', "width": "10%" },
            { data: 'kilometresDriven', "width": "15%" },
            { data: 'powerInKilowatts', "width": "10%" },
            { data: 'yearOfConstruction', "width": "5%" },
            { data: 'category.name', "width": "10%" },

            {
                data: 'id',

                "render": function (data) {
                    return `<div class="w-auto btn-group" role="group">
                    <a href="/admin/product/upsert?id=${data}" class="btn btn-primary mx-auto">
                                    <i class="bi bi-pencil-square"> Edit</i>
                    </a>
                    <a class="btn btn-danger mx-auto">
                        <i class="bi bi-trash3-fill"> Delete</i>
                    </a>
                    </div>`
                },

                "width": "20%"
            },
        ]
        })
};
