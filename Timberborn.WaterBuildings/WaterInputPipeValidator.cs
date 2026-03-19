using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Localization;

namespace Timberborn.WaterBuildings
{
	// Token: 0x02000034 RID: 52
	public class WaterInputPipeValidator : BaseComponent, IAwakableComponent, IPreviewValidator
	{
		// Token: 0x06000263 RID: 611 RVA: 0x0000774A File Offset: 0x0000594A
		public WaterInputPipeValidator(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00007759 File Offset: 0x00005959
		public void Awake()
		{
			this._waterInputCoordinates = base.GetComponent<WaterInputCoordinates>();
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00007767 File Offset: 0x00005967
		public bool IsValid(out string warningMessage)
		{
			if (this._waterInputCoordinates.Depth == 0)
			{
				warningMessage = this._loc.T(WaterInputPipeValidator.PipeObstructedKey);
				return false;
			}
			warningMessage = string.Empty;
			return true;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00007792 File Offset: 0x00005992
		public ReadOnlyHashSet<BaseComponent> InvalidatedObjects(out string warningMessage)
		{
			warningMessage = string.Empty;
			return WaterInputPipeValidator.EmptyHashSet;
		}

		// Token: 0x040000F4 RID: 244
		public static readonly string PipeObstructedKey = "Buildings.PipeObstructed";

		// Token: 0x040000F5 RID: 245
		public static readonly ReadOnlyHashSet<BaseComponent> EmptyHashSet = new HashSet<BaseComponent>().AsReadOnlyHashSet<BaseComponent>();

		// Token: 0x040000F6 RID: 246
		public readonly ILoc _loc;

		// Token: 0x040000F7 RID: 247
		public WaterInputCoordinates _waterInputCoordinates;
	}
}
