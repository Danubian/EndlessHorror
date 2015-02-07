using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SocketIOClient;

public class ServerConnection : MonoBehaviour {
    SocketIOClient.Client socket;
    void Start()
    {
        Debug.Log("Attempting to connect to server...");
        System.Net.WebRequest.DefaultWebProxy = null;

        string url = "http://45.56.101.79:80/";
        Debug.Log("Server url: " + url);

        socket = new SocketIOClient.Client(url);

        socket.On("connect", (fn) =>
        {
            Debug.Log("connect - socket");
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("msg", "what's up?");
            socket.Emit("SEND", args);
        });

        socket.On("RECV", (data) =>
        {
            Debug.Log(data.Json.ToJsonString());
        });

        socket.Error += (sender, e) =>
        {
            Debug.Log("socket Error: " + e.Message.ToString());
        };

        socket.Connect();
    }

    void Update()
    {
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(20, 70, 150, 30), "SEND"))
        {
            Debug.Log("Sending");
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("msg", "hello!");
            socket.Emit("SEND", args);
        }
        if (GUI.Button(new Rect(20, 120, 150, 30), "Close Connection"))
        {
            Debug.Log("Closing");
            socket.Close();
        }
    }
}
