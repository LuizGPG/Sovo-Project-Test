﻿namespace SovosProjectTest.Application
{
    public class PagedResponse<T>
    {
        public IList<T> Data { get; set; } = new List<T>();
        public int TotalItems { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    }
}
