using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Enemy_script : MonoBehaviour, IDistance
{

    public GameObject player;
    private Enemy this_object;
    float speed;
    float minDistance;
    float maxDistance;
	// Use this for initialization
	void Start () {
        speed = 0.05f;
        minDistance = speed;
        maxDistance = 20f;
        player = GameObject.Find("Player");
        GetComponent<MeshRenderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(direction());
        this_object.Distance_platform();
        if (this_object.GetPlatform()!=null && this_object.GetPlatform().TooFar(transform.position))
            this_object.Print();
	}
    public Vector3 direction ()
    {
        Vector3 d = (player.transform.position-transform.position)/(player.transform.position - transform.position).sqrMagnitude * speed;
        if ((player.transform.position - transform.position).sqrMagnitude > minDistance && (player.transform.position - transform.position).sqrMagnitude < maxDistance)
        {
            return d;
        }
        else
        {
            return new Vector3(0,0,0);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject colided_with = collision.gameObject;
        if (colided_with.tag == "Platform")
        {
            int num =int.Parse( colided_with.name.Substring(8));
            this_object.set_Platform((Platform) Platform.allPlat[num]);
        }
    }
    public void setProperties(Enemy thisEnm) {  this_object = thisEnm; }
    public float Distance(Vector3 loc)
    {
        Vector3 place = transform.localPosition;
        return Mathf.Sqrt(Mathf.Pow(place.x - loc.x, 2) + Mathf.Pow(place.y - loc.y, 2) + Mathf.Pow(place.z - loc.z, 2));
    }
    public void visible(int miliSeconds)
    {
        GetComponent<MeshRenderer>().enabled = true;
        System.Timers.Timer timer = new System.Timers.Timer(miliSeconds);
        timer.Elapsed += in_visible;
        timer.AutoReset = false;
    }
    public void invis()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
    public void in_visible(object obj, ElapsedEventArgs e) { GetComponent<MeshRenderer>().enabled = false; }
}


