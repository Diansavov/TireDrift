

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
            { data: 'userName' },
            { data: 'role' },
            { data: 'email' },
            { data: 'firstName' },
            { data: 'lastName' },
            { data: 'phoneNumber' },
        ]
    });
}