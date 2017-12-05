import processing.net.*; 
Client myClient; 
int clicks;
String data;

void setup() { 
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
  String msg = "get time";
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
    data = myClient.readString();   // ...then grab it and print it 
    println(data); 
  } 

} 

void exit(){
  myClient.clear();
  println("stop");//do your thing on exit here
  super.exit();//let processing carry with it's regular exit routine
}