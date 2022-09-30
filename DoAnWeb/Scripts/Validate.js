function MinIsAnotherValue(fromId, thisId,cmd) {
    var fromValue = new Date(document.getElementById(fromId).value);
    var thisValue = new Date(document.getElementById(thisId).value);
    if (thisValue <= fromValue) {
        alert(cmd);
    }
}

function ThumnailFile() {
    var filename = document.getElementById("thumnail").value;
    var isRight = false;
    var allow_extensions = new Array("jpg", "png", "gif");
    var file_extension = filename.split('.').pop().toLowerCase();
    for (var i = 0; i < allow_extensions.length; i++) {
        if (file_extension == allow_extensions) {
            isRight = true;
        }
    }
    if (isRight == false) {
        alert("Không Đúng Định Dạng");
        filename = null;
    }
}

function IsEmptyInformationBook() {
    var title = document.getElementById("Title");
    if (title.value.replace(' ', '') == "") {
        alert("Chưa nhập tiêu đề");
        return false;
    }
    var createDate = document.getElementById("CreatedDate").value;
    if (createDate == "") {
        alert("Chưa nhập ngày xuất bản");
        return false;
    }
    var modifiedDate = document.getElementById("ModifiedDate");
    if (modifiedDate.value < createDate) {
        alert("Ngày tái bản phải sau ngày xuất bản");
        return false;
    }
    var price = document.getElementById("Price");
    if (price.value=="") {
        alert("Chưa nhập giá tiền");
        return false;
    }
    var quantity = document.getElementById("Quantity").value;
    if (quantity=="") {
        alert("Chưa nhập số lượng");
        return false;
    }
    return true;
}

function IsEmptyInformationCategory(id) {
    var cateName = document.getElementById(id);
    if (cateName.value.replace(' ', '') == "") {
        alert("Chưa nhập tên");
        return false;
    }
    return true;
}

