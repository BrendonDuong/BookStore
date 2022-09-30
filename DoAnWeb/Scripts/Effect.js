function readURL() {
    var input = document.getElementById('thumnail');
    var isAccept = false;
    var fileType = input.files[0].type;
    var fileSize = input.files[0].size;
    if (fileType != 'image/jpeg' && fileType != 'image/png') {
        alert('Ảnh định dạng jpeg hoặc png');
        input.value = null;
        return;
    }
    if (fileSize >= 2000000) {
        alert('Dung lượng phải nhỏ hơn 1.5MB');
        input.value = null;
        return;
    }
    var reader = new FileReader();
    reader.onload = function () {
        var output = document.getElementById('canvasImg');
        output.src = reader.result;
        };
    reader.readAsDataURL(input.files[0]);
}

var index = 0;
var idImg;
function SlideImage(isBack, imgLenght) {
    if (isBack == 'next') {
        if (index != imgLenght - 1) {
            idImg = "#cf2 img." + index
            $(idImg).addClass('transparent');
            index++;
            idImg = "#cf2 img." + index
            $(idImg).removeClass('transparent');
        } else {
            idImg = "#cf2 img." + index
            $(idImg).addClass('transparent');
            index=0;
            idImg = "#cf2 img." + index
            $(idImg).removeClass('transparent');
        }
    }
    else {
        if (index != 0) {
            idImg = "#cf2 img." + index
            $(idImg).addClass('transparent');
            index--;
            idImg = "#cf2 img." + index
            $(idImg).removeClass('transparent');
        }
        else {
            idImg = "#cf2 img.0"
            $(idImg).addClass('transparent');
            index = imgLenght - 1;
            idImg = "#cf2 img." + index;
            $(idImg).removeClass('transparent');
        }
    }
   
}

