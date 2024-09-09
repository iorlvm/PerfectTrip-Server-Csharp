using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Application.DTOs
{
    public class Result
    {
        public bool Success { get; set; }
        public string? ErrorMsg { get; set; }
        public object? Data { get; set; }
        public ulong? Total {  get; set; }

        public Result(bool success, object? data = null, string? errorMsg = null, ulong? total = null)
        {
            Success = success;
            ErrorMsg = errorMsg;
            Data = data;
            Total = total;
        }

        public static Result Ok()
        {
            return new Result(success: true);
        }

        public static Result Ok(object data)
        {
            return new Result(success: true, data: data);
        }

        public static Result Ok(object data, ulong total)
        {
            return new Result(success: true, data: data, total: total);
        }

        public static Result Fail(string errorMsg)
        {
            return new Result(success: false, errorMsg: errorMsg);
        }
    }
}
