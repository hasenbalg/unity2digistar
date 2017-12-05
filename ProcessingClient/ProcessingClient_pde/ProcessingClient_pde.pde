import processing.net.*;
import javax.imageio.ImageIO;
import java.io.ByteArrayInputStream;
import java.awt.image.BufferedImage;
Client c;
byte[] data;
PImage img, oldimg;
void setup() {
  size(512, 512, P2D);
  c = new Client(this, "127.0.01", 73);
  c.write("get frame");
  oldimg = new PImage(width, height,PConstants.ARGB);
}


void draw() {

  if (c.available() > 0) {
    data = c.readBytes();
   // println(data);

    try {
      BufferedImage bimg = ImageIO.read(new ByteArrayInputStream(data));
      img=new PImage(width, height,PConstants.ARGB);
      bimg.getRGB(0, 0, img.width, img.height, img.pixels, 0, img.width);
      img.updatePixels();


      image(img, 0, 0);
      oldimg = img;
    }
    catch(Exception e) {
      System.err.println("Can't create image from buffer");
      e.printStackTrace();
    }
    
    
    c.write("get frame");
  }
}



 PImage ByteArrayToBitmap(byte[] bytesIn)     // byteIn the input byte array. Picture size should be known
        {
            PImage picOut=new PImage(width,height,RGB);  //define the output picture
            int i= 0;
            for(int y = 0;y < width * height; y+=3){
                  color c = 
                  picOut.pixels[i] = color(bytesIn[i], bytesIn[i + 1],bytesIn[i + 2]);
                
              
            }
            return picOut;      //  e finita la commedia
        }