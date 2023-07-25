using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.BuildingBlocks.Application
{
    public  class InvalidCommandException:Exception
    {
       public string[] Errors { get; }
        public InvalidCommandException( string[] erros) { Errors = erros; }
    }
}
