import processing.net.*; 
Client myClient; 
int clicks;
String data;

void setup() { 
  size(512, 512, P2D);
  // Connect to the local machine at port 10002.
  // This example will not run if you haven't
  // previously started a server on this port.
  myClient = new Client(this, "192.168.1.117", 73); 
  // Say hello
  myClient.write("Hi there".getBytes());
} 

void mouseReleased() {
  // Count the number of mouse clicks:
  clicks++;
  // Tell the server:
  String msg = "get frame";
  myClient.write(msg);
}

void draw() { 
  // Change the background if the mouse is pressed
  if (mousePressed) {
    background(255);
  } else {
    background(0);
  }
  
  if (myClient.available() > 0) {    // If there's incoming data from the client...
    data += myClient.readString();   // ...then grab it and print it 
    
  } else{
    PImage img = createImage(512, 512, RGB);
    int counter = 0;
    if(data != null){
      println(data);
      String[] colors  = split(data, "|");
      for(String c : colors){
        println("FF" + c);
        img.pixels[counter++] = unhex("FF" + c);
      }
      image(img, 0,0);
      data = null;
    }
    
    //println(data); 
  }

} 

void exit(){
  myClient.clear();
  println("stop");//do your thing on exit here
  super.exit();//let processing carry with it's regular exit routine
}