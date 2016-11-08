using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace Assets.Scripts.Abstract_Actor_Components.States
{
    public class AttackingState : IState
    {
        public void OnEnter(ActorComponent actor)
        {
            actor.rigidBody.velocity = new Vector2(0, 0);
        }

        public void OnLeave(ActorComponent actor)
        {
        }

        public void FixedUpdate(ActorComponent actor)
        {
        }

        public void Update(ActorComponent actor)
        {
        }
    }
}
