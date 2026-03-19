using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;

namespace Timberborn.BlockSystem
{
	// Token: 0x0200004A RID: 74
	public interface IPreviewValidator
	{
		// Token: 0x060001E1 RID: 481
		bool IsValid(out string warningMessage);

		// Token: 0x060001E2 RID: 482
		ReadOnlyHashSet<BaseComponent> InvalidatedObjects(out string warningMessage);
	}
}
