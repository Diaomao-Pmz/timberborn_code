using System;
using System.Collections.Immutable;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.TemplateSystem;

namespace Timberborn.UnderstructureSystem
{
	// Token: 0x0200000A RID: 10
	public class UnderstructureConstraintValidator : IBlockObjectValidator
	{
		// Token: 0x0600002D RID: 45 RVA: 0x0000257A File Offset: 0x0000077A
		public UnderstructureConstraintValidator(UnderstructureFinder understructureFinder)
		{
			this._understructureFinder = understructureFinder;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000258C File Offset: 0x0000078C
		public bool IsValid(BlockObject blockObject, out string errorMessage)
		{
			UnderstructureConstraint understructureConstraint;
			if (!this.IsValid(blockObject, out understructureConstraint))
			{
				errorMessage = understructureConstraint.ErrorMessage;
				return false;
			}
			errorMessage = null;
			return true;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000025B2 File Offset: 0x000007B2
		public bool IsValid(BlockObject blockObject, out UnderstructureConstraint understructureConstraint)
		{
			understructureConstraint = blockObject.GetComponent<UnderstructureConstraint>();
			return understructureConstraint == null || this.IsValidAgainstSpec(blockObject, understructureConstraint);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000025CC File Offset: 0x000007CC
		public bool IsValidAgainstSpec(BlockObject validatedBlockObject, UnderstructureConstraint understructureConstraint)
		{
			ImmutableArray<string> understructureTemplateNames = understructureConstraint.UnderstructureTemplateNames;
			BlockObject blockObject = this._understructureFinder.FindStrict(validatedBlockObject);
			if (blockObject)
			{
				TemplateSpec templateBelow = blockObject.GetComponent<TemplateSpec>();
				if (templateBelow != null)
				{
					return understructureTemplateNames.FastAny((string expected) => templateBelow.IsNamed(expected));
				}
			}
			return false;
		}

		// Token: 0x04000016 RID: 22
		public readonly UnderstructureFinder _understructureFinder;
	}
}
