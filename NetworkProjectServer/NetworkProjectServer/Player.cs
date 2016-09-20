using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace NetworkProjectServer
{
    public class Player
    {
        public int potions { get; set; }
        public  int health { get; set; }
        public int dmg;
        IPEndPoint playerEP;

        public IPEndPoint PlayerEP
        {
            get
            {
                return playerEP;
            }

            set
            {
                playerEP = value;
            }
        }

        public Player(IPEndPoint playerEP)
        {
            this.PlayerEP = playerEP;
            health = 50;
            potions = 5;
           this.dmg = 15;
        }
        public int PlayerTakeDamage(int enemydmg, int health)
        {
          return  health = health - enemydmg;
        }
    }
}
