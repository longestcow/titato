using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playerMovement : MonoBehaviour
{
    Dictionary<string, Button> dict = new Dictionary<string, Button>();
    Vector3 temp;
    Sprite x;
    GameObject buttons;
    Vector3 pos;
    public AudioSource move;
    public AudioSource place;
    public AudioSource noplace;
    void Start()
    {
        int k = 1;
        for(int i = 120; i >= -120; i-=120){
            for(int j = -120; j <= 120; j+=120){
                dict.Add(j+","+i, GameObject.Find(""+k).GetComponent<Button>());
                Debug.Log(k+" = " + j+","+i);
                k+=1;
            }
        }

        x=GameObject.Find("buttons").GetComponent<Image>().sprite;
        buttons=GameObject.Find("buttons");

       
    }

    // Update is called once per frame
    void Update()
    {
        pos = gameObject.GetComponent<RectTransform>().anchoredPosition;
        if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))&& pos.y != 120) {
            temp=pos;
            temp.y+=120;
            move.Play();
            gameObject.GetComponent<RectTransform>().anchoredPosition=temp;
        }
        else if((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && pos.y != -120) {
            temp=pos;
            temp.y-=120;
            move.Play();
            gameObject.GetComponent<RectTransform>().anchoredPosition=temp;
        }
        else if((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && pos.x != -120) {
            temp=pos;
            temp.x-=120;
            move.Play();
            gameObject.GetComponent<RectTransform>().anchoredPosition=temp;
        }
        else if((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && pos.x != 120) {
            temp=pos;
            temp.x+=120;
            move.Play();
            gameObject.GetComponent<RectTransform>().anchoredPosition=temp;
        }
        else if(Input.GetKeyDown(KeyCode.Space)){
            if(dict[pos.x+","+pos.y].enabled!=true){
                noplace.Play();
            }
            else {
                place.Play();
                dict[pos.x+","+pos.y].gameObject.GetComponent<buttonClick>().ButtonClick();
            }
        }
    }
}
