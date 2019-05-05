using System.Text.RegularExpressions;

namespace XnbConvert
{
    internal class XnbUtils
    {
        public static string GetSimplifiedName(string fullName, out int arrayCount)
        {
            var simplifiedName = fullName.Split('`', ',')[0];

            var genericPartRegex = new Regex(@"`[0-9]+(?:[\[])(?:[^\[\]]|(?<Open>[\[])|(?<-Open>[\]]))*\]");
            var genericPartMatch = genericPartRegex.Match(fullName);

            var withoutGeneric = simplifiedName;
            if (genericPartMatch.Success)
                withoutGeneric = fullName.Replace(genericPartMatch.Value, string.Empty).Split(',')[0];

            var regex = new Regex(@"((\[\])+)$");
            var matches = regex.Match(withoutGeneric);
            arrayCount = matches.Groups[2].Captures.Count;

            return regex.Replace(simplifiedName, string.Empty);
        }

        
    }
}