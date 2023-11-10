$(document).ready(function () { loadDataTable(); });

function loadDataTable() {

    dataTable = $('#tableData').DataTable({
        "ajax": { url: '/admin/product/getall'},
        "columns": [
        { data: 'brand', "width": "15%" },
        { data: 'yearOfConstruction', "width": "15%" },
        { data: 'kilometresDriven', "width": "15%" },
        { data: 'powerInKilowatts', "width": "15%" },
        { data: 'price', "width": "15%" },
        { data: 'category.name', "width": "15%" },

    ]);

}