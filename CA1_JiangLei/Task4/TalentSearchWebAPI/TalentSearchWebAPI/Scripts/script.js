$('#search').keyup(function () {
    //get data from json file
    var urlForJson = "Content/data.json";


    //get data from Restful web Service in development environment
    //var urlForJson = "http://localhost:9000/api/talents";

    //get data from Restful web Service in production environment
    //var urlForJson= "http://csc123.azurewebsites.net/api/talents";

    //Url for the Cloud image hosting
    //https://res.cloudinary.com/dwkibjxo5/image/upload/v1592819729/Jonathan_Ferrar_tn.jpg

    //jiang lei's cloudinary
    var urlForCloudImage = "https://res.cloudinary.com/dwkibjxo5/image/upload/v1592819729/";
    var checkByName = false;
    if ($('#byName').is(":checked")) {
        checkByName = true;
    }
    var checkByBio = false;
    if ($('#byBio').is(":checked")) {
        checkByBio = true;
    }

    var searchField = $('#search').val();
    var myExp = new RegExp(searchField, "i");

    $.ajax({
        type: "GET",
        url: `/API/Talents`,
        dataType: "json",
        success: function (data) {
            console.log(data);

            var output = '<ul class="searchresults">';
            if (checkByBio && checkByName) {
                $.each(data, function (key, val) {
                    //for debug
                    if ((val.Name.search(myExp) != -1) ||
                        (val.Bio.search(myExp) != -1)) {
                        output += '<li>';
                        output += '<h2>' + val.Name + '</h2>';
                        //get the absolute path for local image
                        //output += '<img src="images/'+ val.ShortName +'_tn.jpg" alt="'+ val.Name +'" />';

                        //get the image from cloud hosting
                        output += '<img src=' + urlForCloudImage + val.ShortName + "_tn.jpg alt=" + val.Name + '" />';
                        output += '<p>' + val.Bio + '</p>';
                        output += '</li>';
                    }
                });
            } else if (checkByBio) {
                $.each(data, function (key, val) {
                    //for debug
                    if (val.Bio.search(myExp) != -1) {
                        output += '<li>';
                        output += '<h2>' + val.Name + '</h2>';
                        //get the absolute path for local image
                        //output += '<img src="images/'+ val.ShortName +'_tn.jpg" alt="'+ val.Name +'" />';

                        //get the image from cloud hosting
                        output += '<img src=' + urlForCloudImage + val.ShortName + "_tn.jpg alt=" + val.Name + '" />';
                        output += '<p>' + val.Bio + '</p>';
                        output += '</li>';
                    }
                });
            } else if (checkByName) {
                $.each(data, function (key, val) {
                    //for debug
                    if (val.Name.search(myExp) != -1) {
                        output += '<li>';
                        output += '<h2>' + val.Name + '</h2>';
                        //get the absolute path for local image
                        //output += '<img src="images/'+ val.ShortName +'_tn.jpg" alt="'+ val.Name +'" />';

                        //get the image from cloud hosting
                        output += '<img src=' + urlForCloudImage + val.ShortName + "_tn.jpg alt=" + val.Name + '" />';
                        output += '<p>' + val.Bio + '</p>';
                        output += '</li>';
                    }
                });
            } else {

            }

            output += '</ul>';
            $('#update').html(output);
        }

    });

    $.getJSON(urlForJson, function (data) {
        console.log(data);
    }); //get JSON
});