using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    public  class BusinessRules
    {
        public static IResult Run(params IResult[] logics)
        //params key word allows us to use as many parameters
        // as we want with the same type 
        {
            foreach (var logic in logics)
            {
                if (!logic.isSuccess)
                {
                    return logic;
                }
            }
            return null;
        }

    }
}
