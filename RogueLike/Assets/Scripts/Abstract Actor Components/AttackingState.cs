using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace Assets.Scripts.Abstract_Actor_Components
{
    public class AttackingState : FSMState
    {
        public AttackingState()
        {
            this.stateID = StateID.Attacking;
            this.name = "Attacking";
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
            if (!actor.IsAnimationInSync)
            {
                actor.PerformTransition(Transition.EndAttack);
            }
        }

        public override void Act(ActorComponent actor)
        {
            
        }

        public override void ActFixed(ActorComponent actor)
        {
            
        }

    }
}
