using System;
using System.Collections.Generic;
using Analogy.Interfaces;
using Analogy.LogViewer.Template;

namespace Analogy.LogViewer.Sqlite.IAnalogy
{
    public class SqliteDataProviderFactory : DataProvidersFactory
    {
        public override Guid FactoryId { get; set; } = PrimaryFactory.Id;
        public override string Title { get; set; } = "Analogy Sqlite db browser";

        public override IEnumerable<IAnalogyDataProvider> DataProviders { get; set; } = new List<IAnalogyDataProvider>
        {
            //add 2 "real time data providers"
            new SqliteBroswerDataProvider(),
        };
    }
}
