/*
 * Auhtor: Arian Shaafi
 * B.Sc Comuputer Science & Mobile IT
 * Alma Mater: Malmö University & Luleå University Of Technology
 * Spec: AI Development / Software Engineering / GPD
 */

using BattleShipGame.Models;
using BattleShipGame.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShipGame.Services;


namespace BattleShipGame.ViewModels
{
    public class BoardViewModel : ViewModelBase
    {
        private readonly Board _board;
        private readonly GameService _service;
        public ObservableCollection<CellViewModel> Cells { get; } = new();
        public int Rows => _board.Rows;
        public int Cols => _board.Cols;


        public BoardViewModel(Board board, GameService service, bool isPlayerBoard = false)
        {
            _board = board; _service = service;
            foreach (var cell in board.AllCells())
            {
                Cells.Add(new CellViewModel(cell, OnCellAttacked));
            }
        }


        private void OnCellAttacked(CellViewModel cellVm)
        {
            /*
             * MainViewModel orchestrates whos turn it is to attack.
             * We´ll raise an event by forwarding the attack call, and simply store the attack state in here
             */
        }

        // Cellstates refreshed
        public void RefreshCells()
        {
            foreach (var c in Cells) c.Raise(nameof(CellViewModel.State));
        }
    }
}
