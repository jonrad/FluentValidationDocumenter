using System.Text.RegularExpressions;

namespace FluentValidationWikify.Documenters
{
    public class Friendly : IFriendly
    {
        private readonly Regex casingRegex;

        public Friendly()
        {
            casingRegex = new Regex("([a-z])([A-Z])");
        }

        public string Get(string input)
        {
            return casingRegex.Replace(input, "$1 $2").ToLower().Replace('_', ' ');
        }
    }
}