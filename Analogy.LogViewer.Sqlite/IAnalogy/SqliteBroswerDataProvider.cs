using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Analogy.Interfaces;
using Analogy.LogViewer.Sqlite.Properties;
using Analogy.LogViewer.Template.Managers;
using Microsoft.Data.Sqlite;

namespace Analogy.LogViewer.Sqlite.IAnalogy
{
    public class SqliteBroswerDataProvider : Template.OfflineDataProvider
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

        
        public override Task InitializeDataProvider(IAnalogyLogger logger)
        {
            //do some initialization for this provider
            return base.InitializeDataProvider(logger);
        }

        public override void MessageOpened(AnalogyLogMessage message)
        {
            //nop
        }

        public override async Task<IEnumerable<AnalogyLogMessage>> Process(string fileName, CancellationToken token, ILogMessageCreatedHandler messagesHandler)
        {
            using (var conn = new SqliteConnection($"Data Source={fileName};Mode=readonly"))
            {
                await conn.OpenAsync(token);
                // executes query that select names of all tables in master table of the database
                string query = "SELECT name FROM sqlite_master " +
                               "WHERE type = 'table'" +
                               "ORDER BY 1";

                try
                {
                    DataTable dt = new DataTable();
                    using (SqliteCommand cmd = new SqliteCommand(query, conn))
                    {
                        using (SqliteDataReader rdr = await cmd.ExecuteReaderAsync(token))
                        {
                            dt.Load(rdr);
                        }

                    }

                    DataTable dtData = new DataTable();
                    foreach (DataRow row in dt.Rows)
                    {
                        string dataQuery = "SELECT * FROM " + row.ItemArray[0];
                        using SqliteCommand cmd = new SqliteCommand(dataQuery, conn);
                        using (SqliteDataReader reader = await cmd.ExecuteReaderAsync(token))
                        {
                            DataTable schemaTable = reader.GetSchemaTable();

                            foreach (DataRow rowName in schemaTable.Rows)
                            {
                                dtData.Columns.Add(rowName["ColumnName"].ToString(), typeof(object));
                            }

                            dtData.Load(reader);
                        }
                    }
                    var messages = new List<AnalogyLogMessage>();
                    foreach (DataRow entry in dtData.Rows)
                    {
                        AnalogyLogMessage m = new AnalogyLogMessage();
                        m.Source = dtData.TableName;
                        m.AdditionalInformation = new Dictionary<string, string>();
                        for (var i = 0; i < entry.ItemArray.Length; i++)
                        {
                            var key = dtData.Columns[i].ColumnName;
                            var itm = entry.ItemArray[i];
                            m.AdditionalInformation.Add(key, itm.ToString());
                        }
                        messages.Add(m);
                        messagesHandler.AppendMessage(m, fileName);

                    }

                    return messages;
                }

                catch (Exception e)
                {
                    LogManager.Instance.LogException($"error:{e.Message}", e, "");
                }

                return new List<AnalogyLogMessage>(0);
            }
        }
    }
}
