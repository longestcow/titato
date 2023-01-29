using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playerMovement : MonoBehaviour
{
    Vector3 temp;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKey(KeyCode.W) && gameObject.GetComponent<RectTransform>().anchoredPosition.y != 120) {
            Debug.Log("W");
            temp=gameObject.GetComponent<RectTransform>().anchoredPosition;
            temp.y+=120;
            gameObject.GetComponent<RectTransform>().anchoredPosition=temp;
        }
        else if(Input.GetKey(KeyCode.S) && gameObject.GetComponent<RectTransform>().anchoredPosition.y != -120) {
            Debug.Log("S");
            temp=gameObject.GetComponent<RectTransform>().anchoredPosition;
            temp.y-=120;
            gameObject.GetComponent<RectTransform>().anchoredPosition=temp;
        }
        else if(Input.GetKey(KeyCode.A) && gameObject.GetComponent<RectTransform>().anchoredPosition.x != -120) {
            Debug.Log("A");
            temp=gameObject.GetComponent<RectTransform>().anchoredPosition;
            temp.x-=120;
            gameObject.GetComponent<RectTransform>().anchoredPosition=temp;
        }
        else if(Input.GetKey(KeyCode.D) && gameObject.GetComponent<RectTransform>().anchoredPosition.x != 120) {
            Debug.Log("D");
            temp=gameObject.GetComponent<RectTransform>().anchoredPosition;
            temp.x+=120;
            gameObject.GetComponent<RectTransform>().anchoredPosition=temp;
        }
    }
}
