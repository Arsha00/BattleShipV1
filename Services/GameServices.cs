/*
 * Auhtor: Arian Sjöström
 * B.Sc Comuputer Science & Mobile IT
 * Malmö University & Luleå University Of Technology
 * Spec: AI Development / Software Engineering / GPD
 */

using BattleShipGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipGame.Services
{

    /*
     *Initizilazes the player and enemyboard
     *Random instances for AI move (if applied, currently not in use)
     *
    */
    public class GameService
    {
        
        private readonly Random _rnd = new();


        public void PlaceShipsRandomly(Board board, params int[] sizes)
        {
            board.Ships.Clear();
            foreach (var size in sizes)
            {
                bool placed = false;
                for (int tries = 0; tries < 200 && !placed; tries++)
                {
                    bool horizontal = _rnd.Next(2) == 0;
                    int r = _rnd.Next(board.Rows - (horizontal ? 0 : size - 1));
                    int c = _rnd.Next(board.Cols - (horizontal ? size - 1 : 0));


                    var candidate = Enumerable.Range(0, size)
                    .Select(i => board.Cells[r + (horizontal ? 0 : i), c + (horizontal ? i : 0)])
                    .ToArray();


                    if (candidate.Any(cell => cell.State == CellState.Ship)) continue;


                    var ship = new Ship();
                    foreach (var cell in candidate)
                    {
                        cell.State = CellState.Ship;
                        ship.Cells.Add(cell);
                    }
                    board.Ships.Add(ship);
                    placed = true;
                }
                if (!placed) throw new InvalidOperationException($"Could not place ship of that size {size}");
            }
        }


        public bool Attack(Board board, int row, int col, out bool sunk)
        {
            var cell = board.Cells[row, col];
            if (cell.State == CellState.Hit || cell.State == CellState.Miss)
            {
                sunk = false; return false; // already tried
            }
            if (cell.State == CellState.Ship)
            {
                cell.State = CellState.Hit;
                var ship = board.Ships.FirstOrDefault(s => s.Cells.Contains(cell));
                sunk = ship?.IsSunk ?? false;
                return true;
            }
            else
            {
                cell.State = CellState.Miss;
                sunk = false;
                return false;
            }
        }


        public bool AllShipsSunk(Board board) => board.Ships.All(s => s.IsSunk);
    }
}

