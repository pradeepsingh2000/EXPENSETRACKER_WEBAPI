

// dropdown
var button = document.getElementById("Cat");
button.onclick = function () {
    var select = document.getElementById("CategoryId");

    categoryid = select[select.selectedIndex].value; //value selected in select

    window.location.href = '@Url.Action("getexpenseByCategory","Category")?categoryid=' + categoryid;
}