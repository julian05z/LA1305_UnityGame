using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMotion : MonoBehaviour
{

    public float speed = 3.0f;
    public float rechts;
    public float links;

    private Quaternion originalRotation;

    
    // Start is called before the first frame update
    void Start()
    {
        rechts = transform.position.x + rechts;
        links = transform.position.x - links;

        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (transform.position.x < links)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0) * originalRotation;
        }
        if(transform.position.x > rechts)
        {
            transform.rotation = originalRotation;
        }
    }
}
