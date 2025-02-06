

document.addEventListener("DOMContentLoaded", function () {
    console.log('es')
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
            "sEmptyTable": "Няма непотвърдени потребители"
        },
        ajax: {
            url: '/Manager/GetEmployees',
            data: {
                firstName: firstName,
            },
            dataSrc: '',
        },
        "createdRow": function (row, data) {
            var isConfirmed = data.isConfirmed;

            if (isConfirmed == false) {
                $(row).addClass('red');
            }
        },
        columns: [
            { data: 'userName' },
            { data: 'roles' },
            { data: 'email' },
            { data: 'firstName' },
            { data: 'email' },
            { data: 'email' },

        ]
    });
}