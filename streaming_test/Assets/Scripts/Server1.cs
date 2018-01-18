



using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class Server1 : MonoBehaviour {
    //https://www.youtube.com/watch?v=xgLRe7QV6QI



    public int port = 73;
    string ip;
    byte[] frameBuffer;


    private List<Socket> _clientSockets = new List<Socket>();
    private byte[] _buffer = new byte[1024];
    private Socket _serverSocket = new Socket(
        AddressFamily.InterNetwork,
        SocketType.Stream,
        ProtocolType.Tcp
    );

    // Use this for initialization
    void Start()
    {
        Application.runInBackground = true;
        SetupServer();
    }

    // Update is called once per frame
    void OnPostRender()
    {
            int width = Screen.width;
            int height = Screen.height;
            Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
            tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            tex.Apply();
            frameBuffer = tex.EncodeToJPG();
            Destroy(tex);
    }
   
    private void SetupServer()
    {
        ip = IPAddress.Any.ToString();
        Debug.Log("Setting up server on " + ip);
        _serverSocket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
        _serverSocket.Listen(1);
        _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
    }

   

    private void AcceptCallback(IAsyncResult AR)
    {
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

        //int recieved = socket.EndReceive(AR);
        //byte[] dataBuffer = new byte[recieved];
        //Array.Copy(_buffer, dataBuffer, recieved);
        //string text = Encoding.ASCII.GetString(dataBuffer);
        //Debug.Log("Text recieved: " + text);
        

        string header = @"HTTP/1.1 200 OK" + Environment.NewLine + "Content-Type: image/jpg" + Environment.NewLine + "Content-Length: "
+ frameBuffer.Length + "" + Environment.NewLine + "" + Environment.NewLine + "";
        byte[] data = Encoding.UTF8.GetBytes(header).Concat(frameBuffer).ToArray();

        socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
        //socket.Send(data, 0, data.Length, SocketFlags.None);

        socket.BeginReceive(
            _buffer,
            0,
            _buffer.Length,
            SocketFlags.None,
            new AsyncCallback(ReceiveCallback),
            socket
        );
    }



    private void SendCallback(IAsyncResult AR)
    {
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

