﻿namespace FluentValidationWikify.NodeTokenizers
{
    public class GreaterThanTokenizer : SingleArgumentTokenizer
    {
        public override string MethodName
        {
            get { return "GreaterThan"; }
        }

        public override bool IsNewRule
        {
            get { return false; }
        }
    }
}