/*
 * Auhtor: Arian Sjöström
 * B.Sc Comuputer Science & Mobile IT
 * Malmö University & Luleå University Of Technology
 * Spec: AI Development / Software Engineering / GPD
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Identifies the cells that the ship´s made of.
 * IsSunk returns true when all cells for a ships been HIT
 */

namespace BattleShipGame.Models
{
    public class Ship
    {
        public List<Cell> Cells { get; } = new List<Cell>();
        public bool IsSunk => Cells.All(c => c.State == CellState.Hit);

    }
}

