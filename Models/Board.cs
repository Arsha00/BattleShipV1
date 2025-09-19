/*
 * Auhtor: Arian Shaafi
 * B.Sc Comuputer Science & Mobile IT
 * Alma Mater: Malmö University & Luleå University Of Technology
 * Spec: AI Development / Software Engineering / GPD
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Lists all cells and ships
 * 
 */

namespace BattleShipGame.Models
{

    public class Board
    {
        public int Rows { get; }
        public int Cols { get; }
        public Cell[,] Cells { get; } //Gets a specific cell
        public List<Ship> Ships { get; } = new List<Ship>();

        //Places the ships into the cells, 
        public Board(int rows = 10, int cols = 10)
        {
            Rows = rows; Cols = cols;
            Cells = new Cell[rows, cols];
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    Cells[r, c] = new Cell { Row = r, Col = c };
        }

        //True when all cells have been hit

        public IEnumerable<Cell> AllCells()
        {
            for (int r = 0; r < Rows; r++) for (int c = 0; c < Cols; c++) yield return Cells[r, c];
        }
    }
}
