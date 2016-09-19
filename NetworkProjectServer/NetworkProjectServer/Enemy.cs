using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkProjectServer
{
    public class Enemy
    {
        public int health { get; set; }
        public int dmg;
        public Enemy(int health, int dmg)
        {
            this.health = health;
            this.dmg = dmg;
        }
        public int TakeDamage(int dmg,int health)
        {
         return   health = health - dmg;
        }
    }
}
