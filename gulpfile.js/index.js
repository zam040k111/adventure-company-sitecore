var gulp = require("gulp");
var HubRegistry = require("gulp-hub");

global.$ = {
	config: require("./config/config.js")
};


/* Register all tasks */
var hub = new HubRegistry(["tasks/*.js"]);
gulp.registry(hub);

/* Create your tasks */
