

String url = "http://192.168.0.24:73";

void setup() {
  size(1000, 1000);
  textSize(32);
}

void draw() {
  PImage img = loadImage(url, "jpg");
  if (img != null){
    image(img, 0, 0);
  }
  text(frameRate, 10, 30);
}