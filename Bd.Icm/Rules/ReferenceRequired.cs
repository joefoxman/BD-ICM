using System;
using System.Collections.Generic;
using Csla.Core;
using Csla.Rules;

namespace Bd.Icm.Rules
{
    public class ReferenceRequired<T> : BusinessRule where T : class, IReferenceObject
    {
        public IPropertyInfo Object { get; set; }

        public ReferenceRequired(IPropertyInfo objectProperty)
            : base(objectProperty)
        {
            Object = objectProperty;
            InputProperties = new List<IPropertyInfo> { objectProperty };
        }

        protected override void Execute(RuleContext context)
        { 
            var obj = context.InputPropertyValues[PrimaryProperty] as T;
            if ((obj == null) || (obj.Id == 0))
            {
                context.AddErrorResult(string.Format("{0} is required.", PrimaryProperty.FriendlyName));
            }
        }
    }
}
