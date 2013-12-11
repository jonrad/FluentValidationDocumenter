namespace FluentValidationWikify
{
    public class Doc
    {
        public Doc(string id)
            : this(id, null)
        {
        }

        public Doc(string id, object info)
        {
            Id = id;
            Info = info;
        }

        public string Id { get; private set; }

        public object Info { get; private set; }
    }
}