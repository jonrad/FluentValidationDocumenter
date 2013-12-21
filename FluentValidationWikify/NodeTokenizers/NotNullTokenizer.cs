namespace FluentValidationWikify.NodeTokenizers
{
    public class NotNullTokenizer : RequiredTokenizer
    {
        public override string MethodName
        {
            get { return "NotNull"; }
        }
    }
}