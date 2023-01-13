//delete function

function Delete(id) {
    alertify.confirm('Budget ', 'Are you Sure to delete ', function () {
        window.location.href = '@Url.Action("Delete","Expense")/'+ id;
    }, null);

}

//
