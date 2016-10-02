using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    private float speed = 18.0f;

    private Rigidbody2D rb;

	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 looking = transform.rotation * Vector3.right;
        rb.velocity = (new Vector2(looking.x, looking.y)) * speed;
    }
	
	void Update ()
    {
	    
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            Destroy(this.gameObject);
        }
    }
}
