namespace FluentValidationWikify.NodeDocumenters
{
    public class NotNullDocumenter : RequiredDocumenter
    {
        public override string MethodName
        {
            get { return "NotNull"; }
        }
    }
}