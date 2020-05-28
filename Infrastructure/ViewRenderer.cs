using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RouteData = Microsoft.AspNetCore.Routing.RouteData;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace WebApplication3.Infrastructure
{
    public class ViewRenderer : IViewRenderer
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly ITempDataProvider _tempDataProvider;

        private readonly IRazorViewEngine _viewEngine;

        public ViewRenderer(IRazorViewEngine viewEngine, ITempDataProvider tempDataProvider, IServiceProvider serviceProvider)
        {
            _viewEngine = viewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
        }
        public string Render<TModel>(string name, TModel model)
        {
            var actionContext = GetActionContext();

            var viewEngineResult = _viewEngine.FindView(actionContext, name, false);
            if (viewEngineResult.Success == false)
            {
                throw new InvalidOperationException($"Couldn't find view {name}");
            }
            var view = viewEngineResult.View;

            using (var output = new StringWriter())
            {
                WriteToOutPut(model, actionContext, view, output);
                return output.ToString();
            }
        }

        private ActionContext GetActionContext()
        {
            var httpContext = new DefaultHttpContext
            {
                RequestServices = _serviceProvider
            };
            return new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
        }


        private void WriteToOutPut<TModel>(TModel model, ActionContext actionContext, IView view, TextWriter output)
        {
            var viewContext = new ViewContext(
                actionContext,
                view,
                new ViewDataDictionary<TModel>(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = model
                },
                new TempDataDictionary(
                    actionContext.HttpContext,
                    _tempDataProvider
                    ),
                output,
                new HtmlHelperOptions()
                );
            view.RenderAsync(viewContext).GetAwaiter().GetResult();

        }
    }
}
    