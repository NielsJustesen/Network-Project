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
        public string name { get; set; }
        public Enemy(int health, int dmg, string name)
        {
            this.health = health;
            this.dmg = dmg;
            this.name = name;
        }
        public int TakeDamage(int dmg,int health)
        {
         return   health = health - dmg;
        }
    }
}
