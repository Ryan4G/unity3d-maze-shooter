using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IGameManager
{
    ManagerStatus status { get; }

    void Startup();
}
