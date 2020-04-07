using System.Collections;
using UnityEditor.UIElements;
using UnityEngine;
struct Size
{
    public float y;
    public float x;
    public float z;
}
public class Platform :  IPrintable, IDistance
{
    public static int maxDis = 10;
    public static ArrayList allPlat = new ArrayList();
    private Vector3 place;
    private static int platform_count = 0;
    private Material material;
    private bool _gameObjectDef = false;
    private GameObject platform_GameObject;
    private Size size = new Size();
    private Mesh plat;
    private int platform_num;
    public Platform(float x, float y, float z, float Size_x, float Size_y, float Size_z)
    {
        platform_num = platform_count;
        size.x = Size_x;
        size.y = Size_y;
        size.z = Size_z;
        place = new Vector3(x, y, z);
        allPlat.Add(this);
        platform_count++;
    }
    public void Set_Material(Material material) { this.material = material; }
    public void Print()
    {
        /*Vector3[] vertices = CalcCorners();        //Vector2[] uv = new Vector2[0];        int[] triangles = new int[0];        plat = new Mesh();      plat.vertices = vertices;        //plat.uv = uv;      plat.triangles = triangles; string _name = "platform" + platform_count;*/
        if (_gameObjectDef)
            Un_print();
        platform_GameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        platform_GameObject.name = ("platform" + this.platform_num);
        platform_GameObject.transform.localScale = new Vector3(size.x, size.y, size.z);
        platform_GameObject.transform.localPosition = place;
        _gameObjectDef = true;
        platform_GameObject.GetComponent<Renderer>().material = this.material;
        platform_GameObject.AddComponent<BoxCollider>();
        platform_GameObject.gameObject.tag = "Platform";

    }
    public void Un_print()
    {
        MonoBehaviour.Destroy(platform_GameObject);
        _gameObjectDef = false;
    }
    public bool TooFar(Vector3 loc)
    {
        Debug.Log("Too Far+ "+this.Distance(loc));
        return this.Distance(loc)>maxDis;
    }
    public float Distance(Vector3 loc) {
        return Mathf.Sqrt(Mathf.Pow(place.x - loc.x, 2) + Mathf.Pow(place.y - loc.y, 2) + Mathf.Pow(place.z - loc.z, 2));
    }
}
