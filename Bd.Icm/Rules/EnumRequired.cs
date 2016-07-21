using System;
using System.Collections.Generic;
using Csla.Rules;
using Csla.Core;

namespace Bd.Icm.Rules
{
    public class EnumRequired<TEnum> : BusinessRule where TEnum : IComparable
    {
        TEnum _emptyValue;

        public EnumRequired(IPropertyInfo primaryProperty, TEnum emptyValue)
            : base(primaryProperty)
        {
            InputProperties = new List<IPropertyInfo>();
            InputProperties.Add(primaryProperty);
            _emptyValue = emptyValue;
        }

        protected override void Execute(RuleContext context)
        {
            object value = context.InputPropertyValues[PrimaryProperty];
            if ((Enum.IsDefined(typeof(TEnum), value) == false) ||
                (value.Equals(_emptyValue)))
            {
                context.AddErrorResult(string.Format("{0} required.", PrimaryProperty.FriendlyName));
            }
        }
    }
}
