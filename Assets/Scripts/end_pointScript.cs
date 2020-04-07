using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class end_pointScript : MonoBehaviour
{
    levels level = new levels();

    public void Set_Level(string current, string target)
    {
        level.current_level = current;
        level.target_level = target;
    }
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag =="Player")
        {
            SceneManager.LoadScene(level.target_level);
        }
    }
}
