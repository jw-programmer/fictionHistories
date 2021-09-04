using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Src.Queries;

namespace Src.Extentions
{
    public static class HttpContextExtentions
    {
        public async static Task InsertPageMetadata<T>(this HttpContext context, IQueryable<T> query, PaginationQuery pageQuery)
        {
            if(context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            int qtdeTotal = await query.CountAsync();
            
            PaginationData metadata = new(
                qtdeTotal, pageQuery.PageNumber, pageQuery.PageSize
            );
            
            context.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
        }
    }
}