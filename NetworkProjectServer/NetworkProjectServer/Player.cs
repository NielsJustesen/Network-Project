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
        private static int health;
        private int dmg;
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
            dmg = 15;
        }
        public static void PlayerTakeDamage(int dmg)
        {
            health = health - dmg;
        }
    }
}
