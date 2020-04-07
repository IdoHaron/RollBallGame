using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class end_script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnGUI()
    {
        GUI.TextArea(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), "you won");
    }
}
