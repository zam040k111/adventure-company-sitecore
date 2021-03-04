var gulp = require("gulp");
var exec = require("child_process").exec;

function generateModels(cb) {
    exec('.\\..\\tools\\Leprechaun\\Leprechaun.console.exe /c .\\..\\..\\src\\Leprechaun.config',
        function (err, stdout, stderr) {
            console.log(stdout);
            console.log(stderr);
            cb(err);
        });
};

gulp.task("generateModels", generateModels);