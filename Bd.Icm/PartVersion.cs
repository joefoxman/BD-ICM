using System;
using System.Linq.Expressions;
using Bd.Icm.Core;
using Csla;
using Csla.Reflection;

namespace Bd.Icm
{
    public class PartVersion
    {
        public Part From { get; private set; }
        public Part To { get; private set; }

        public PartVersion(Part fromPart, Part toPart)
        {
            fromPart.ThrowIfNull(nameof(fromPart));
            toPart.ThrowIfNull(nameof(toPart));
            From = fromPart;
            To = toPart;
        }

        public bool IsPropertyChanged(Expression<Func<Part, object>> propertyExpression)
        {
            var reflectedPropertyInfo = Reflect<Part>.GetProperty(propertyExpression);
            var currentValue = reflectedPropertyInfo.GetValue(To);
            var previousValue = reflectedPropertyInfo.GetValue(From);
            return !currentValue.Equals(previousValue);
        }
    }
}
