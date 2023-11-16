using Analogy.Interfaces;
using Analogy.LogViewer.Sqlite.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Analogy.LogViewer.Sqlite.IAnalogy
{
    public class PrimaryFactory : Analogy.LogViewer.Template.PrimaryFactory
    {
        internal static readonly Guid Id = new Guid("092e0375-a44a-4067-9a50-b2cbbeaf9ce8");
        public override Guid FactoryId { get; set; } = Id;

        public override string Title { get; set; } = "Analogy Sqlite";
        public override Image? SmallImage { get; set; } = Resources.Analogy_image_16x16;
        public override Image? LargeImage { get; set; } = Resources.Analogy_image_32x32;

        public override IEnumerable<IAnalogyChangeLog> ChangeLog { get; set; } = new List<AnalogyChangeLog>
        {
            new AnalogyChangeLog("Initial version", AnalogChangeLogType.None, "Lior Banai", new DateTime(2023, 03, 03), ""),
        };
        public override IEnumerable<string> Contributors { get; set; } = new List<string> { "Lior Banai" };
        public override string About { get; set; } = "Analogy Sqlite Data Provider";
    }
}