using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Analogy.Interfaces;
using Analogy.LogViewer.Sqlite.Properties;

namespace Analogy.LogViewer.Sqlite.IAnalogy
{
    public sealed class SqliteBroswerDataProvider : Template.OfflineDataProvider
    {
        public override Guid Id { get; set; } = new Guid("25b2b926-47f8-4f13-8db8-0803f8829eba");
        public override Image? LargeImage { get; set; } = Resources.Analogy_image_32x32;
        public override Image? SmallImage { get; set; } = Resources.Analogy_image_16x16;

        public override string? OptionalTitle { get; set; } = "Sqlite db browser";
        public override string FileOpenDialogFilters { get; set; } = "sqlite db file (*.db)|*.db";
        public override IEnumerable<string> SupportFormats { get; set; } = new[] { "*.db" };
        public override string? InitialFolderFullPath { get; set; } = Environment.CurrentDirectory;
        public override IEnumerable<(string originalHeader, string replacementHeader)> GetReplacementHeaders()
            => Array.Empty<(string, string)>();

        public override (Color backgroundColor, Color foregroundColor) GetColorForMessage(IAnalogyLogMessage logMessage)
            => (Color.Empty, Color.Empty);
        public SqliteBroswerDataProvider()
        {
        }

        public override  Task InitializeDataProvider(IAnalogyLogger logger)
        {
            //do some initialization for this provider
            return base.InitializeDataProvider(logger);
        }

        public override  void MessageOpened(AnalogyLogMessage message)
        {
            //nop
        }

        public override Task<IEnumerable<AnalogyLogMessage>> Process(string fileName, CancellationToken token, ILogMessageCreatedHandler messagesHandler)
        {
            return Task.FromResult(new List<AnalogyLogMessage>(0).AsEnumerable());
        }

        protected override List<FileInfo> GetSupportedFilesInternal(DirectoryInfo dirInfo, bool recursive)
        {
            return base.GetSupportedFilesInternal(dirInfo, recursive);
        }

        public override Task SaveAsync(List<AnalogyLogMessage> messages, string fileName)
        {
            return Task.CompletedTask;
        }

        public override bool CanOpenFile(string fileName)
        {
            return false;
        }

        public override bool CanOpenAllFiles(IEnumerable<string> fileNames)
        {
            return fileNames.All(CanOpenFile);
        }


    }
}
