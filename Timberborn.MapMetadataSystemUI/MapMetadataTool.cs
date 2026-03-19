using System;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using Timberborn.ToolSystem;
using Timberborn.ToolSystemUI;

namespace Timberborn.MapMetadataSystemUI
{
	// Token: 0x02000009 RID: 9
	public class MapMetadataTool : ITool, IToolDescriptor, IWaterIgnoringTool, ILoadableSingleton
	{
		// Token: 0x06000024 RID: 36 RVA: 0x0000261E File Offset: 0x0000081E
		public MapMetadataTool(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000262D File Offset: 0x0000082D
		public void Load()
		{
			this._toolDescription = new ToolDescription.Builder(this._loc.T(MapMetadataTool.TitleLocKey)).Build();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000264F File Offset: 0x0000084F
		public void Enter()
		{
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000264F File Offset: 0x0000084F
		public void Exit()
		{
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002651 File Offset: 0x00000851
		public ToolDescription DescribeTool()
		{
			return this._toolDescription;
		}

		// Token: 0x0400001F RID: 31
		public static readonly string TitleLocKey = "MapEditor.MapMetadata.Title";

		// Token: 0x04000020 RID: 32
		public readonly ILoc _loc;

		// Token: 0x04000021 RID: 33
		public ToolDescription _toolDescription;
	}
}
