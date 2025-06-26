using System.Linq;
using TMPro;
using UnityEngine;

public class TextReverse : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject text;
    void Start()
    {
        
    }

    public void reverseText()
    {
        TMP_InputField input = text.GetComponent<TMP_InputField>();
        input.text = new string(input.text.Reverse().ToArray());

    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
