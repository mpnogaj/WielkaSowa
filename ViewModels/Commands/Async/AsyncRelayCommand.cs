﻿using Avalonia.Threading;
using System.Windows.Input;

namespace WielkaSowa.ViewModels.Commands.Async
{
    public class AsyncRelayCommand<T> : IAsyncCommand<T>
    {
        public event EventHandler? CanExecuteChanged;
        private readonly Func<T, Task> _execute;
        private readonly Func<bool>? _canExecute = null;
        private readonly DispatcherTimer _canExecuteChangedTimer;
        private bool _isExecuting = false;

        public AsyncRelayCommand(Func<T, Task> execute) : this(execute, () => true) { }

        public AsyncRelayCommand(Func<T, Task> execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
            _canExecuteChangedTimer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 0, 50),
            };
            _canExecuteChangedTimer.Tick += CanExecuteChangedTimer_Tick;
            _canExecuteChangedTimer.Start();
        }

        private void CanExecuteChangedTimer_Tick(object? sender, EventArgs e)
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(T parameter)
        {
            if (_isExecuting)
            {
                return false;
            }
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute();
        }

        public async Task ExecuteAsync(T parameter)
        {
            if (_execute == null) return;
            if (!_isExecuting)
            {
                _isExecuting = true;
                await _execute(parameter);
                _isExecuting = false;
            }
        }

        #region ICommand implementation

        bool ICommand.CanExecute(object? parameter)
        {
            if (parameter == null) return false;
            return CanExecute((T)parameter);
        }

        void ICommand.Execute(object? parameter)
        {
            if (parameter == null) return;
            Task.Run(() => ExecuteAsync((T)parameter));
        }

        #endregion
    }

    public class AsyncRelayCommand : IAsyncCommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly Func<Task> _execute;
        private readonly Func<bool>? _canExecute = null;
        private readonly DispatcherTimer _canExecuteChangedTimer;
        private bool _isExecuting = false;

        public AsyncRelayCommand(Func<Task> execute) : this(execute, () => true) { }

        public AsyncRelayCommand(Func<Task> execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
            _canExecuteChangedTimer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 0, 50),
            };
            _canExecuteChangedTimer.Tick += CanExecuteChangedTimer_Tick;
            _canExecuteChangedTimer.Start();
        }

        private void CanExecuteChangedTimer_Tick(object? sender, EventArgs e)
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute()
        {
            if (_isExecuting)
            {
                return false;
            }
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute();
        }

        public async Task ExecuteAsync()
        {
            if (!_isExecuting)
            {
                _isExecuting = true;
                await _execute();
                _isExecuting = false;
            }
        }

        #region ICommand implementation

        bool ICommand.CanExecute(object? parameter)
        {
            return CanExecute();
        }

        void ICommand.Execute(object? parameter)
        {
            Task.Run(() => ExecuteAsync());
        }

        #endregion
    }
}
