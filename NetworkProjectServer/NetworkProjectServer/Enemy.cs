using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkProjectServer
{
    public class Enemy
    {
        public int health;
        public int dmg;
        public Enemy(int health, int dmg)
        {
            this.health = health;
            this.dmg = dmg;
        }
        public void TakeDamage(int dmg,int health)
        {
            health = health - dmg;
        }
    }
}
