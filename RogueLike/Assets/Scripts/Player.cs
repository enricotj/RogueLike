using UnityEngine;
using System.Collections;

using Assets.Scripts;

public class Player : MonoBehaviour {

    public GameObject bulletPrefab;

    private GameObject cam;

    private PlayerStats stats;

    private const float SHAKE_AMOUNT = 0.1f;
    private float shakeTimeStop = 0;

    private float acceleration = 50;
    private float friction = 0.6f;
    private Rigidbody2D rb;

    public float Acceleration
    {
        get
        {
            return acceleration;
        }
    }

	// Use this for initialization
	void Start ()
    {
        cam = GameObject.Find("MainCamera");
        stats = new PlayerStats(
            5,   // max health
            5.0f);   // move speed
        rb = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Attack/ability logic
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 looking = (transform.rotation * Vector3.right) * 0.4f;
            Instantiate(bulletPrefab, new Vector3(transform.position.x + looking.x, transform.position.y + looking.y, 10.0f), transform.rotation);
        }
	}

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //shakeTimeStop = Time.realtimeSinceStartup + 1.0f;
        }

        Move();
    }

    private void Move()
    {
        // normal movement
        Vector3 move = CaptureMovementInput();
        float dx = move.x;
        float dy = move.y;
		float ax = dx * acceleration;
		float ay = dy * acceleration;
		this.rb.AddForce(new Vector2 (ax, ay));
		this.rb.velocity = Mathf.Clamp(this.rb.velocity.magnitude, 0, stats.MoveSpeed) * this.rb.velocity.normalized;
        if (dx == 0 && dy == 0 && this.rb.velocity.magnitude != 0)
		{
            this.rb.AddForce(this.rb.velocity.normalized * -0.75f * acceleration);
			if (this.rb.velocity.magnitude < 0.001f)
			{
				this.rb.velocity = new Vector2(0, 0);
			}
		}

        // adjust camera offset
        Vector3 mouseInWorld = cam.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        Vector3 cameraOffset = transform.position + (mouseInWorld - transform.position) * 0.3f;
        cameraOffset.z = -10.0f;
        if (Time.realtimeSinceStartup < shakeTimeStop)
        {
            cameraOffset.x += Random.Range(-SHAKE_AMOUNT, SHAKE_AMOUNT);
            cameraOffset.y += Random.Range(-SHAKE_AMOUNT, SHAKE_AMOUNT);
        }
        cam.transform.position = cameraOffset;

        // mouse aim
        Vector2 positionOnScreen = cam.GetComponent<Camera>().WorldToViewportPoint(transform.position);
        Vector2 mouseOnScreen = (Vector2)cam.GetComponent<Camera>().ScreenToViewportPoint(Input.mousePosition);
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen) + 180;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private Vector3 CaptureMovementInput()
    {
        float dx = 0;
        float dy = 0;

        if (Input.GetKey(KeyCode.W))
        {
            dy += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            dx -= 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            dy -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            dx += 1;
        }

        return (new Vector3(dx, dy, 0)).normalized;
    }

    void OnGUI()
    {
    }

    private float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
