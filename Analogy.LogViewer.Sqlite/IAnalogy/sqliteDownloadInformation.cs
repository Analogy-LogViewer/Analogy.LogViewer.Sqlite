using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Versioning;

namespace Analogy.LogViewer.Sqlite.IAnalogy
{
    public class sqliteDownloadInformation : Template.AnalogyDownloadInformation
    {
        public override Guid FactoryId { get; set; } = PrimaryFactory.Id;
        public override string Name { get; set; } = "Analogy Sqlite db browser";
        public override bool IsUpdateAvailable { get; set; }
        public override string? DownloadURL { get; set; }
        public override string? ChangeLogURL { get; set; }

        private string? _installedVersionNumber;
        public override string InstalledVersionNumber
        {
            get
            {
                if (_installedVersionNumber != null)
                {
                    return _installedVersionNumber;
                }
                Assembly assembly = Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                _installedVersionNumber = fvi.FileVersion;
                return _installedVersionNumber;
            }
        }

        public override string? LatestVersionNumber { get; set; }
        public override TargetFrameworkAttribute CurrentFrameworkAttribute { get; set; } = (TargetFrameworkAttribute)Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(TargetFrameworkAttribute));
        protected override string RepositoryURL { get; set; } = "https://api.github.com/repos/Analogy-LogViewer/Analogy.LogViewer.Sqlite";
    }
}