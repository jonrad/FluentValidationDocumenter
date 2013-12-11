namespace FluentValidationWikify
{
    public class Token
    {
        public Token(string id)
            : this(id, null)
        {
        }

        public Token(string id, object info)
        {
            Id = id;
            Info = info;
        }

        public string Id { get; private set; }

        public object Info { get; private set; }
    }
}