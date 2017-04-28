namespace Sitecore.Support.Data.Archiving
{
    using Sitecore.Data;
    using Sitecore.Data.DataProviders.Sql;
    using Sitecore.Security.Accounts;
    using System.Collections.Generic;

    public class SqlArchive : Sitecore.Data.Archiving.SqlArchive
    {
        public SqlArchive(string name, Database database) : base(name, database)
        {
        }

        protected override int GetEntryCount(User user)
        {
            List<string> list = new List<string>(new string[]
            {
                "archiveName",
                base.Name
            });
            string text = "SELECT COUNT(*) FROM {0}Archive{1}\r\n        WHERE {0}ArchiveName{1} = {2}archiveName{3}";
            #region Changed code
            if (user != null && !user.IsAdministrator) 
            #endregion
            {
                text += " AND {0}ArchivedBy{1} = {2}archivedBy{3}";
                list.AddRange(new string[]
                {
                    "archivedBy",
                    user.Name
                });
            }
            int result;
            using (DataProviderReader dataProviderReader = base.Api.CreateReader(text, list.ToArray()))
            {
                result = ((!dataProviderReader.Read()) ? 0 : base.Api.GetInt(0, dataProviderReader));
            }
            return result;
        }
    }
}
