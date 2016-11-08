using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace Assets.Scripts.Abstract_Actor_Components.States
{
    public class IdleState : IState
    {

        public void OnEnter(ActorComponent actor)
        {
        }

        public void OnLeave(ActorComponent actor)
        {
        }

        public void FixedUpdate(ActorComponent actor)
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

        public void Update(ActorComponent actor)
        {
            actor.Look();
        }

    }
}
