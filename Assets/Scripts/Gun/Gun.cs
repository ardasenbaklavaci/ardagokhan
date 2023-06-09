using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    GameObject BulletPrefab;
    [SerializeField]
    GameObject BulletOut;
    [SerializeField]
    float bulletSpeed = 600;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            shoot();
        }


    }

    void shoot() {
        Debug.Log("Shoot");
        GameObject bullet = Instantiate(BulletPrefab , BulletOut.transform.position , transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        Destroy(bullet,1);
    }
}
