using System.Collections.Generic;
using Csla.Core;
using Csla.Rules;

namespace Bd.Icm.Rules
{
    public class ParentIdRequired : BusinessRule
    {
        public IPropertyInfo PrimaryIdProperty { get; set; }
        public IPropertyInfo SecondaryIdProperty { get; set; }

        public ParentIdRequired(IPropertyInfo primaryIdProperty, IPropertyInfo secondaryIdProperty)
            : base(primaryIdProperty)
        {
            PrimaryIdProperty = primaryIdProperty;
            SecondaryIdProperty = secondaryIdProperty;
            InputProperties = new List<IPropertyInfo> { primaryIdProperty, secondaryIdProperty };
        }

        protected override void Execute(RuleContext context)
        {
            var target = (Part) context.Target;
            if (target.IsChild) return;
            var primaryId = (int?)context.InputPropertyValues[PrimaryIdProperty];
            var secondaryId = (int?)context.InputPropertyValues[SecondaryIdProperty];
            if ((primaryId.HasValue == false) && (secondaryId.HasValue == false))
            {
                context.AddErrorResult("A parent ID is required.");
            }
            if (primaryId.HasValue && secondaryId.HasValue)
            {
                context.AddErrorResult("Only one parent ID is allowed.");
            }
        }
    }
}
