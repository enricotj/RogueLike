using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Abstract_Actor_Components;
using Assets.Scripts.Abstract_Actor_Components.States;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class ActorComponent : MonoBehaviour {

    // define components
    private InputComponent input;
    public Rigidbody2D rigidBody;
    public Animator animator;

    // define state machine
    private int currentState = 0;
    private Dictionary<int, IState> states; // shortNameHash => IState

    // define stats
    protected float acceleration = 50;
    protected float moveSpeed = 5;
    protected float Rotation = 0;

    public float Acceleration { get { return acceleration; } }
    public float MoveSpeed { get { return moveSpeed; } }
    public Vector3 Position { get { return this.transform.position; } }

    // define inputs
    public Vector2 MovementIntent = new Vector2(0, 0);
    public float RotationIntent = 0;

	// Use this for initialization
	void Start ()
    {
        input = new PlayerInputComponent();
        rigidBody = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        
        states = new Dictionary<int, IState>();
        states.Add(Animator.StringToHash("Idle"), new IdleState());
        states.Add(Animator.StringToHash("Attacking"), new AttackingState());
	}

    #region Update
    void FixedUpdate()
    {
        CheckStateTransition();
        states[currentState].FixedUpdate(this);
    }

    void Update()
    {
        ResetAnimatorParameters();
        input.ReadInputs(this);
        
        CheckStateTransition();
        states[currentState].Update(this);

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Rotation));
    }
    #endregion

    #region Actions
    public virtual void Look()
    {
        Rotation = RotationIntent;
    }

    public virtual void Attack()
    {
        animator.SetBool("TryAttack", true);
    }
    #endregion

    #region Logic Control
    public virtual void ResetAnimatorParameters()
    {
        animator.SetBool("TryAttack", false);
    }

    private void CheckStateTransition()
    {
        int state = animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
        if (currentState != state)
        {
            if (currentState != 0)
            {
                states[currentState].OnLeave(this);
            }

            currentState = state;

            states[currentState].OnEnter(this);
        }
    }
    #endregion

}
