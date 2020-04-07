using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour, IPrintable, IFall
{
    public static List<Enemy> printed_enemies = new List<Enemy>(); 
    private Vector3 Place;

    private Size size;
    private bool _isPrinted = false;
    private int enemy_number;
    private GameObject Enemy_grp;
    private Platform On_Platform;
    //
    private static int enemy_count = 0;
    private static string material_name;
    public static GameObject Enemy_Type;
    public Enemy(float x, float y, float z, float Size_x, float Size_y, float Size_z)
    {
        Place = new Vector3(x, y, z);
        size.x = Size_x;
        size.y = Size_y;
        size.z = Size_z;
        enemy_number = enemy_count;
        enemy_count++;
    }
    public static void Set_material(string material_nameP){material_name = material_nameP;}
    public static void Set_PrimitiveType(GameObject EnemyT) { Enemy_Type = EnemyT; }
    public void Set_PlatformOn(Platform k) { On_Platform = k; }
    public void Print()
    {
        if (_isPrinted)
            Un_print();
        Enemy_grp = Instantiate(Enemy_Type, Place, Quaternion.identity);
        Enemy_grp.name = "Enemy" + enemy_number;
        Enemy_grp.transform.localScale = new Vector3(size.x, size.y, size.z);
        //
        Enemy_grp.GetComponent<Renderer>().material = Resources.Load<Material>(material_name);
        Enemy_grp.AddComponent<BoxCollider>();
        Enemy_grp.AddComponent<Enemy_script>();
        Enemy_grp.AddComponent<Rigidbody>(); //  nice!
        //
        Enemy_grp.GetComponent<Enemy_script>().setProperties(this);
        //Enemy_grp.GetComponent<Enemy_script>().player = player;
        printed_enemies.Add(this);
        _isPrinted = true;

    }
    public void Un_print() 
    {
        Destroy(Enemy_grp);
        printed_enemies.Remove(this);
        _isPrinted = false;
    }
    public void set_Platform(Platform a) { On_Platform = a; }
    public Platform GetPlatform() { return On_Platform; }
    public void Fall(){ Un_print(); Print(); }
    public void Distance_platform() {
        if (On_Platform == null) { return;  }
        if (On_Platform.TooFar(Enemy_grp.transform.localPosition)) { this.Fall(); } 
    }
    public void visible(int miliSeconds, Vector3 location_player)
    {
        Enemy_grp.GetComponent<Enemy_script>().visible(miliSeconds);
    }
}
