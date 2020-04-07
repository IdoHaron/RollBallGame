using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Timers;

public class Player : MonoBehaviour
{

    public GameObject camera;
    public Platform on;

    public bool visible;

    Rigidbody rb;
    Vector3 camera0;

    float g;
    float maxVelocity;
    float maxangleForwads;
    float maxangleSideways;
    float angleForwards;
    float angleSideways;
    float CameraHeight;
    float r0;
    float minheight;

    bool canJump;
    int died_Frames = 0;

    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        tag = "Player";
        g = 9.8f;
        maxVelocity = 10f;
        maxangleSideways = 10;
        maxangleForwads = 30;
        CameraHeight = 0.1f;
        camera0 = camera.transform.position;
        r0 = (camera0 - transform.position).sqrMagnitude;
    }

    // Update is called once per frame
    void Update()
    {
        if (died_Frames != 0)
        {
            died_Frames--;
            if (died_Frames == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            return;
        }
        TiltGravity(-transform.up, rb, g);
        float[] angles = calculateAngle();
        transform.eulerAngles = new Vector3(angleSideways, 0, angleForwards);
        CameraFollow(camera, angles, gameObject, CameraHeight, camera0);
        if(Mathf.Abs(angleSideways)>= maxangleSideways/2 || Mathf.Abs(angleForwards)>= maxangleForwads/ 2)
        {
            EnemyVisible();
        }
        else
        {
            EnemyInvisible();
        }
        Debug.Log("on: " + on);
        if (on!=null&&on.TooFar(this.transform.position))
        {
            died_Frames = 120;
        }

    }
    static void TiltGravity(Vector3 bottom, Rigidbody r, float g)
    {
        r.AddForce(g * bottom);
    }
    float[] calculateAngle()
    {
        angleForwards = Input.GetAxis("Horizontal") * maxangleForwads;
        angleSideways = -Input.GetAxis("Vertical") * maxangleSideways;
        float[] l = { angleForwards, angleSideways };
        return l;
    }
    void CameraFollow(GameObject camera, float[] c, GameObject p, float cameraHeight, Vector3 camera0)
    {
        camera.transform.eulerAngles = new Vector3(maxangleSideways + transform.eulerAngles.x, 0, transform.eulerAngles.z);
        float y = r0 * Mathf.Sin(Mathf.Deg2Rad * p.transform.eulerAngles.x);
        if (y < 0)
        {
            y = 0;
        }
        camera.transform.position = p.transform.position + new Vector3(0, camera0.y + r0 * Mathf.Sin(Mathf.Deg2Rad * p.transform.eulerAngles.x), -r0 * Mathf.Cos(Mathf.Deg2Rad * p.transform.eulerAngles.x));
    }
    void Jump(bool canJump, Vector3 top, float height)
    {
        if (Input.GetAxis("Jump") > 0 && canJump)
        {
            rb.AddForce(top * height, ForceMode.Impulse);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag =="Platform")
        {
            Debug.Log("name Platform: " + collision.gameObject.name);
            int Plat_num = int.Parse(collision.gameObject.name.Substring(8));
            on = (Platform) Platform.allPlat[Plat_num];
            Debug.Log(on);
        }
        if (collision.gameObject.tag == "Floor" && collision.gameObject.transform.position.y < transform.position.y)
        {
            canJump = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" && collision.gameObject.transform.position.y < transform.position.y)
        {
            canJump = false;
        }
    }
    void EnemyVisible()
    {
        GameObject[] enemies = (GameObject[])FindObjectsOfType<GameObject>();
        foreach (GameObject e in enemies)
        {
            if (e.tag == "Enemy")
            {
                e.GetComponent<Enemy_script>().visible(1000);
            }
        }
    }
    void EnemyInvisible()
    {
        GameObject[] enemies = (GameObject[])FindObjectsOfType<GameObject>();
        foreach (GameObject e in enemies)
        {
            if (e.tag == "Enemy")
            {
                e.GetComponent<Enemy_script>().invis();
            }
        }
    }
    public void OnGUI()
    {
        if (died_Frames != 0)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 300f, 300f), "You died, isn't it ironic, don't you think?");
        }
    }
}
