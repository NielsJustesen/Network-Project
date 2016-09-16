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
        private int health = 50;
        private int dmg = 10;
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
        }
        public static void Attack()
        {

        }
        public static void TakeDamage()
        {

        }
    }
}
