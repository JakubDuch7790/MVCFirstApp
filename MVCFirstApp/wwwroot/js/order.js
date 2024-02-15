var dataTable;

$(document).ready(function () {

    var url = new URL(window.location.href);
    var status = url.searchParams.get("status");

    //if (status === "inprocess" || status === "completed" || status === "pending" || status === "approved") {
    //    loadDataTable(status);
    //} else {
    //    loadDataTable("all");
    //}
    if (status == "pending") {
        loadDataTable("pending");
    }
    else {
        loadDataTable("all");
    }
    
});

function loadDataTable(status) {

    dataTable = $('#tableData').DataTable({
        "ajax": { url: '/Admin/Order/Getall?status=' + status },
        "columns": [
            { data: 'id', "width": "10%" },
            { data: 'name', "width": "5%" },
            { data: 'phoneNumber', "width": "10%" },
            { data: 'applicationUser.email', "width": "15%" },
            { data: 'orderStatus', "width": "10%" },
            { data: 'orderTotal', "width": "5%" },

            {
                data: 'id',

                "render": function (data) {
                    return `<div class="w-auto btn-group" role="group">
                    <a href="/admin/order/details?orderId=${data}" class="btn btn-primary mx-auto">
                                    <i class="bi bi-pencil-square"></i>
                    </a>
                    </div>`
                },

                "width": "15%"
            },
        ]
        })
};
