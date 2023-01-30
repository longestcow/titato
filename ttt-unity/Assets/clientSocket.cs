using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;

public class clientSocket : MonoBehaviour
{

    public AudioSource aud;
    IPEndPoint addr;
    Socket sock;
    Sprite o;
    string rcv;
    void Start()
    {
        try{
        addr = new IPEndPoint(IPAddress.Loopback, 8080);
        Debug.Log("addr");
        sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Debug.Log("sock");
        sock.Connect(addr);
        Debug.Log("connect");
        }
        catch(SocketException e){
            Debug.Log(e);
            Application.Quit();
        }
        o=GameObject.Find("graphics").GetComponent<Image>().sprite;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) {
            aud.Play();
            foreach(Transform child in gameObject.transform) {
                child.GetComponent<buttonClick>().state="."; 
                Color temp = child.GetComponent<Image>().color;
                temp.a=0;
                child.GetComponent<Image>().color=temp;
                child.GetComponent<Image>().sprite=null;
                child.GetComponent<Button>().enabled=true; 
            }
        }
    }

    public void whenThe(string st){
        byte[] bytes = System.Text.Encoding.ASCII.GetBytes(st);
        sock.Send(bytes);

        //receive
        byte[] rcvBytes = new byte[20];
        sock.Receive(rcvBytes);
        rcv = System.Text.Encoding.ASCII.GetString(rcvBytes);
        Debug.Log("r: "+rcv);
        rcv=rcv.Replace("/", "");
        if(rcv.StartsWith("won:")){ 
            Debug.Log("won by " + rcv.ToCharArray()[4]);
            if(rcv.ToCharArray()[4]=='o'){
                rcv=rcv.Split(new char[] {'$'})[1];
                theWhen();
            }
            foreach(Transform child in GameObject.Find("buttons").transform)
                child.GetComponent<Button>().enabled=false; 
        }
        else
            theWhen();
    }


    public void theWhen() {

            int i = 0;
            string curr;
            foreach(Transform child in GameObject.Find("buttons").transform){
                curr = Char.ToString(rcv.ToCharArray()[i]);
                
                if(curr=="o"&&child.GetComponent<Button>().enabled==true){
                        child.GetComponent<buttonClick>().state=(curr);
                        Color temp = child.GetComponent<Image>().color;
                        temp.a=1;
                        child.GetComponent<Image>().color=temp;
                        child.GetComponent<Image>().sprite=o;
                        child.GetComponent<Button>().enabled=false; 
                }
                i++;
            }
    }
}

