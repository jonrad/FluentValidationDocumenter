namespace FluentValidationWikify.NodeTokenizers
{
    public class GreaterThanOrEqualToTokenizer : SingleArgumentTokenizer
    {
        public override string MethodName
        {
            get { return "GreaterThanOrEqualTo"; }
        }

        public override bool IsNewRule
        {
            get { return false; }
        }
    }
}