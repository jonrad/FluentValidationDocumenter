namespace FluentValidationWikify.NodeTokenizers
{
    public class NotEqualTokenizer : SingleArgumentTokenizer
    {
        public override string MethodName
        {
            get { return "NotEqual"; }
        }

        public override bool IsNewRule
        {
            get { return false; }
        }
    }
}