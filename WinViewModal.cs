using ReactiveUI;
using System;
using System.Reactive;

namespace MyGameApp
{
    public class WinModalViewModel : ReactiveObject
    {
        public ReactiveCommand<Unit, Unit> NewGameCommand { get; }

        public WinModalViewModel(Action onNewGame)
        {
            NewGameCommand = ReactiveCommand.Create(onNewGame);
        }
    }
}