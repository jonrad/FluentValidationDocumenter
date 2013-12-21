namespace FluentValidationWikify.NodeTokenizers
{
    public class NotEmptyTokenizer : RequiredTokenizer
    {
        public override string MethodName
        {
            get { return "NotEmpty"; }
        }
    }
}