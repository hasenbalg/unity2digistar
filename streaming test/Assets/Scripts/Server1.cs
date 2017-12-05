using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class Server1 : MonoBehaviour {
    //https://www.youtube.com/watch?v=xgLRe7QV6QI


    Camera cam;
    Texture2D tex;
    public int imgWidth = 512;
    public int imgHeight = 512;
    RenderTexture rt;
    byte[] currentFrame;
    string ip; 

    private List<Socket> _clientSockets = new List<Socket>();
    private byte[] _buffer = new byte[1024];
    private Socket _serverSocket = new Socket(
        AddressFamily.InterNetwork,
        SocketType.Stream,
        ProtocolType.Tcp
        );

    

    // Use this for initialization
    void Start() {
        SetupServer();

        cam = GetComponent<Camera>();
        tex = new Texture2D(imgWidth, imgHeight);
        rt = new RenderTexture(imgWidth, imgWidth, 16);
    }

    private void Update()
    {
        // Render to RenderTexture
        cam.targetTexture = rt;
        cam.Render();
        // Read pixels to texture
        RenderTexture.active = rt;
        tex.ReadPixels(new Rect(0, 0, imgWidth, imgHeight), 0, 0);
        // Read texture to array
        currentFrame = tex.EncodeToJPG(); 
    }

   

    private void SetupServer() {
        ip = IPAddress.Any.ToString();
        Debug.Log("Setting up server on " + ip);
        _serverSocket.Bind(new IPEndPoint(IPAddress.Parse(ip), 73));
        _serverSocket.Listen(1);
        _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
    }

    private void AcceptCallback(IAsyncResult AR) {
        Socket socket = _serverSocket.EndAccept(AR);
        _clientSockets.Add(socket);
        Debug.Log("Client connected");
        socket.BeginReceive(
            _buffer,
            0,
            _buffer.Length,
            SocketFlags.None,
            new AsyncCallback(ReceiveCallback),
            socket
            );
        //accept more than one connection
        _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
    }

    private void ReceiveCallback(IAsyncResult AR)
    {
        Socket socket = (Socket)AR.AsyncState;

        int recieved = socket.EndReceive(AR);
        byte[] dataBuffer = new byte[recieved];
        Array.Copy(_buffer, dataBuffer, recieved);
        string text = Encoding.ASCII.GetString(dataBuffer);
        Debug.Log("Text recieved: " + text);

        // byte[] data = tex.EncodeToJPG();
        byte[] data = currentFrame;
         socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);

        socket.BeginReceive(
            _buffer,
            0,
            _buffer.Length,
            SocketFlags.None,
            new AsyncCallback(ReceiveCallback),
            socket
            );
    }

  

    private void SendCallback(IAsyncResult AR) {
        Socket socket = (Socket)AR.AsyncState;
        socket.EndSend(AR);
    }

    private void OnApplicationQuit()
    {
        foreach (var s in _clientSockets)
        {
            s.Close();
        }
        _serverSocket.Close();
    }
}
