﻿using Microsoft.AspNetCore.Http;
using ProjectManagement.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjectManagement.Common.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response, int currentPage, int itemsPerPage,
            int totalItems, int totalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);
            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}