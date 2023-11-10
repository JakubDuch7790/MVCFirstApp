$(document).ready(function () { loadDataTable(); });



$('#myTable').DataTable({
    ajax: '/api/myData'
});

function loadDataTable() {

    dataTable = $('#tableData').DataTable({
        "ajax": { url: '/admin/product/getall'},
        "columns": [
        { data: 'name', "width": "15%" },
        { data: 'position', "width": "15%" },
        { data: 'salary', "width": "15%" },
        { data: 'office', "width": "15%" }
    ]);

}