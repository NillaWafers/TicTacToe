using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class PlayCube
    {
        bool taken;
        char player;

        public bool Taken
        {
            get
            {
                return taken;
            }

            set
            {
                taken = value;
            }
        }

        public char Player
        {
            get
            {
                return player;
            }

            set
            {
                player = value;
            }

        }

        public bool TakenBy(char _player)
        {
            if (taken && _player == player)
                return true;

            return false;
        }

        public PlayCube()
        {
            taken = false;
            player = '0';
        }

        public bool TakeSpace(bool _playerOne)
        {
            if (!taken)
            {
                taken = true;

                if (_playerOne)
                    player = '1';
                else
                    player = '2';

                return true;
            }

            return false;
        }
    }
}
