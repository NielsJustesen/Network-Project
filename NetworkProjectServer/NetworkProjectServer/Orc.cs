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
        private string name;

        public Orc(int health, int dmg, string name) : base(health, dmg, name)
        {
            this.health = health;
            this.dmg = dmg;
            this.name = name;
        }
    }
}
