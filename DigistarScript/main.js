//start me with: "js play main"

var boardHeight = 6.0;              // this is how tall (Z) I want the board to be as the game is played
var boardWidth = 10.0;              // this is how wide (X) I want the board to be as the game is played
var nominalBoardHeight = 10.0;      // this is how tall the image.x model is for the board (Z) before it is scaled
var nominalBoardWidth = 10.0;       // this is how wide the image.x model is for the board (X) before it is scaled
var imgPath = "$Content\\Library\\Scripts\\Vignettes\\Games\\JPong\\GameBoard.jpg";
var running = false;

//        System reset
Ds.ExecuteObjectCommand("system", "reset");
//        Wait a little while for the reset to work
Ds.Wait(1.0);

// Set the eye position so that it is looking at the board
Ds.SetObjectAttr("eye", "position", {'x': 0, 'y': -10, 'z': 0});
Ds.SetObjectAttr("eye", "attitude", {'h': 0, 'p': Radians(-25), 'r': 0});


// Create the game board

Ds.CreateObject("board", "solidModelClass");
Ds.SetObjectAttr("board", "model", "$Content\\Library\\Models\\Misc\\image.x");
Ds.SetObjectArrayElem("board", "modelTexture", 0, imgPath);
Ds.SetObjectAttr("board", "scale", {'x': boardWidth/nominalBoardWidth, 'y': 1, 'z': boardHeight/nominalBoardHeight});
Ds.SetObjectAttr("board", "position", {'x': 0, 'y': 0.01, 'z': 0});  // put the board back just a bit so the paddles are not coplanar with the board
Ds.SetObjectAttr("board", "intensity", 1);  // start with intensity 1 so it covers the stars and so that I can fade it up after the stars

Ds.SceneAddObject("board");

// This is the main loop. To get out of the loop the user has to stop the script by pressing the Fade/Stop/Reset button

while (running) {
  //reset the tex every frame
  Ds.SetObjectArrayElem("board", "modelTexture", 0, imgPath);

//fun goes here

  Ds.Wait(0.01, "system");   // wait until the next frame before going through the main loop again
}
