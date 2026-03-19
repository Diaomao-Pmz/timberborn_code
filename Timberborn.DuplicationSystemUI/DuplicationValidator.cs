using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.DuplicationSystem;
using Timberborn.ToolSystem;

namespace Timberborn.DuplicationSystemUI
{
	// Token: 0x0200000E RID: 14
	public class DuplicationValidator
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00002904 File Offset: 0x00000B04
		public DuplicationValidator(IEnumerable<IToolFinder> toolFinders)
		{
			this._toolFinders = toolFinders;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002913 File Offset: 0x00000B13
		public bool CanDuplicateSettings(BaseComponent entity)
		{
			return entity.AllComponents.FastAny(delegate(object component)
			{
				IDuplicable duplicable = component as IDuplicable;
				return duplicable != null && duplicable.IsDuplicable;
			});
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002944 File Offset: 0x00000B44
		public bool CanDuplicateObject(BaseComponent entity, out Action toolActivationAction)
		{
			toolActivationAction = null;
			if (!entity.HasComponent<DuplicationBlocker>())
			{
				using (IEnumerator<IToolFinder> enumerator = this._toolFinders.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.TryFindTool(entity, out toolActivationAction))
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x04000038 RID: 56
		public readonly IEnumerable<IToolFinder> _toolFinders;
	}
}
