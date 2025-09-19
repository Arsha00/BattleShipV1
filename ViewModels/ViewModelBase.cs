/*
 * Auhtor: Arian Sjöström
 * B.Sc Comuputer Science & Mobile IT
 * Malmö University & Luleå University Of Technology
 * Spec: AI Development / Software Engineering / GPD
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

/*
 * This class more or less runs the whole game
 * PCEH commands runs all user interactions and manages to check wether an command is valid or false
 */

namespace BattleShipGame.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void Raise([CallerMemberName] string? name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        public bool Set<T>(ref T field, T value, [CallerMemberName] string? name = null)
        {
            if (Equals(field, value)) return false;
            field = value; Raise(name); return true;
        }
    }
}

