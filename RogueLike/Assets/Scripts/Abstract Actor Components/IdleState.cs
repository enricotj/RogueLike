using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Abstract_Actor_Components
{
    public class IdleState : FSMState
    {
        
        public IdleState()
        {
            this.stateID = StateID.Idle;
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
            if (actor.MovementIntent.magnitude != 0)
            {
                actor.PerformTransition(Transition.StartWalk);
            }
        }

        public override void Act(ActorComponent actor)
        {
            
        }

    }
}
