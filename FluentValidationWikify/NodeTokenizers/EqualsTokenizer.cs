namespace FluentValidationWikify.NodeTokenizers
{
    public class EqualsTokenizer : SingleArgumentTokenizer
    {
        public override string MethodName
        {
            get { return "Equals"; }
        }

        public override bool IsNewRule
        {
            get { return false; }
        }
    }
}