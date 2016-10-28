using UnityEngine;
using System.Collections;
using Assets.Scripts.Abstract_Actor_Components;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class ActorComponent : MonoBehaviour {

    // define components
    private InputComponent input;
    public Rigidbody2D rigidBody;
    public Animator animator;
    private ActorFSM fsm;

    // define stats
    protected float acceleration = 50;
    protected float moveSpeed = 5;
    protected float Rotation = 0;

    public float Acceleration { get { return acceleration; } }
    public float MoveSpeed { get { return moveSpeed; } }
    public Vector3 Position { get { return this.transform.position; } }

    public bool IsAnimationInSync { get { return animator.GetCurrentAnimatorStateInfo(0).IsName(fsm.CurrentState.Name); } }
    
    // define inputs
    public Vector2 MovementIntent = new Vector2(0, 0);
    public float RotationIntent = 0;

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
        idle.AddTransition(Transition.StartAttack, StateID.Attacking);

        WalkingState walking = new WalkingState();
        walking.AddTransition(Transition.EndWalk, StateID.Idle);
        walking.AddTransition(Transition.StartAttack, StateID.Attacking);

        AttackingState attacking = new AttackingState();
        attacking.AddTransition(Transition.EndAttack, StateID.Idle);

        fsm = new ActorFSM();
        fsm.AddState(idle);
        fsm.AddState(walking);
        fsm.AddState(attacking);
    }

    public virtual void PerformTransition(Transition t)
    {
        fsm.PerformTransition(t);
    }

    void FixedUpdate()
    {
        fsm.CurrentState.ActFixed(this);
    }

    void Update()
    {
        ResetAnimatorParameters();
        input.ReadInputs(this);
        
        fsm.CurrentState.Reason(this);
        fsm.CurrentState.Act(this);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Rotation));
    }

    public virtual void Look()
    {
        Rotation = RotationIntent;
    }

    public virtual void Attack()
    {
        this.rigidBody.velocity = new Vector2(0, 0);
        animator.SetBool("TryAttack", true);
    }

    public virtual void ResetAnimatorParameters()
    {
        animator.SetBool("TryAttack", false);
    }

}
