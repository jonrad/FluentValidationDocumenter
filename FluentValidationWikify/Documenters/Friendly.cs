using System.Text.RegularExpressions;

namespace FluentValidationWikify.Documenters
{
    public class Friendly : IFriendly
    {
        private readonly Regex casingRegex;

        private readonly Regex underscoreRegex;

        public Friendly()
        {
            casingRegex = new Regex("([a-z])([A-Z])");
            underscoreRegex = new Regex("([a-z])_([a-z])");
        }

        public string Get(string input)
        {
            return underscoreRegex.Replace(casingRegex.Replace(input, "$1 $2").ToLower(), "$1 $2");
        }
    }
}