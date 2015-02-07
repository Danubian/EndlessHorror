using UnityEngine;
using System.Collections;

public class GlobalConstants : Singleton<GlobalConstants> {

    protected GlobalConstants() { } // guarantee this will be always a singleton only - can't use the constructor!
    
    void Awake ()
    {
        DontDestroyOnLoad(this);
    }

    //public string HOST = "45.56.101.79";
    //public int PORT = 3000;
    public string CONNECTION = "http://45.56.101.79:80/";
}
