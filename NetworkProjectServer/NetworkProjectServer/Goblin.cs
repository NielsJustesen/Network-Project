using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkProjectServer
{
    class Goblin : Enemy
    {
        private static int health;
        public Goblin()
        {
            health = 15;
        }
        public static void GoblinTakeDamage(int dmg)
        {
            health = health - dmg;
        }
    }
}
