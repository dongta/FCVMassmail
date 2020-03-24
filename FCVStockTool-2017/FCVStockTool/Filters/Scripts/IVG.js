function ConfirmDelete() {
    return window.confirm("Bạn có chắc chắn muốn xóa!");
}

function dateFormat(date) {
    var dateString = date.substr(6);
    var currentTime = new Date(parseInt(dateString));
    var month = currentTime.getMonth() + 1;
    var day = currentTime.getDate();
    var year = currentTime.getFullYear();
    return day + "/" + month + "/" + year;
}