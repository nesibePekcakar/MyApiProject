using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    //For Void type return response
    public interface IResult
    {
        bool isSuccess { get; }
        string? Message { get; }

    }
}
