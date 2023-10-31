using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trans.Core.Repository;
using Trans.Infrascture.EF_DB;

namespace Trans.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;
        private readonly TransContext _transContext;

        public DataInitializer(IUserService userService, ILogger logger, TransContext transContext)
        {
            _userService = userService;
            _logger = logger;
            _transContext = transContext;
        }

        //Add users to database if database is empty
        public async Task SeedAsync()
        {
            _logger.Information("Data start Initialize");
            if (_transContext.Users.Any())
            {
                _logger.Information("Data already exist in database.");
                return;
            }; 
            var tasks = new List<Task>();
            for (int i = 2; i < 11; i++)
            {
                var userId = Guid.NewGuid();
                tasks.Add(_userService.RegisterAsync(userId, $"user{i}@email.com", "secret", $"user{i}", "Nowak", "user"));
            }
            await Task.WhenAll(tasks);

            _logger.Information("Data initialized");
        }
    }
}
