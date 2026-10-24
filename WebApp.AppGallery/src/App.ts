let _jsonData = [{ id: 0, name: '', title: '', link: '', img: '' }];

$(document).ready(function () {

    /*== GetAll Applications ==*/
    $.ajax({
        url: 'IISHandler1.ashx',
        type: "GET",
        cache: false,
        async: true,
        beforeSend: function () { $('#img-loading').removeAttr('hidden'); },
        complete: function () { $('#img-loading').attr('hidden', ''); },
        success: function (data) {
            _jsonData = data;
            drawGallery(data);

            checkCookie();
            $('#lblSignIn').hide();
        },
        error: function (xhr, ajaxOptions, error) {
            alert(xhr.status);
        }
    });

    /*== Add New Click Event ==*/
    $('#btn-add-new').click(function () {
        $('#txt-name').val('');
        $('#txt-title').val('');
        $('#txt-link').val('');
        $('#inputFileToLoad').val('');
        $('#textarea1').val('');
        $('#img-preview').attr('src', 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAYAAACqaXHeAAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwgAADsIBFShKgAAAABh0RVh0U29mdHdhcmUAcGFpbnQubmV0IDQuMC45bDN+TgAAA/BJREFUeF7tmd2LTVEYxmemmWGGmUy+IvmMJFFKkWuu1OSriBFJSj6upMyFjxskkUiUUrigKErKjeNSuXfvD/AvjOfZ7Wf1rtc6+5wxZ5h1zvzqp7Wf9e51znrXPnsudNVqtQkKuminXbd8wdyui386UTUiOVlFqv5f2UpCAzSYrg+aiUR79Q3oBLTXYr/2ogjaHO1TBx9tnrY72mdogAbt2IDUnpRR7Lu/6QZoPkctNse+R6KA1sPX5aRFmQ4+KqT18HU5aVEWGqBBqtii+Ry12Bz7XtZUAzSXqxabFw2wAU3ha3LTokwHHxXSFL4mNy3KQgM0SBULzeWqxebY9+w7IApoCl+TmxZlOviokHr8fI5alIUGaJAqJspz1mJz7LvxO0B5zlpsXjTABtTj53PUokwHHxVSj5/PUYuy0AANUsVEec5abI59z74DooB6/HyOWpTp4KNC6vHzOWpRFhqgQaqYKM9Zi82x78GWNwAMw02wx+UL4BybVQkWw3WJfCHs9XmVFptj3/1RQD1+vp6gH47D9/AJvGjmNsKfcNzekxIMwYfwLXwG95u5vfAX3G3vaaRFmQ4+KqQeP19PsB2+g4PldfEEgAH4Bd6AV9w92+DWcrwWboDc5G3YV+ZaZyn8Ch/A1jVAg1QxUd5IcATeh8fgUbiqzPfBy/AQjJ4AwCfjM1wBX8LV8Do8B0/AA3BRWXsacq1rcMoNoNh3dysbcBY+h/z9n4T8KfBxfg15emxA9ASU912A3yGb1AMfwUuQzbkDH8MlkOvMg1NqAFFeNMAWUo+fryc4DosTBt2Qjz1P8SPkhl7AT3CHu49Pzg94CvK+m3BPObcG8gm5Cl9BrsPrp3C9XadKDzMdfFPFzQh2wW+Qpz4G+RIbgctLz8BbcMjcw78YbMoWyN83n5RR+AbOhXwX3IN2nbvwMBywn1+lh1logAZVxc0I+uB5+AHysV3p5nfCUZcdhGPlmE8Cmzgf8sS5Dp+GYXcPf0qbbdZITzTXqgZI0Au7U3OTkeuk8r/RE83ZiyJI4Gty08NMB19ZKGxNjqYIDdCgXiGxi+VoijDX8Q1oVEhsTa56dPCVRcLW5KonNECDVJGwC+WqJ+Qd34CqImFrctWjg68sErYmVz2hARqkioRdKFc9IW+mAUQ35Kon5FVFFluXox4dfN2i1E0zBfudm9UTGqCBL0rdlBPauPSE3DbAFtpxOxL2q0EISnzeSu36/wN+rg4+fBH/hXw+050MrA8N0MAvpOt20RLl9Rrwr9HnT5eWKLcXRdDmaJ86+Gjz0+1MQN8lNEADTeRsM9h67PvP/xvsNMPAN6JTrouLqoL2vq5N/AY8c7CmWi2dogAAAABJRU5ErkJggg==');
        $('#btn-save').hide();
        $('#btn-add').show();
        $('#btn-remove').hide();
        $('#myModal').modal('show');
    });

    /*== Add new click event ==*/
    $(document).on('click', '#btn-add', function () {
        let base64 = getBase64Image(document.getElementById("img-preview"));

        let model: any = {};
        model.id = 0;
        model.name = $('#txt-name').val();
        model.title = $('#txt-title').val();
        model.link = $('#txt-link').val();
        model.img = base64;

        $.ajax({
            url: 'IISHandler1.ashx',
            type: "POST",
            cache: false,
            async: true,
            data: { model: JSON.stringify(model) },
            beforeSend: function () { $('.btn').attr('disabled', 'disabled'); },
            complete: function () { $('.btn').removeAttr('disabled'); },
            success: function (data) {
                window.location.reload();
            },
            error: function (xhr, ajaxOptions, error) {
                alert(xhr.status);
            }
        });
    });


    /*== Save Click Event ==*/
    $(document).on('click', '#btn-save', function () {
        let base64 = getBase64Image(document.getElementById("img-preview"));

        let textarea1 = document.getElementById('textarea1');
        textarea1.innerText = base64;

        let model: any = {};
        model.id = $('#' + this.id).attr('data-id');
        model.name = $('#txt-name').val();
        model.title = $('#txt-title').val();
        model.link = $('#txt-link').val();
        model.img = base64;


        $.ajax({
            url: 'IISHandler1.ashx',
            type: "PUT",
            cache: false,
            async: true,
            data: { model: JSON.stringify(model) },
            beforeSend: function () { $('.btn').attr('disabled', 'disabled'); },
            complete: function () { $('.btn').removeAttr('disabled'); },
            success: function (data) {
                if(data.success)
                    window.location.reload();
            },
            error: function (xhr, ajaxOptions, error) {
                alert(xhr.status);
            }
        });

    });

    /*== Remove Click Event ==*/
    $(document).on('click', '#btn-remove', function () {
        let id = parseInt($('#' + this.id).attr('data-id'));
        if (!confirm('Are you sure you want to remove this application?')) return;

        $.ajax({
            url: 'IISHandler1.ashx',
            type: "DELETE",
            cache: false,
            async: true,
            data: { id: id },
            beforeSend: function () { $('.btn').attr('disabled', 'disabled'); },
            complete: function () { $('.btn').removeAttr('disabled'); },
            success: function (data) {
                if (data.success)
                    window.location.reload();
            },
            error: function (xhr, ajaxOptions, error) {
                alert(xhr.status);
            }
        });
    });


    /*== SignIn click event ==*/
    $(document).on('click', '#btn-signin', function () {
        let username = $('#txt-username').val();
        let password = $('#txt-password').val();

        $.ajax({
            url: 'IISHandler2.ashx',
            type: "POST",
            cache: false,
            async: true,
            data: { username: username, password: password },
            beforeSend: function () { },
            complete: function () {  },
            success: function (data) {
                if (data.auth) {
                    setCookie('username', 'admin', 0);
                    $('#a-display-name').text('Welcome, ' + username.toUpperCase());
                    editMode();

                    $('#txt-username').val('');
                    $('#txt-password').val('');
                    $('#lblSignIn').hide();

                } else {
                    $('#lblSignIn').show();
                    //let's keep the popup window open
                    $('#a-display-name').attr('aria-expanded', 'true');
                    $('#a-display-name').parent('li').addClass('open');
                }
            },
            error: function (xhr, ajaxOptions, error) {
                alert(xhr.status);
            }
        });
                
    });
});

function drawGallery(data: any) {
    let divMain = document.getElementById('main-content');

    for (let r = 0; r < data.length; r++) {
        let div = document.createElement('div');
        div.className = "col-lg-2 col-md-4 col-xs-6 thumb";

        let a = document.createElement('a');
        a.className = "thumbnail";
        a.setAttribute("style", "padding:0px; width:155px;");
        a.href = data[r].link;

        let div1 = document.createElement('div');
        div1.className = "inner-wrap";

        let img = document.createElement('img');
        img.className = 'img-responsive tn';
        img.alt = '';
        img.src = data[r].img;
        img.width = 64;

        let div2 = document.createElement('div');
        div2.className = 'footer-text';
        div2.setAttribute('title', data[r].title);
        div2.innerHTML = data[r].name.substring(0, 18);

        let span = document.createElement('span');
        span.className = 'pull-right glyphicon glyphicon-edit editIcon';
        span.setAttribute('style', 'display:none;');
        //span.setAttribute('style', 'margin:4px 14px 0px 0px; cursor:pointer; color:lightgray;');
        span.setAttribute('data-id', data[r].id.toString());
        span.onclick = function () {
            let span = this as HTMLSpanElement;
            let id = parseInt(span.getAttribute('data-id'));
            $('#btn-save').attr('data-id', id.toString());
            $('#btn-remove').attr('data-id', id.toString());
                    
            /*== Get Application by Id ==*/
            $.ajax({
                url: 'IISHandler1.ashx?id=' + id,
                type: "GET",
                cache: false,
                async: true,
                //data: {id: id },
                beforeSend: function () { },
                complete: function () { },
                success: function (data) {
                    //console.log(data);
                    if (data.length > 0) {
                        $('#txt-name').val(data[0].name);
                        $('#txt-title').val(data[0].title);
                        $('#txt-link').val(data[0].link);
                        $('#img-preview').attr('src', data[0].img);

                        $('#btn-save').show();
                        $('#btn-remove').show();
                        $('#btn-add').hide();
                        $('#myModal').modal('show');
                    } else {
                        $('#ValidationSummary1').text('No applications was found.').show();
                    }
                    
                },
                error: function (xhr, ajaxOptions, error) {
                    alert(xhr.status);
                }
            });
        };

        div1.appendChild(img);
        a.appendChild(div1);
        a.appendChild(div2);

        div.appendChild(span);
        div.appendChild(a);
        divMain.appendChild(div);
    }
}

function getBase64Image(img) {
    let canvas = document.createElement("canvas");
    canvas.width = img.width;
    canvas.height = img.height;
    let ctx = canvas.getContext("2d");
    ctx.drawImage(img, 0, 0);
    let dataURL = canvas.toDataURL("image/png");
    return dataURL; //.replace(/^data:image\/(png|jpg);base64,/, "");
}

function encodeImageFileAsURL() {
    let filesSelected = document.getElementById("inputFileToLoad") as HTMLInputElement;

    if (filesSelected.files.length > 0) {
        let fileToLoad = filesSelected.files[0];
        let fileReader = new FileReader();

        fileReader.onload = function (fileLoadedEvent:any) {
            let srcData = fileLoadedEvent.target.result; // <--- data: base64

            let imgPreview = document.getElementById('img-preview') as HTMLImageElement;
            imgPreview.src = srcData;

            let textarea1 = document.getElementById('textarea1');
            textarea1.innerText = srcData;;
        }
        fileReader.readAsDataURL(fileToLoad);
    }
}

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    //d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    d.setTime(d.getTime() + (5 * 60 * 1000)); // set to 5 mins
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function checkCookie() {
    var username = getCookie("username");
    if (username != "") {
        $('#a-display-name').text('Welcome, ' + username.toUpperCase());
        editMode();
    } else {
        //username = prompt("Please enter your name:", "");
        //if (username != "" && username != null) {
        //    //setCookie("username", username, 365);
        //}
    }
}

function editMode()
{
    $('.editIcon').each(function () {
        $(this).show();
    });
    $('#btn-add-new').show();
}

//interface FileReaderEventTarget extends EventTarget {
//    result: string
//}

//interface FileReaderEvent extends Event {
//    target: FileReaderEventTarget;
//    getMessage(): string;
//}