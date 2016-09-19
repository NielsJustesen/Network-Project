using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkProjectServer
{
    class Command
    {
       
        public Command()
        {

        }
        public bool Combat(Player player, Enemy enemy)
        {
           
            player.PlayerTakeDamage(player.health, enemy.dmg);
            Console.WriteLine("player health : "+player.health);
            Console.WriteLine("enemy damage : " + enemy.dmg);
            enemy.TakeDamage( enemy.health,player.dmg);
            Console.WriteLine("enemy health : " + enemy.health);
            Console.WriteLine("player damage : "+player.dmg);
            if (player.health<=0|| enemy.health<=0)
            {
                return false;
            }
            return true;
        }
        public string MeetEnemy()
        {
            Random rnd = new Random();
            int nmb = rnd.Next(1, 101);
            string enemy;
            if (nmb <= 50)
            {
                Enemy enm = new Goblin(15,5);
                enemy = "a goblin";
                Program.Enemies.Add(enm);

            }
            else if (nmb >= 51 && nmb <= 75)
            {
                Enemy enm = new Orc(20,10);
                enemy = "an orc";
                Program.Enemies.Add(enm);
            }
            else
            {
                Enemy enm = new Troll(25,15);
                enemy = "a troll";
                Program.Enemies.Add(enm);
            }

            return enemy;
        }
    }
}
