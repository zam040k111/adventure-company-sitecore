var gulp = require("gulp");
var _msbuild = require("msbuild");
var argv = require("yargs").argv;
var glob = require("glob");

function publishProject(src, publishProfile){	
	var msbuild = new _msbuild();
	
	msbuild.sourcePath = src;
	msbuild.version = $.config.msBuildVersion;
	msbuild.overrideParams.push("/p:PublishProfile=" + publishProfile);
	
	msbuild.publish();
};

async function publishSitecoreProject(){
	var src = argv.layer + "/" + argv.name + "/code/*.csproj";
	var publishProfile = (argv.publishProfile === undefined) ? "Default" : argv.publishProfile;
	
	var filePath = glob.sync(src)[0];
	if(filePath != "undefined"){
		publishProject(filePath, publishProfile);
	} else{
		console.log("Project does not exist!"); 
	}
};

async function publishSitecoreLayer(){
	var src = argv.layer + "/**/code/*.csproj";
	var publishProfile = (argv.publishProfile === undefined) ? "Default" : argv.publishProfile;
	
	var filePathes = glob.sync(src);
	if(filePathes.length > 0){
		filePathes.forEach(async function(filePath){
			publishProject(filePath, publishProfile);
		});
	} else{
		console.log("Layer does not exist or empty!");
	}
};

async function publishSitecoreSolution(){
	var src = "./**/**/code/*.csproj";
	var publishProfile = (argv.publishProfile === undefined) ? "Default" : argv.publishProfile;
		
	var filePathes = glob.sync(src);
	if(filePathes.length > 0){
		filePathes.forEach(async function(filePath){
			publishProject(filePath, publishProfile);
		});
	} else{
		console.log("Layer does not exist or empty!");
	}
};

gulp.task("publishSitecoreProject", publishSitecoreProject);
gulp.task("publishSitecoreSolution", publishSitecoreSolution);
gulp.task("publishSitecoreLayer", publishSitecoreLayer);