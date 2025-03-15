function ShowSuccessAlret() {
    Swal.fire({
        title: "موفق",
        text: "عملیات با موفقیت انجام شد",
        icon: "success",
        confirmButtonText: "بستن",
    });
}
function ShowErrorAlret() {
    Swal.fire({
        title: "خطا",
        text: "عملیات با خطا مواجه شد ",
        icon: "error",
        confirmButtonText: "بستن",
    });
}