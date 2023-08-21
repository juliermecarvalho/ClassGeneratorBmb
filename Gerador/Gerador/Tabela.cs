


namespace Gerador
{
    public class Table
    {
        public string Name { get; set; }
        public string NameClass { get => GetNameClass(Name); }
        public IList<String> Collection { get; set; } = new List<string>();

        public string GetNameClass(string name)
        {
            if (name.ToLower().EndsWith("s"))
            {
                name = name.Substring(0, name.ToLower().LastIndexOf("s"));
            }
            return name;
        }

        public IList<Filde> Fildes { get; set; } = new List<Filde>();
    }

    public class Filde
    {

        public string Collum { get; set; }
        public string CollumForemKey { get => RemoverId(this.Collum); }

        public bool IsNull { get; set; }
        public string Type { get; set; }
        public string TypeCshap { get => GetCSharpType(this.Type); }
        public int? MaximumCharacters { get; set; }
        public bool IsForenKey { get; set; } = false;
        public string TableForemKey { get; set; }
        public string TableClassForemKey { get => GetNameClass(TableForemKey); }

        private string GetNameClass(string name)
        {
            if (name.ToLower().EndsWith("s"))
            {
                name = name.Substring(0, name.ToLower().LastIndexOf("s"));
            }
            return name;
        }

        private string RemoverId(string collum)
        {
            if (IsForenKey && collum.ToLower().EndsWith("_id"))
            {
                collum = collum.Substring(0, collum.ToLower().LastIndexOf("_id"));
            }
            return collum;
        }
        public string GetCSharpType(string sqlServerType)
        {
            switch (sqlServerType.ToLower())
            {
                case "bigint":
                    return "long";
                case "binary":
                case "image":
                case "timestamp":
                case "varbinary":
                    return "byte[]";
                case "bit":
                    return "bool";
                case "char":
                case "nchar":
                case "nvarchar":
                case "text":
                case "ntext":
                case "varchar":
                case "xml":
                    return "string";
                case "date":
                case "datetime":
                case "datetime2":
                case "smalldatetime":
                case "time":
                    return "DateTime";
                case "decimal":
                case "money":
                case "numeric":
                case "smallmoney":
                    return "decimal";
                case "float":
                    return "double";
                case "int":
                    return "int";
                case "real":
                    return "float";
                case "smallint":
                    return "short";
                case "tinyint":
                    return "byte";
                case "uniqueidentifier":
                    return "Guid";
                default:
                    return "object";
            }
        }

    }
}
