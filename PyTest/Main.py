import socket
from PIL import Image
import io


import mttkinter as tk

client_socket = None
ip_address = 'localhost'
port = 73
data = None
buffer_size = 1024
connected = False

def main():
    try_2_connect()
    loop()
    client_socket.close()


def try_2_connect():
    global client_socket
    global connected
    client_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

    while not connected:
        try:
            client_socket.connect((ip_address, port))
            print("connected to ", ip_address, port )
            connected = True
        except:
            print("Connecting to ", ip_address, port )

def loop():
    global client_socket
    global data

    client_socket.send("get frame".encode())

    result = client_socket.recv(buffer_size)

    data = result
    while len(result) == buffer_size:
        result = client_socket.recv(buffer_size)

        data += result
        print(len(result))

    print("\n\n-----------------------------------------------\n\n")
    print(data)

    image = Image.open(io.BytesIO(data))
    print(image.size)

    # create the canvas, size in pixels
    canvas = tk.Canvas(width=512, height=512, bg='black')

    # pack the canvas into a frame/form
    canvas.pack(expand=tk.YES, fill=tk.BOTH)

    # load the .gif image file
    gif1 = tk.PhotoImage(image)

    # put gif image on canvas
    # pic's upper left corner (NW) on the canvas is at x=50 y=10
    canvas.create_image(0, 0, image=gif1, anchor=tk.NW)

if __name__ == '__main__':
    main()