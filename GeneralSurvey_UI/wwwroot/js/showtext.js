

/********************** **********************提交表单，  答卷展示页* ********************** /* **********************/



/**
 * ************************************   上传文件
 */
// 获取文件的大小
var GetFileSize = function (fileid) {
    var fileSize = 0;
    fileSize = $("#" + fileid)[0].files[0].size;
    fileSize = fileSize / 1048576;
    return fileSize;
}

//根据上传路径获取文件的大小
var getNameFromPath = function (strFilepath) {
    var objRE = new RegExp(/([^\/\\]+)$/);
    var strName = objRE.exec(strFilepath);
    if (strName == null) {
        return null;
    }
    else {
        return strName[0];
    }
}
//判断文件扩展名是否正确
var exten = function (extension) {
    var fileTypeBool = true;
    switch (extension) {
        //Word
        case 'doc':
        case 'docx':
        case 'dotm':
        case 'docm':
        //Excel
        case 'xlsx':
        case 'xlsm':
        case 'xltx':
        case 'xltm':
        case 'xlsb':
        case 'xlam':
        //ppt
        case 'pptx':
        case 'ppt':
        case 'pptm':
        case 'ppsx':
        case 'ppsx':
        case 'potx':
        case 'potm':
        case 'ppam':
            fileTypeBool = true;
            break;
        default:
            fileTypeBool = false;
    }
    return fileTypeBool;

}





