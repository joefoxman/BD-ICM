using System.Collections.Generic;
using Csla.Core;
using Csla.Rules;

namespace Bd.Icm.Rules
{
    public class ObjectRequired : BusinessRule
    {
        public object Object { get; set; }

        public ObjectRequired(IPropertyInfo objectProperty)
            : base(objectProperty)
        {
            Object = objectProperty;
            InputProperties = new List<IPropertyInfo> { objectProperty };
        }

        protected override void Execute(RuleContext context)
        { 
            var obj = context.InputPropertyValues[PrimaryProperty];
            if (obj == null)
            {
                context.AddErrorResult(string.Format("{0} is required.", PrimaryProperty.FriendlyName));
            }
        }
    }
}
