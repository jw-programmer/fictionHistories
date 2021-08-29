using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Src.Extentions
{
    public static class HttpContextExtentions
    {
        public async static Task InsertPageMetadata<T>(this HttpContext context, IQueryable<T> query, int qtdeTotalToShow)
        {
            if(context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            double qtdeTotal = await query.CountAsync();
            double pageTotal = Math.Ceiling(qtdeTotal/qtdeTotalToShow);
            
            context.Response.Headers.Add("count", qtdeTotal.ToString());
            context.Response.Headers.Add("numPages", pageTotal.ToString());
        }
    }
}