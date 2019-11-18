

$('#card-toast').on('show.bs.toast', () => {
    setTimeout(() => {
        $("#card-toast").attr("class") = "block";
    }, 2000);
});


