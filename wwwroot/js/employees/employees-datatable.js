

document.addEventListener("DOMContentLoaded", function () {
    LoadTable();
});

function searchSubmit() {
    table.destroy();
    LoadTable();
}

function LoadTable() {
    var firstName = $('input[name="firstName"]').val();

    DataTable.type('num', 'className', '');
    DataTable.type('date', 'className', '');

    table = $('#usersTable').DataTable({
        dom: 'tp',
        pageLength: 6,
        "oLanguage": {
            "sEmptyTable": "Няма персонал"
        },
        ajax: {
            url: '/Manager/GetEmployees',
            data: {
                firstName: firstName,
            },
            dataSrc: '',
        },
        columns: [
            { data: 'firstName' },
            { data: 'lastName' },
            { data: 'userName' },
            { data: 'role' },
            { data: 'email' },
            { data: 'phoneNumber' },
            {
                orderable: false,
                data: 'id',
                render: function (data) {
                    return `
                    <div class="d-flex justify-content-center gap-1">
                        <a class="btn btn-warning" href="/Manager/EditEmployee/${data}">Редактиране</a>
                        <a class="btn btn-danger" href="/Manager/DeleteEmployee/${data}">Изтриване</a>
                    </div>`;
                },
            },
        ]
    });
}