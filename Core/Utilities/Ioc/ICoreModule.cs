using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Core.Utilities.Ioc
{
    public interface ICoreModule
    {
        void Load(IServiceCollection collection);
    }
}

