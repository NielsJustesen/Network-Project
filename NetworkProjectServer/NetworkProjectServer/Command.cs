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
        public string MeetEnemy()
        {
            Random rnd = new Random();
            int nmb = rnd.Next(1, 101);
            string enemy;
            if (nmb <= 50)
            {
                Enemy enm = new Goblin();
                enemy = "a goblin";
            }
            else if (nmb >= 51 && nmb <= 75)
            {
                Enemy enm = new Orc();
                enemy = "an orc";
            }
            else
            {
                Enemy enm = new Troll();
                enemy = "a troll";
            }

            return enemy;
        }
    }
}
