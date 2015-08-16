using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreCard.Models
{
    public interface ICommand
    {
        string reflect { get; }
        object[] parameters { get; }
        void Do();
        void Undo();
    }
}
