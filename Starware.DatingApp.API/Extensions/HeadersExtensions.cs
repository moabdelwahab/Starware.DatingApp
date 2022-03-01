using Starware.DatingApp.SharedKernal.Common;
using System.Text.Json;

namespace Starware.DatingApp.API.Extensions
{
    public static class HeadersExtensions
    {

        public static void AddPaginationHeader(this HttpResponse httpResponse , int totalPages,int totalCount, int pageSize, int currentPage)
        {
            var paginationHeader = new PaginationHeader(totalCount, totalPages, pageSize, currentPage);
            var serializingOptions = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var paginatinHeaderSerialized = JsonSerializer.Serialize(paginationHeader);
            httpResponse.Headers.Add("Pagination", paginatinHeaderSerialized);
            httpResponse.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}
