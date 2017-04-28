namespace Sitecore.Support.Data.Archiving
{
    using Sitecore.Data;
    using Sitecore.Data.Archiving;
    using Sitecore.Xml;
    using System.Xml;

    public class SqlArchiveProvider : Sitecore.Data.Archiving.SqlArchiveProvider
    {
        protected override Archive GetArchive(XmlNode configNode, Database database)
        {
            string attribute = XmlUtil.GetAttribute("name", configNode);
            Archive result;
            if (string.IsNullOrEmpty(attribute))
            {
                result = null;
            }
            else
            {
                result = new Sitecore.Support.Data.Archiving.SqlArchive(attribute, database);
            }
            return result;
        }
    }
}
