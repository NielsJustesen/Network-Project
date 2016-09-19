using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkProjectServer
{
    public class Goblin : Enemy
    {
        private int health;
        private int dmg;
        public Goblin(int health, int dmg) : base(health, dmg)
        {
            this.health = health;
            this.dmg = dmg;
        }
    }
}
