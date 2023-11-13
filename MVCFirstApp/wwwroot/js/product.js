$(document).ready(function () { loadDataTable(); });

function loadDataTable() {

    dataTable = $('#tableData').DataTable({
        "ajax": { url: '/Admin/Product/Getall' },
        "columns": [
            { data: 'brand', "width": "15%" },
            { data: 'price', "width": "15%" },
            { data: 'kilometresDriven', "width": "15%" },
            { data: 'powerInKilowatts', "width": "15%" },
            { data: 'yearOfConstruction', "width": "15%" },
            { data: 'category.name', "width": "15%" },

            {
                data: 'id',

                "render": function (data) {
                    return `<div class="w-auto btn-group" role="group">
                    <a href="" class="btn btn-primary mx-auto">
                                    <i class="bi bi-pencil-square"> Edit</i>
                                </a>
                    </div>`
                },

                "width": "15%"

            },
        ]
        })
};
