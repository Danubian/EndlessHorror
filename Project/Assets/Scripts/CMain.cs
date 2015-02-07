using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CMain : MonoBehaviour
{
    public string URL;
    public int Port;

    SocketIOClient.Client socket;

    void Start()
    {

        socket = new SocketIOClient.Client("http://" + URL + ":" + Port + "/");
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

