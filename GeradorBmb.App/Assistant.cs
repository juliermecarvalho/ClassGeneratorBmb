namespace GeradorBmb.App
{
    public class Assistant
    {
        //public string Name { get; private set; }
        //public string Abbreviations { get; private set; }
        //public string UserId { get; private set; }


        const string abre = "{";
        const string fecha = "}";

        public string GerarPropertys(IDictionary<string, string> propertys, bool setPrivate)
        {
            string str = "";
            foreach (var property in propertys)
            {
                var strPrivate = setPrivate ? "private set;" : "set;";
                str += $"    public {property.Value} {property.Key} {abre} get; {strPrivate} {fecha} {Environment.NewLine}";
            }

            return str;
        }


        public string GerarConstrutor(string classeName, IDictionary<string, string> propertys)
        {
            string str = "";

            str += $"   public {classeName}() {abre}{fecha}{Environment.NewLine}{Environment.NewLine}";


            string param = "";
            var virgula = "";

            foreach (var property in propertys)
            {
                param += $"{virgula}{property.Value} {FirstCharToUpper(property.Key)}";
                virgula = ", ";
            }


            string atribuicao = "";
            foreach (var property in propertys)
            {
                atribuicao += $"        {property.Key} = {FirstCharToUpper(property.Key)};{Environment.NewLine}";

            }

            str += $@"
    public {classeName}(int id, bool isActive, {param}) 
    {abre}
        Id = id;
        IsActive = isActive;
{atribuicao}
    {fecha}
                
    public {classeName}({param}) 
    {abre}
{atribuicao}
    {fecha}
                
    public void Change{classeName}({param}) 
    {abre}
{atribuicao}
    {fecha}{Environment.NewLine}";



            return str;

        }

        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("Insira uma palavra diferente de nula ou vazia");
            return input.First().ToString().ToLower() + input.Substring(1);
        }
    }
}
