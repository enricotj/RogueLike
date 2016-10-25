using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class ActorComponent : MonoBehaviour {

    // TODO: refactor into StatMachine component
    // prototype of possible states
    public enum State
    {
        Idle,
        Moving,
        Attacking,
        InKnockback,
        Frozen
    }

    // define stats
    protected float Acceleration = 50;
    protected float MoveSpeed = 5;
    
    // define inputs
    public Vector2 MovementIntent = new Vector2(0, 0);
    public Vector2 LookTowards = new Vector2(0, 0);

    // define components
    private InputComponent input;
    private Rigidbody2D rigidBody;
    private Animator animator;
    

	// Use this for initialization
	void Start ()
    {
        input = new PlayerInputComponent();
        rigidBody = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
	}

    void FixedUpdate()
    {
        // TODO: refactor out into StateMachine component?

        Vector2 positionOnScreen = GameObject.Find("Main Camera").GetComponent<Camera>().WorldToViewportPoint(transform.position);
        Vector2 mouseOnScreen = (Vector2)GameObject.Find("Main Camera").GetComponent<Camera>().ScreenToViewportPoint(LookTowards);
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen) + 180;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        rigidBody.AddForce(MovementIntent * Acceleration);
        rigidBody.velocity = Mathf.Clamp(rigidBody.velocity.magnitude, 0, MoveSpeed) * rigidBody.velocity.normalized;
        if (MovementIntent.magnitude == 0 && rigidBody.velocity.magnitude != 0)
        {
            rigidBody.AddForce(rigidBody.velocity.normalized * -1f * Acceleration);
            if (rigidBody.velocity.magnitude < 0.001f)
            {
                rigidBody.velocity = new Vector2(0, 0);
            }
        }
    }

    void Update()
    {
        input.ReadInputs(this);
    }

    public virtual void Attack()
    {
        animator.SetTrigger("Attack");
    }


    private float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
