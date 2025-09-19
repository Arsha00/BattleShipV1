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
using System.Windows.Input;

/*
 * This whole class specifies all commands being made throughout the whole game.
 * All user interactions are placed into this particular class for easier orientation.
 * Checks wether or not a specific command has been made.
 * Checks if command is executed or can be executed
 * Throws null exception if not --> for easier systemtestning instead of program "crash"
 * EventHandler to verify the executed commands
 */

namespace BattleShipGame.Helpers
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool>? _canExecute;
        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }


        public event EventHandler? CanExecuteChanged;
        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;
        public void Execute(object? parameter) => _execute(parameter);
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}

