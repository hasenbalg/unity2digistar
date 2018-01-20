var myFile = Ds.FileOpen("$Content\\user\\debug.txt", "w");
function debug(msg, ds){
	ds.FileWriteLine(myFile, msg + "\n");
}
function getFPS(){

}


var tmpPath;
var tmpPath1 = '$content\\User\\test1.jpg';
var tmpPath2 = '$content\\User\\test2.jpg';

var lastLoop = new Date();

var i = 0;
while(true){
	var thisLoop = new Date();
    var fps = 1000 / (thisLoop - lastLoop);
    lastLoop = thisLoop;
	debug(fps, Ds);

	if(i % 2 == 1){
		tmpPath = tmpPath1;
	}else{
		tmpPath = tmpPath2;
	}
    Ds.FileFromURL(tmpPath, 'http://127.0.0.1:73');
  	Ds.SetObjectArrayElem("img", "modelTexture", 0, tmpPath);
  i++;
}

Ds.FileClose(myFile);



