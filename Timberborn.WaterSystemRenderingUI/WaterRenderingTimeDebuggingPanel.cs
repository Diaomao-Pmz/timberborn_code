using System;
using System.Text;
using Timberborn.Common;
using Timberborn.DebuggingUI;
using Timberborn.SingletonSystem;
using Timberborn.WaterSystemRendering;

namespace Timberborn.WaterSystemRenderingUI
{
	// Token: 0x02000005 RID: 5
	public class WaterRenderingTimeDebuggingPanel : ILoadableSingleton, IDebuggingPanel
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002A30 File Offset: 0x00000C30
		public WaterRenderingTimeDebuggingPanel(IWaterRenderer waterRenderer, DebuggingPanel debuggingPanel)
		{
			this._debuggingPanel = debuggingPanel;
			this._waterRenderer = waterRenderer;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002A51 File Offset: 0x00000C51
		public void Load()
		{
			this._debuggingPanel.AddDebuggingPanel(this, "Water rendering times");
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002A64 File Offset: 0x00000C64
		public string GetText()
		{
			this._stringBuilder.Clear();
			this._stringBuilder.AppendLine(string.Format("Update mesh: {0}ms", this._waterRenderer.UpdateMeshTime));
			this._stringBuilder.AppendLine(string.Format("Update textures: {0}ms", this._waterRenderer.UpdateTexturesTime));
			return this._stringBuilder.ToStringWithoutNewLineEnd();
		}

		// Token: 0x0400001F RID: 31
		public readonly IWaterRenderer _waterRenderer;

		// Token: 0x04000020 RID: 32
		public readonly DebuggingPanel _debuggingPanel;

		// Token: 0x04000021 RID: 33
		public readonly StringBuilder _stringBuilder = new StringBuilder();
	}
}
