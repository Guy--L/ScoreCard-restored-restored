using ScoreCard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoreCard.Hubs
{
    public class URStack
    {
        private Stack<ICommand> _Undo;
        private Stack<ICommand> _Redo;

        public int UndoCount
        {
            get
            {
                return _Undo.Count;
            }
        }
        public int RedoCount
        {
            get
            {
                return _Redo.Count;
            }
        }

        public URStack()
        {
            Reset();
        }
        public void Reset()
        {
            _Undo = new Stack<ICommand>();
            _Redo = new Stack<ICommand>();
        }

        public void Do(ICommand cmd)
        {
            _Undo.Push(cmd);
            _Redo.Clear(); // Once we issue a new command, the redo stack clears
        }

        public ICommand Undo()
        {
            if (_Undo.Count == 0)
                return null;
            ICommand cmd = _Undo.Pop();
            cmd.Undo();
            _Redo.Push(cmd);
            return cmd;
        }
        public ICommand Redo()
        {
            if (_Redo.Count == 0)
                return null;
            ICommand cmd = _Redo.Pop();
            cmd.Do();
            _Undo.Push(cmd);
            return cmd;
        }
    }
}