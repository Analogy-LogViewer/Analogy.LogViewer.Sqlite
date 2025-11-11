using Analogy.Interfaces;
using Analogy.Interfaces.WinForms;
using Analogy.LogViewer.Template;
using System;
using System.Collections.Generic;

namespace Analogy.LogViewer.Sqlite.IAnalogy
{
    public class SqliteDataProviderFactory : DataProvidersFactoryWinForms
    {
        public override Guid FactoryId { get; set; } = PrimaryFactory.Id;
        public override string Title { get; set; } = "Analogy Sqlite db browser";

        public override IEnumerable<IAnalogyDataProviderWinForms> DataProviders { get; set; } = new List<IAnalogyDataProviderWinForms>
        {
            new SqliteBroswerDataProvider(),
        };
    }
}