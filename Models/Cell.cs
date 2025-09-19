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
using System.Windows.Controls;


/*
 * Simply properties, coordinations, states and an id (string) to identify a unique coordinate(box). 
 */

namespace BattleShipGame.Models
{

    public enum CellState {Empty, Ship, Miss, Hit }

    public class Cell
    {
        public int Row { get; set; }

        public int Col { get; set; }
        public CellState State
        { get; set; }

        public string id => $"R{Row}C{Col}";
    }
}

