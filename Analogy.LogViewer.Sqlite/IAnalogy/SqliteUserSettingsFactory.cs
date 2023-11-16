using Analogy.Interfaces;
using Analogy.Interfaces.DataTypes;
using Analogy.LogViewer.Sqlite.Properties;
using Analogy.LogViewer.Template;
using Microsoft.Extensions.Logging;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analogy.LogViewer.Sqlite.IAnalogy
{
    public class SqliteUserSettingsFactory : TemplateUserSettingsFactory
    {
        public override Guid FactoryId { get; set; } = PrimaryFactory.Id;
        public override Guid Id { get; set; } = new Guid("113ebf43-3290-4fe3-82bc-5ccc79b67706");
        public override UserControl DataProviderSettings { get; set; }
        public override string Title { get; set; } = "Sqlite db User Settings";
        public override Image? SmallImage { get; set; } = Resources.Analogy_image_16x16;
        public override Image? LargeImage { get; set; } = Resources.Analogy_image_32x32;

        public override AnalogyToolTip? ToolTip { get; set; } = new AnalogyToolTip("Sqlite dB", "",
            "", Resources.Analogy_image_16x16, Resources.Analogy_image_32x32);

        public override void CreateUserControl(ILogger logger)
        {
            DataProviderSettings = new UserControl();
        }

        public override Task SaveSettingsAsync()
        {
            return Task.CompletedTask;
        }
    }
}