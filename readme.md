# WCT_Server
> A back-end framework for web compareTool project.

## How to Run:
1. Install MongoDB
2. Create DB:   
webct   
|  
-----_users  
|  
-----_compareResults  
3. Restor library by nuget
4. Ctrl + F5 on VS

## Completed Prototype
### MongoDB DAO
1. [MongoDB Entity](https://www.cnblogs.com/huys03/p/3476594.html)
2. [DAO Design](https://github.com/xiong-ang/Node_DAO_Demo)

### JWT Token
1. [JWT](http://www.ruanyifeng.com/blog/2018/07/json_web_token-tutorial.html)
2. [JWT](https://www.cnblogs.com/java-jun-world2099/p/9146143.html)
3. [JWT by C#](https://www.cnblogs.com/ye-hcj/articles/8151385.html)

### [Web API Filter](https://www.cnblogs.com/UliiAn/p/5402146.html)
### Web API Cros
[.Net两种Cros配置方式](https://www.cnblogs.com/landeanfen/p/5177176.html)

### FileUpload by FormData
```javascript
//Front-end
var form = $('form')[0]; // You need to use standard javascript object here
var formData = new FormData(form); 
$.ajax({
    url: 'Your url here',
    data: formData,
    type: 'POST',
    contentType: false, // NEEDED, DON'T OMIT THIS (requires jQuery 1.6+)
    processData: false, // NEEDED, DON'T OMIT THIS
    // ... Other options like success and etc
}); 

// OR

var data = new FormData();
jQuery.each(jQuery('#file')[0].files, function(i, file) {
    data.append('file-'+i, file);
}); 
data.append('key1', 'value1');
data.append('key2', 'value2');

jQuery.ajax({
    url: 'php/upload.php',
    data: data,
    cache: false,
    contentType: false,
    processData: false,
    method: 'POST',
    type: 'POST', // For jQuery < 1.9
    success: function(data){
        alert(data);
    }
});
```

```C#
//Back-end
public bool Post()
{
    string value1 = HttpContext.Current.Request["key1"];
    string value2 = HttpContext.Current.Request["key2"];
    HttpFileCollection files = HttpContext.Current.Request.Files;

    foreach (string f in files.AllKeys)
    {
        HttpPostedFile file = files[f];
        if (string.IsNullOrEmpty(file.FileName) == false)
            file.SaveAs(HttpContext.Current.Server.MapPath("~/App_Data/") + file.FileName);
    }

    return true;
}
```