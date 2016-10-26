using UnityEngine;
using System.Collections;
using Assets.Scripts.Abstract_Actor_Components;

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
    protected float acceleration = 50;
    protected float moveSpeed = 5;

    public float Acceleration
    {
        get
        {
            return acceleration;
        }
    }

    public float MoveSpeed
    {
        get
        {
            return moveSpeed;
        }
    }
    
    // define inputs
    public Vector2 MovementIntent = new Vector2(0, 0);
    public Vector2 LookTowards = new Vector2(0, 0);

    // define components
    private InputComponent input;
    public Rigidbody2D rigidBody;
    public Animator animator;
    private ActorFSM fsm;

	// Use this for initialization
	void Start ()
    {
        input = new PlayerInputComponent();
        rigidBody = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();

        MakeFSM();
	}

    protected virtual void MakeFSM()
    {
        IdleState idle = new IdleState();
        idle.AddTransition(Transition.StartWalk, StateID.Walking);

        WalkingState walking = new WalkingState();
        walking.AddTransition(Transition.EndWalk, StateID.Idle);

        fsm = new ActorFSM();
        fsm.AddState(idle);
        fsm.AddState(walking);
    }

    public virtual void PerformTransition(Transition t)
    {
        fsm.PerformTransition(t);
    }

    void FixedUpdate()
    {
        fsm.CurrentState.Reason(this);
        fsm.CurrentState.Act(this);
    }

    void Update()
    {
        input.ReadInputs(this);

        // TODO: refactor into FSM system
        Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Vector2 positionOnScreen = camera.WorldToViewportPoint(transform.position);
        Vector2 mouseOnScreen = (Vector2)camera.ScreenToViewportPoint(LookTowards);
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen) + 180;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public virtual void Attack()
    {
        // TODO: refactor into FSM system
        animator.SetTrigger("Attack");
    }

    private float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
