using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models;
using System.Diagnostics;
using Dapper;
using Npgsql;

namespace OnlineStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("PONG");
        }

        [HttpGet("get")]
        public IActionResult GetGoods()
        {
            var goods = new List<Goods>() {
                new Goods(Guid.NewGuid(), "HP Omen", "Very powerful computer", 100000),
                new Goods(Guid.NewGuid(), "Apple Iphone 13", "Very good smartphone", 27000),
                new Goods(Guid.NewGuid(), "Razer Basilisk", "Very good computer mouse", 1500)
            };

            return Ok(goods);
        }

        [HttpGet("createDatabase")]
        public async Task<IActionResult> CreateDatabase()
        {
            var connectionString = "Server=postgresql.default.svc.cluster.local;Port=5432;User Id=postgres;Password=postgres;";
            using var connection = new NpgsqlConnection(connectionString);
            var databaseName = "OnlineStore";
            var sqlDbCount = $"SELECT COUNT(*) FROM pg_database WHERE datname = '{databaseName.ToLower()}';";
            var dbCount = await connection.ExecuteScalarAsync<int>(sqlDbCount);
            if (dbCount == 0)
            {
                var sqlCreate = $"CREATE DATABASE OnlineStore";
                await connection.ExecuteAsync(sqlCreate);
            }
            return Ok();
        }
    }
}
