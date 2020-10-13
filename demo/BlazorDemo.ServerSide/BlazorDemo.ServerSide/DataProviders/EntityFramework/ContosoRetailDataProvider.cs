using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlazorDemo.Data;
using Microsoft.Extensions.Configuration;

namespace BlazorDemo.DataProviders {
    class ContosoRetailDataProvider : DataProviderBase, IContosoRetailDataProvider {
        private readonly ContosoRetailContext _context;
        readonly IConfiguration _configuration;

        public ContosoRetailDataProvider(ContosoRetailContext context, IConfiguration configuration) {
            _context = context;
            _configuration = configuration;
        }

        public Task<IEnumerable<Sale>> GetSalesAsync(CancellationToken ct = default) {
            return Task.FromResult((IEnumerable<Sale>)_context.Sales);
        }

        public override Task<IObservable<int>> GetLoadingStateAsync() {
            var connectionString = _configuration.GetConnectionString("PivotGridLargeDataConnectionString");
            if(string.IsNullOrEmpty(connectionString) || connectionString == "{Remote demo database connection string}")
                return Task.FromResult<IObservable<int>>(null);
            return base.GetLoadingStateAsync();
        }
    }
}
