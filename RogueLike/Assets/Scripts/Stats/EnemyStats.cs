using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class EnemyStats : Stats
    {
        public EnemyStats()
        {

        }

        public EnemyStats(int maxHealth, float moveSpeed) :
            base(maxHealth, moveSpeed)
        {

        }
    }
}
