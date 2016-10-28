using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Abstract_Actor_Components
{
    public class WalkingState : IdleState
    {

        public WalkingState()
        {
            this.stateID = StateID.Walking;
            this.name = "Walking";
        }

        public override void DoBeforeEntering()
        {
            base.DoBeforeEntering();
        }

        public override void DoBeforeLeaving()
        {
            base.DoBeforeLeaving();
        }

        public override void Reason(ActorComponent actor)
        {
            if (actor.animator.GetBool("TryAttack"))
            {
                actor.PerformTransition(Transition.StartAttack);
            }
            else if (actor.rigidBody.velocity.magnitude == 0)
            {
                actor.PerformTransition(Transition.EndWalk);
            }
        }

        public override void Act(ActorComponent actor)
        {
            base.Act(actor);
        }

        public override void ActFixed(ActorComponent actor)
        {
            Rigidbody2D rigidBody = actor.rigidBody;
            Vector2 movementIntent = actor.MovementIntent;
            float acceleration = actor.Acceleration;
            float moveSpeed = actor.MoveSpeed;

            rigidBody.AddForce(movementIntent * acceleration);
            rigidBody.velocity = Mathf.Clamp(rigidBody.velocity.magnitude, 0, moveSpeed) * rigidBody.velocity.normalized;
            if (movementIntent.magnitude == 0 && rigidBody.velocity.magnitude != 0)
            {
                rigidBody.AddForce(rigidBody.velocity.normalized * -1f * acceleration);
                if (rigidBody.velocity.magnitude < 0.001f)
                {
                    rigidBody.velocity = new Vector2(0, 0);
                }
            }
        }
    }
}
