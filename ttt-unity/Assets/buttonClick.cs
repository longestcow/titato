using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonClick : MonoBehaviour
{
    
    public string state;
    Button but;
    Image img;
    Sprite x;
    GameObject buttons;
    

    void Start()
    {
        but= GameObject.Find(gameObject.name).GetComponent<Button>();
        img= GameObject.Find(gameObject.name).GetComponent<Image>();
        but.onClick.AddListener(ButtonClick);
        
        x=GameObject.Find("buttons").GetComponent<Image>().sprite;
        state=".";
        buttons=GameObject.Find("buttons");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ButtonClick(){
        state="x";
        Color temp = GameObject.Find(gameObject.name).GetComponent<Image>().color;
        temp.a=1;
        img.sprite=x;
        img.color=temp;
        but.enabled=false;
        string st="";
        int i = 1;
        foreach(Transform child in buttons.transform){
           st+=child.GetComponent<buttonClick>().state; 
           if(i%3==0){
            st+="/";
           }
           i++;
        }
        //send to java and receive and check
        Debug.Log(st);

        GameObject.Find("buttons").GetComponent<clientSocket>().whenThe(st);
    }
}
