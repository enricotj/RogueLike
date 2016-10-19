using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
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
    protected float Acceleration = 100;
    protected float MoveSpeed = 5;
    
    // define inputs
    public Vector2 MovementIntent = new Vector2(0, 0);

    // define components
    private InputComponent input;
    private Rigidbody2D rigidBody;
    

	// Use this for initialization
	void Start ()
    {
        input = new PlayerInputComponent();
        rigidBody = this.GetComponent<Rigidbody2D>();
	}

    void FixedUpdate()
    {
        input.ReadInputs(this);

        // TODO: refactor out into StateMachine component?
        rigidBody.AddForce(MovementIntent * Acceleration);
        rigidBody.velocity = Mathf.Clamp(rigidBody.velocity.magnitude, 0, MoveSpeed) * rigidBody.velocity.normalized;
        if (MovementIntent.magnitude == 0 && rigidBody.velocity.magnitude != 0)
        {
            rigidBody.AddForce(rigidBody.velocity.normalized * -0.75f * Acceleration);
            if (rigidBody.velocity.magnitude < 0.001f)
            {
                rigidBody.velocity = new Vector2(0, 0);
            }
        }
    }

    void Update()
    {

    }
	
}
