using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkProjectServer
{
    public class Orc : Enemy
    {
        private int health;
        private int dmg;
        public Orc(int health, int dmg) : base(health, dmg)
        {
            this.health = health;
            this.dmg = dmg;
        }
    }
}
