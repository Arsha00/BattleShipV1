/*
 * Auhtor: Arian Sjöström
 * B.Sc Comuputer Science & Mobile IT
 * Malmö University & Luleå University Of Technology
 * Spec: AI Development / Software Engineering / GPD
 */

using BattleShipGame.Helpers;
using BattleShipGame.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BattleShipGame.ViewModels
{

    /*
     * States related to the model
     */
    public class CellViewModel : ViewModelBase
    {
        private readonly Cell _model;
        public int Row => _model.Row;
        public int Col => _model.Col;
        public string Id => _model.id;


        /*
         * Raise inherted from ViewModelBase, triggers an UI update
         */
        public CellState State
        {
            get => _model.State;
            set { _model.State = value; Raise(nameof(State)); Raise(nameof(Display)); }
        }


        /*
         * Display specifications
         * Ship and Empty displayed only for system test purposes
         * X and O marks an HIT or MISS
         */

        public string Display => _model.State switch
        {
            CellState.Empty => "~",
            CellState.Ship => "S",
            CellState.Miss => "O",
            CellState.Hit => "X"
        };


        public ICommand AttackCommand { get; }


        public CellViewModel(Cell model, Action<CellViewModel> attackAction)
        {
            _model = model;
            AttackCommand = new RelayCommand(_ => attackAction(this));
        }
    }
}

