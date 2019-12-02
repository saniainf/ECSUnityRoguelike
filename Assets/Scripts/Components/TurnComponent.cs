using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Client
{
    sealed class TurnComponent : IEcsAutoReset
    {
        public int Queue = 0;
        public bool ReturnInput = false;
        public IInputCommand InputCommand = null;
        public bool SkipTurn = false;

        void IEcsAutoReset.Reset()
        {
            ReturnInput = false;
            InputCommand = null;
            SkipTurn = false;
        }
    }
}