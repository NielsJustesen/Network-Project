using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkProjectServer
{
    public class Troll : Enemy
    {
        private int health;
        private int dmg;
        public Troll(int health, int dmg) : base(health, dmg)
        {
            this.health = health;
            this.dmg = dmg;
        }
    }
}
