var sock = Socket('127.0.0.1:73');
sock.openClientTCP();
print('socket open');
sock.write('give img');
for(;;) {
  sock.waitForData();

  var data = sock.read();
  if (data == 'exit'){
    break;
  }
  Ds.SetObjectArrayElem("img", "modelTexture", 0, data);
  sock.write('give img');
}

sock.close();
