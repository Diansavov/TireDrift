document.addEventListener("DOMContentLoaded", function () {
    LoadTable();
});

function LoadTable() {
    DataTable.type('num', 'className', '');
    DataTable.type('date', 'className', '');

    table = $('#usersTable').DataTable({
        dom: 'tp',
        pageLength: 6,
        "oLanguage": {
            "sEmptyTable": "Няма персонал"
        },
        ajax: {
            url: '/Tires/GetAllHotelTires',
            dataSrc: '',
        },
        columns: [
            {
                data: 'tireImageUrl',
                render: function (data) {
                    return `<div style="width: 60px; height: 60px;">
                            <img style="width: 100%; height: 100%; object-fit: cover;" src="${data}"></img>
                        </div>`;
                }
            },
            { data: 'tireName' },
            { data: 'tireQuanity' },
            { data: 'userName' },
        ]
    });
}