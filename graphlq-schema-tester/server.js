var http = require('http');
var fs = require('fs');

http.createServer(function (req, res) {
    //console.log(req.method);
    //console.log(req.url);
    //console.log(req.headers);
    //console.log(req.rawHeaders);
    //TODO: remove query parameters ?para=value

    let url = "." + req.url;
    let ext = "";
    if (req.url === "/") {
        url = "./index.html";
        ext = "html";
    }
    if (req.url === "/graphql") {
        url = "./schema.json";
        ext = "json";
    }
    else if (req.url.lastIndexOf(".") > -1) {
        let s = req.url.split(".");
        if (s.length > 0 ) {
            ext = s[s.length - 1];
        }
    }

    switch (ext) {
        case "css":
            fs.readFile(url, function(err, data) {
                res.writeHead(200, {'Content-Type': 'text/css'});
                res.write(data);
                return res.end();
              });
            break;
        case "js":
            fs.readFile(url, function(err, data) {
                res.writeHead(200, {'Content-Type': 'application/javascript'});
                res.write(data);
                return res.end();
                });
            break;
        case "png":
            fs.readFile(url, function(err, data) {
                res.writeHead(200, {'Content-Type': 'image/png'});
                res.write(data);
                return res.end();
                });
            break;               
        case "html":
            fs.readFile(url, function(err, data) {
                res.writeHead(200, {'Content-Type': 'text/html'});
                res.write(data);
                return res.end();
              });
            break;
        case "json":
            fs.readFile(url, function(err, data) {
                res.writeHead(200, {'Content-Type': 'application/json'});
                res.write(data);
                return res.end();
                });
            break;
    }
}).listen(8080);