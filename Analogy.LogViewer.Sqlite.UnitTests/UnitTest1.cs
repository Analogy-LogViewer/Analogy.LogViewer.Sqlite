using Analogy.LogViewer.Sqlite.IAnalogy;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Analogy.LogViewer.Sqlite.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        private string Folder { get; } = Environment.CurrentDirectory;
        [TestMethod]
        public async Task ReadDBTest()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            string fileName = Path.Combine(Folder, "db", "example.db");
            MessageHandlerForTesting forTesting = new MessageHandlerForTesting();
            SqliteBroswerDataProvider reader = new SqliteBroswerDataProvider();
            LoggerFactory factory = new LoggerFactory();
            Microsoft.Extensions.Logging.ILogger logger = factory.CreateLogger("test");
            await reader.InitializeDataProvider(null);
            var messages = (await reader.Process(fileName, cts.Token, forTesting)).ToList();
            Assert.IsTrue(messages.Count == 389);
        }
    }
}