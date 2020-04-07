using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

struct levels
{
    public string current_level;
    public string target_level;
}
public class EndPoint : IPrintable
{
    private static GameObject endModel;
    private static string material_name;
    private Vector3 Place;
    private GameObject EndPointGRP;
    private bool _isPrinted = false;
    levels level = new levels();
    public EndPoint(float x, float y, float z ,string end_level, string current_level)
    {
        Place = new Vector3(x, y, z);
        level.current_level = current_level;
        level.target_level = end_level;
    }
    public static void set_material(string matName) { material_name = matName; }
    public static void set_model(GameObject T) { endModel = T; }

    public void Print()
    {
        if (_isPrinted)
            Un_print();
        EndPointGRP = MonoBehaviour.Instantiate(endModel, Place, Quaternion.identity);
        EndPointGRP.name = "EndPoint from: "+level.current_level+ " To: "+ level.target_level;
        EndPointGRP.AddComponent<BoxCollider>();
        EndPointGRP.AddComponent<end_pointScript>();
        EndPointGRP.GetComponent<end_pointScript>().Set_Level(level.current_level, level.target_level);
        _isPrinted = true;
    }

    public void Un_print()
    {
        MonoBehaviour.Destroy(EndPointGRP);
        _isPrinted = false;
    }

    // Update is called once per frame

}
