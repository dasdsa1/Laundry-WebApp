using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Infrastructure
{
    public interface IViewRenderer
    {
        string Render<TModel>(string name, TModel model);

    }
}
