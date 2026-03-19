using System;
using Timberborn.BlockSystem;
using UnityEngine.UIElements;

namespace Timberborn.BuildingTools
{
	// Token: 0x0200000B RID: 11
	public interface ISectionProvider
	{
		// Token: 0x06000023 RID: 35
		bool TryGetSection(Preview preview, out VisualElement section);
	}
}
