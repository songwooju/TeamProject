using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float time;
    public GameObject prefabBullet;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        FireBullet();
    }
    public void FireBullet()
    {
        time += Time.deltaTime;
        //Debug.Log("Fire" + time);
        if (time > 0.3f)
        {
            Instantiate(prefabBullet, transform.position, Quaternion.identity);
            time -= 0.5f;
        }
    }
}
