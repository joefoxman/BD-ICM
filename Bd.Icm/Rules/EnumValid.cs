using System;
using System.Collections.Generic;
using Csla.Core;
using Csla.Rules;

namespace Bd.Icm.Rules
{
    public class EnumValid<TEnum> : BusinessRule where TEnum : IComparable
    {
        public EnumValid(IPropertyInfo primaryProperty)
            : base(primaryProperty)
        {
            InputProperties = new List<IPropertyInfo>();
            InputProperties.Add(primaryProperty);
        }

        protected override void Execute(RuleContext context)
        {
            object value = context.InputPropertyValues[PrimaryProperty];
            if (Enum.IsDefined(typeof(TEnum), value) == false)
            {
                context.AddErrorResult("Enum invalid.");
            }
        }
    }
}
