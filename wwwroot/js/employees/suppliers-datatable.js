

document.addEventListener("DOMContentLoaded", function () {
    LoadTable();
});

function searchSubmit() {
    table.destroy();
    LoadTable();
}

function LoadTable() {
    var name = $('input[name="name"]').val();

    DataTable.type('num', 'className', '');
    DataTable.type('date', 'className', '');

    table = $('#usersTable').DataTable({
        dom: 'tp',
        pageLength: 6,
        "oLanguage": {
            "sEmptyTable": "Няма доставчици"
        },
        ajax: {
            url: '/Employees/GetSuppliers',
            data: {
                name: name,
            },
            dataSrc: '',
        },
        columns: [
            { data: 'name' },
            { data: 'phoneNumber' },
            {
                orderable: false,
                data: 'id',
                render: function (data) {
                    return `
                    <div class="d-flex justify-content-center gap-1">
                        <a class="btn btn-warning" href="/Employees/EditSupplier/${data}">Edit</a>
                        <a class="btn btn-danger" href="/Employees/DeleteSupplier/${data}">Delete</a>
                    </div>`;
                },
            },
        ]
    });
}