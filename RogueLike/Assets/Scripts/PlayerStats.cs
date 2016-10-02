using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class PlayerStats
    {
        /*
        public enum Status
        {
            Vulnerable,
            Intangible,
            Invincible
        }
        private Status status;
        */

        private float maxHealth;
        private float health;

        private float moveSpeed;

        public float MaxHealth
        {
            get { return maxHealth; }
            set { maxHealth = value; }
        }
        public float Health
        {
            get { return health; }
            set { health = value; }
        }
        public float MoveSpeed
        {
            get { return moveSpeed; }
            set { moveSpeed = value; }
        }

        public PlayerStats(float maxHealth, float moveSpeed)
        {
            this.maxHealth = maxHealth;
            this.health = this.maxHealth;
            this.moveSpeed = moveSpeed;
        }
    }
}
