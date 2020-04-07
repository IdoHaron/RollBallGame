using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setup1 : MonoBehaviour
{
    Platform p0 = new Platform(0,-1,0,5,0.5f,10);
    Platform p1 = new Platform(2.5f, -1, 7.5f, 20, 0.5f, 5);
    Platform p2 = new Platform(15f, -1, 11f,5,0.5f,20);
 
    EndPoint ep;

    Enemy e0 = new Enemy(5,1,7,1,1,1);
    Enemy e1 = new Enemy(25,1,20,1,1,1);
    // Start is called before the first frame update
    void Start()
    {
        p0.Set_Material(Resources.Load("Material/White") as Material);
        p0.Print();
        p1.Set_Material(Resources.Load("Material/Red") as Material);
        p1.Print();
        p2.Set_Material(Resources.Load("Material/Green") as Material);
        p2.Print();
        e0.Set_PrimitiveType(Resources.Load("Cube") as GameObject);
        e0.Print();
        e1.Set_PrimitiveType(Resources.Load("Cube") as GameObject);
        e1.Print();
        ep = new EndPoint(15, 30, 0.1f, "Winner", SceneManager.GetActiveScene().name);
    }
}
