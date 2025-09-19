/*
 * Auhtor: Arian Sjöström
 * B.Sc Comuputer Science & Mobile IT
 * Malmö University & Luleå University Of Technology
 * Spec: AI Development / Software Engineering / GPD
 */

using BattleShipGame.Helpers;
using BattleShipGame.Models;
using BattleShipGame.Services;
using BattleShipGame.ViewModels;
using Intercom.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BattleshipGame.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly GameService _gameService = new();
        public Board PlayerBoardModel { get; }
        public Board EnemyBoardModel { get; }
        public BoardViewModel PlayerBoard { get; }
        public BoardViewModel EnemyBoard { get; }


        private string _status = "Battleship V1 by: Arian Shaafi";
        public string Status { get => _status; set => Set(ref _status, value); }

        public ICommand NewGameCommand { get; }
        public ICommand AttackCommand { get; }

        /*
         * NewGameCommand runs the new game command in GameService
         * AttackCommand runs the atack on the enemy board
         * StartNewGame when all ships on either board has been hit
         */
        public MainViewModel()
        {
            PlayerBoardModel = new Board(10, 10);
            EnemyBoardModel = new Board(10, 10);
            PlayerBoard = new BoardViewModel(PlayerBoardModel, _gameService, true);
            EnemyBoard = new BoardViewModel(EnemyBoardModel, _gameService, false);


            NewGameCommand = new RelayCommand(_ => StartNewGame());
            AttackCommand = new RelayCommand(param =>
            {
                if (param is CellViewModel cvm)
                {
                    bool sunk;
                    var hit = _gameService.Attack(EnemyBoardModel, cvm.Row, cvm.Col, out sunk);
                    EnemyBoard.RefreshCells();
                    if (hit)
                    {
                        Status = sunk ? "Enemy Ship Sunk! Great Work!" : "HIT!";
                        if (_gameService.AllShipsSunk(EnemyBoardModel)) Status = "Congratulations! YOU WON!";
                    }
                    else
                    {
                        Status = "Miss...";
                        
                        EnemyTurn();
                    }
                }
            }, param => param is CellViewModel cvm && cvm.State != CellState.Hit && cvm.State != CellState.Miss);


            StartNewGame();
        }


        // Initizates enemyturn after player commits an MISS
        // If all player ships has been HIT and SUNK, Game Over message is sent
        private void EnemyTurn()
        {
            
            var rnd = new System.Random();
            var choices = PlayerBoardModel.AllCells().Where(c => c.State != CellState.Hit && c.State != CellState.Miss).ToArray();
            if (!choices.Any()) return;
            var pick = choices[rnd.Next(choices.Length)];
            bool sunk;
            _gameService.Attack(PlayerBoardModel, pick.Row, pick.Col, out sunk);
            PlayerBoard.RefreshCells();
            if (_gameService.AllShipsSunk(PlayerBoardModel)) Status = "Oh Snap! - YOU LOST!";
        }


        public void StartNewGame()
        {
            // Boards Reset
            for (int r = 0; r < PlayerBoardModel.Rows; r++)
                for (int c = 0; c < PlayerBoardModel.Cols; c++)
                    PlayerBoardModel.Cells[r, c].State = CellState.Empty;
            for (int r = 0; r < EnemyBoardModel.Rows; r++)
                for (int c = 0; c < EnemyBoardModel.Cols; c++)
                    EnemyBoardModel.Cells[r, c].State = CellState.Empty;


            // Ships are placed on the board
            var sizes = new[] { 5, 4, 3, 3, 2 };
            _gameService.PlaceShipsRandomly(PlayerBoardModel, sizes);
            _gameService.PlaceShipsRandomly(EnemyBoardModel, sizes);


            PlayerBoard.RefreshCells();
            EnemyBoard.RefreshCells();
            Status = "New Game Started. Your Turn.";

        }
    }
}


