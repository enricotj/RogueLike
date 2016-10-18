using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(InputComponent))]
public class ActorComponent : MonoBehaviour {

    // define readonly stats
    protected float Acceleration = 100;
    protected float MoveSpeed = 5;
    
    // define inputtable actions
    public Vector2 MovementIntent = new Vector2(0, 0);

    // define components
    private InputComponent input;
    private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start ()
    {
        input = this.GetComponent<InputComponent>();
        rigidBody = this.GetComponent<Rigidbody2D>();
	}

    void Update()
    {
        input.ReadInputs(this.gameObject);
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        // TODO: refactor out into component?
        rigidBody.AddForce(MovementIntent * Acceleration);
        rigidBody.velocity = Mathf.Clamp(rigidBody.velocity.magnitude, 0, MoveSpeed) * rigidBody.velocity.normalized;
	}
}
