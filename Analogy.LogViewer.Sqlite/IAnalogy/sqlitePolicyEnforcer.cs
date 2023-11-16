namespace Analogy.LogViewer.Sqlite.IAnalogy
{
    public class sqlitePolicyEnforcer : Template.AnalogyPolicyEnforcer
    {
        public override bool DisableUpdates { get; set; }
    }
}