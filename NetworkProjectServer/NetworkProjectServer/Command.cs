using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkProjectServer
{
    class Command
    {
        public static Enemy enm;
        public Command()
        {

        }
        public static void Combat()
        {
            
        }
        public Enemy MeetEnemy()
        {
            Random rnd = new Random();
            int nmb = rnd.Next(1, 101);
            if (nmb <= 50)
            {
                Enemy enm = new Goblin();
            }
            else if (nmb >= 51 && nmb <= 75)
            {
                Enemy enm = new Orc();
            }
            else
            {
                Enemy enm = new Troll();
            }

            return enm;
        }
    }
}
