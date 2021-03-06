﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MovieSearch.Common
{
    /// <summary>
    /// Ein Befehl mit dem einzigen Zweck, seine Funktionalität zu vermitteln 
    /// zu anderen Objekten durch Aufrufen von Delegaten. 
    /// Der Standardrückgabewert für die CanExecute-Methode ist 'true'.
    /// <see cref="RaiseCanExecuteChanged"/> muss jedes mal aufgerufen werden, wenn
    /// <see cref="CanExecute"/> muss einen anderen Wert zurückgeben.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<bool> _canExecute;

        /// <summary>
        /// Wird ausgelöst, wenn RaiseCanExecuteChanged aufgerufen wird.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Erstellt einen neuen Befehl, der immer ausgeführt werden kann.
        /// </summary>
        /// <param name="execute">Die Ausführungslogik.</param>
        public DelegateCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Erstellt einen neuen Befehl.
        /// </summary>
        /// <param name="execute">Die Ausführungslogik.</param>
        /// <param name="canExecute">Die Logik des Ausführungsstatus.</param>
        public DelegateCommand(Action<object> execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Legt fest, ob dieser <see cref="DelegateCommand"/> im aktuellen Zustand ausgeführt werden kann.
        /// </summary>
        /// <param name="parameter">
        /// Die vom Befehl verwendeten Daten. Wenn für den Befehl keine Datenübergabe erforderlich ist, kann dieses Objekt auf NULL festgelegt werden.
        /// </param>
        /// <returns>True, wenn dieser Befehl ausgeführt werden kann, andernfalls False.</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute();
        }

        /// <summary>
        /// Führt den <see cref="DelegateCommand"/> im aktuellen Befehlsziel aus.
        /// </summary>
        /// <param name="parameter">
        /// Die vom Befehl verwendeten Daten. Wenn für den Befehl keine Datenübergabe erforderlich ist, kann dieses Objekt auf NULL festgelegt werden.
        /// </param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        /// <summary>
        /// Zum Aufrufen des <see cref="CanExecuteChanged"/>-Ereignisses verwendete Methode
        /// um anzugeben, dass der Rückgabewert von <see cref="CanExecute"/>
        /// Die Methode hat sich geändert.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}