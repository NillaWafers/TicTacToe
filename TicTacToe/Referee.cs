using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public static class Referee
    {
        public static bool Draw(PlayCube[][,] _gameCube)
        {
            for (byte y = 0; y < _gameCube.Length; y++)
            {
                for (byte x = 0; x < _gameCube[y].GetLength(0); x++)
                {
                    for (byte z = 0; z < _gameCube[y].GetLength(1); z++)
                    {
                        if (!_gameCube[y][x, z].Taken)
                            return false;
                    }
                }
            }

            return true;
        }

        public static bool PlayerWon(PlayCube[][,] _gameCube, bool _playerOne)
        {
            YXZ[] takenCubes;
            bool yzWin = true;
            bool xyWin = true;
            bool xzWin = true;
            bool xyzWin = true;
            char player = '0';

            takenCubes = new YXZ[27];

            if (_playerOne)
                player = '1';
            else
                player = '2';

            for (byte y = 0; y < _gameCube.Length; y++)
            {
                for (byte x = 0; x < _gameCube[y].GetLength(0); x++)
                {
                    for (byte z = 0; z < _gameCube[y].GetLength(1); z++)
                    {
                        if (_gameCube[y][x, z].TakenBy(player))
                        {
                            int i = Array.FindLastIndex(takenCubes, t => t != null);

                            if (i > -1)
                                takenCubes[i + 1] = new YXZ(y, x, z);
                            else
                                takenCubes[0] = new YXZ(y, x, z);
                        }
                    }
                }
            }

            if (takenCubes[0] != null)
            {
                Array.Resize(ref takenCubes, Array.FindLastIndex(takenCubes, t => t != null) + 1);

                if (takenCubes.Length > 2)
                {
                    if (!Array.Exists(takenCubes, t => t.Y == 0)
                        || !Array.Exists(takenCubes, t => t.Y == 1)
                        || !Array.Exists(takenCubes, t => t.Y == 2)) //If we don't have anything in even one horizontal slice, we know we don't have any wins via CORNER TO CORNER orientation
                    {
                        xyzWin = false;
                    }

                    #region XYZ (corner to corner) win
                    // This is really the only one that needs custom programming
                    // the rest, "slice" checking, are all easily genericized
                    // can probably integrate this somehow, though
                    if (xyzWin)
                    {
                        xyzWin = false;

                        YXZ center = null;

                        if (Array.Exists(takenCubes, t => t.X == 1 && t.Y == 1 && t.Z == 1))
                            center = new YXZ(1, 1, 1);

                        if (center != null)
                        {
                            if (Array.FindAll(takenCubes     //FindAll and length check use contributed by Carl Dalton while brainstorming on 09/22/16
                                , s => (s.Z == center.Z - 1 && s.Y == center.Y - 1 && s.X == center.X - 1)
                                    || (s.Z == center.Z + 1 && s.Y == center.Y + 1 && s.X == center.X + 1)).Length == 2)
                            {
                                return true;
                            }

                            if (Array.FindAll(takenCubes
                                , s => (s.Z == center.Z + 1 && s.Y == center.Y - 1 && s.X == center.X + 1)
                                    || (s.Z == center.Z - 1 && s.Y == center.Y + 1 && s.X == center.X - 1)).Length == 2)
                            {
                                return true;
                            }

                            if (Array.FindAll(takenCubes
                                , s => (s.Z == center.Z - 1 && s.Y == center.Y - 1 && s.X == center.X + 1)
                                    || (s.Z == center.Z + 1 && s.Y == center.Y + 1 && s.X == center.X - 1)).Length == 2)
                            {
                                return true;
                            }

                            if (Array.FindAll(takenCubes
                                , s => (s.Z == center.Z - 1 && s.Y == center.Y + 1 && s.X == center.X + 1)
                                    || (s.Z == center.Z + 1 && s.Y == center.Y - 1 && s.X == center.X - 1)).Length == 2)
                            {
                                return true;
                            }
                        }
                    }
                    #endregion

                    if (xzWin && SliceCheck('Y', 'X', 'Z', takenCubes))
                        return true;

                    if (xyWin && SliceCheck('Z', 'X', 'Y', takenCubes))
                        return true;

                    if (yzWin && SliceCheck('X', 'Y', 'Z', takenCubes))
                        return true;
                }
            }

            return false;
        }


        private static bool SliceCheck(char _sliceBy, char _d1, char _d2, YXZ[] _takenCubes)
        {
            YXZ[] slice = null;

            for (byte d = 0; d <= (byte)SliceBounds.Upper; d++)
            {
                slice = _takenCubes.Where(t => t[_sliceBy] == d).ToArray();

                if (slice != null && slice.Length > (byte)SliceBounds.Upper)
                {
                    for (byte b = 0; b < slice.Length; b++)
                    {
                        if (slice[b][_d1] == (byte)SliceBounds.Middle)
                        {
                            if (Array.FindAll(slice 
                                , s => (s[_d1] == (byte)SliceBounds.Lower && s[_d2] == slice[b][_d2])
                                    || (s[_d1] == (byte)SliceBounds.Upper && s[_d2] == slice[b][_d2])).Length == (byte)SliceBounds.Upper)
                            {
                                return true;
                            }
                        }

                        if (slice[b][_d2] == (byte)SliceBounds.Middle)
                        {
                            if (Array.FindAll(slice
                                , s => (s[_d2] == (byte)SliceBounds.Lower && s[_d1] == slice[b][_d1])
                                    || (s[_d2] == (byte)SliceBounds.Upper && s[_d1] == slice[b][_d1])).Length == (byte)SliceBounds.Upper)
                            {
                                return true;
                            }
                        }

                        if (slice[b][_d1] == (byte)SliceBounds.Middle && slice[b][_d2] == (byte)SliceBounds.Middle)
                        {
                            if (Array.FindAll(slice
                                , s => (s[_d2] == (byte)SliceBounds.Lower && s[_d1] == (byte)SliceBounds.Lower)
                                    || (s[_d2] == (byte)SliceBounds.Upper && s[_d1] == (byte)SliceBounds.Upper)).Length == (byte)SliceBounds.Upper)
                            {
                                return true;
                            }

                            if (Array.FindAll(slice
                                , s => (s[_d2] == (byte)SliceBounds.Upper && s[_d1] == (byte)SliceBounds.Lower)
                                    || (s[_d2] == (byte)SliceBounds.Lower && s[_d1] == (byte)SliceBounds.Upper)).Length == (byte)SliceBounds.Upper)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}
