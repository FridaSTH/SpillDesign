using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGeneration : MonoBehaviour
{
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void FireBullet(float speed) {
        //print("fired at " + speed + " speed"); //new Vector3(0 * 2.0f, 0, 0)
        GameObject singleBullet = Instantiate(bullet, transform.position, Quaternion.identity);
    }
}