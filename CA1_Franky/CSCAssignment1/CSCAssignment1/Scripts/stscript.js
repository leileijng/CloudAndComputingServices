$('#search').keyup(function () {
    //get data from json file
    //var urlForJson = "data.json";


    //get data from Restful web Service in development environment
    var urlForJson = "https://localhost:44357/api/talents";

    //get data from Restful web Service in production environment
    //var urlForJson= "http://csc123.azurewebsites.net/api/talents";

    //Url for the Cloud image hosting
    var urlForCloudImage = "https://res.cloudinary.com/doh5kivfn/image/upload/v1460006156/talents/";

    var searchField = $('#search').val();
    var myExp = new RegExp(searchField, "i");
    $.getJSON(urlForJson, function (data) {
        var output = '<ul class="searchresults">';
        $.each(data, function (key, val) {
            //for debug
            console.log(data);
            if ((val.Name.search(myExp) != -1) ||
                (val.Bio.search(myExp) != -1)) {
                output += '<li>';
                output += '<h2>' + val.Name + '</h2>';
                //get the absolute path for local image
                //output += '<img src="images/'+ val.ShortName +'_tn.jpg" alt="'+ val.Name +'" />';

                //get the image from cloud hosting
                output += '<img src=' + urlForCloudImage + val.ShortName + "_tn.jpg alt=" + val.Name + '" />';
                output += '<p>' + val.Bio + '</p>';
                output += `<button onclick="upload('${urlForCloudImage + val.ShortName}_tn.jpg', '${val.ShortName}_tn.jpg');">Upload to AWS S3</button>`;
                output += '</li>';
            }
        });
        output += '</ul>';
        $('#update').html(output);
    }); //get JSON
});
var bucketName = "*";
var bucketRegion = "*";
var IdentityPoolId = "*";

AWS.config.update({
    region: bucketRegion,
    credentials: new AWS.CognitoIdentityCredentials({
        IdentityPoolId: IdentityPoolId
    })
});

var s3 = new AWS.S3({
    apiVersion: '2006-03-01',
    params: { Bucket: bucketName }
});
function upload(url, fileName) {
    var filePath = 'my-first-bucket-path/' + fileName;
    s3.upload({
        Key: filePath,
        Body: url,
        ACL: 'public-read'
    }, function (err, data) {
        if (err) {
            reject('error');
        }
        alert('Successfully Uploaded!');
        console.log(data.Location);
        shortenUrl(data.Location);
    }).on('httpUploadProgress', function (progress) {
        var uploaded = parseInt((progress.loaded * 100) / progress.total);
        $("progress").attr('value', uploaded);
    });
}
function shortenUrl(url) {
    var accessToken = "*";
    var params = {
        "long_url": url
    };
    $.ajax({
        url: "https://api-ssl.bitly.com/v4/shorten",
        cache: false,
        dataType: "json",
        method: "POST",
        contentType: "application/json",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", "Bearer " + accessToken);
        },
        data: JSON.stringify(params)
    }).done(function (data) {
        document.getElementById('snackbar').style.visibility = "hidden";
        $('#displayURL').text(`Your Bitly URL is`);
        $('#newlink').text(`${data.link}`);
        $('#newlink').attr('href', `${data.link}`);
    }).fail(function (data) {
        document.getElementById('snackbar').style.visibility = "visible";
        console.log(data);
        setTimeout(function () {
            shortenUrl(url);
        }, 1000);
        alert("Fail to retrieve data");
    });
}