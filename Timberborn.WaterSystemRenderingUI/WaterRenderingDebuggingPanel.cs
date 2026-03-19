using System;
using System.Text;
using Timberborn.Common;
using Timberborn.CursorToolSystem;
using Timberborn.DebuggingUI;
using Timberborn.SingletonSystem;
using Timberborn.WaterSystem;
using Timberborn.WaterSystemRendering;
using UnityEngine;

namespace Timberborn.WaterSystemRenderingUI
{
	// Token: 0x02000004 RID: 4
	public class WaterRenderingDebuggingPanel : ILoadableSingleton, IUnloadableSingleton, IDebuggingPanel
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public WaterRenderingDebuggingPanel(DebuggingPanel debuggingPanel, CursorDebugger cursorDebugger, IThreadSafeWaterMap threadSafeWaterMap)
		{
			this._debuggingPanel = debuggingPanel;
			this._cursorDebugger = cursorDebugger;
			this._threadSafeWaterMap = threadSafeWaterMap;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E8 File Offset: 0x000002E8
		public void Load()
		{
			this._debuggingPanel.AddDebuggingPanel(this, "Water rendering data");
			this._byteSamplingTexture = new Texture2D(1, 1, 63, false, true);
			this._byteSamplingRenderTexture = new RenderTexture(1, 1, 0, 16)
			{
				enableRandomWrite = true
			};
			this._vector2SamplingTexture = new Texture2D(1, 1, 19, false, true);
			this._vector2SamplingRenderTexture = new RenderTexture(1, 1, 0, 12)
			{
				enableRandomWrite = true
			};
			this._vector4SamplingTexture = new Texture2D(1, 1, 20, false, true);
			this._vector4SamplingRenderTexture = new RenderTexture(1, 1, 0, 11)
			{
				enableRandomWrite = true
			};
			this._colorSamplingTexture = new Texture2D(1, 1, 5, false, true);
			this._colorSamplingRenderTexture = new RenderTexture(1, 1, 0, 0)
			{
				enableRandomWrite = true
			};
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021A4 File Offset: 0x000003A4
		public void Unload()
		{
			Object.Destroy(this._byteSamplingRenderTexture);
			Object.Destroy(this._byteSamplingTexture);
			Object.Destroy(this._vector2SamplingRenderTexture);
			Object.Destroy(this._vector2SamplingTexture);
			Object.Destroy(this._vector4SamplingRenderTexture);
			Object.Destroy(this._vector4SamplingTexture);
			Object.Destroy(this._colorSamplingRenderTexture);
			Object.Destroy(this._colorSamplingTexture);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000220C File Offset: 0x0000040C
		public string GetText()
		{
			this._stringBuilder.Clear();
			if (this._cursorDebugger.Active)
			{
				this._stringBuilder.AppendLine(string.Format("Tick progress: {0:0.0000}", Shader.GetGlobalFloat(WaterRenderingDebuggingPanel.TickProgress)));
				this.GetGlobalTextures();
				this.SampleGlobalTextures();
			}
			return this._stringBuilder.ToStringWithoutNewLineEnd();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002270 File Offset: 0x00000470
		public void GetGlobalTextures()
		{
			this._oldWaterData = Shader.GetGlobalTexture(WaterTextureNames.OldWaterData);
			this._newWaterData = Shader.GetGlobalTexture(WaterTextureNames.NewWaterData);
			this._oldOutflows = Shader.GetGlobalTexture(WaterTextureNames.OldOutflows);
			this._newOutflows = Shader.GetGlobalTexture(WaterTextureNames.NewOutflows);
			this._oldEdgeLinks = Shader.GetGlobalTexture(WaterTextureNames.OldEdgeLinks);
			this._newEdgeLinks = Shader.GetGlobalTexture(WaterTextureNames.NewEdgeLinks);
			this._oldCornerLinks = Shader.GetGlobalTexture(WaterTextureNames.OldCornerLinks);
			this._newCornerLinks = Shader.GetGlobalTexture(WaterTextureNames.NewCornerLinks);
			this._oldSkirts = Shader.GetGlobalTexture(WaterTextureNames.OldSkirts);
			this._newSkirts = Shader.GetGlobalTexture(WaterTextureNames.NewSkirts);
			this._oldContaminations = Shader.GetGlobalTexture(WaterTextureNames.OldContaminations);
			this._newContaminations = Shader.GetGlobalTexture(WaterTextureNames.NewContaminations);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002340 File Offset: 0x00000540
		public void SampleGlobalTextures()
		{
			RenderTexture active = RenderTexture.active;
			Vector2Int coords = this._cursorDebugger.Coordinates.XY();
			for (int i = 0; i < this._threadSafeWaterMap.MaxColumnCount; i++)
			{
				this.SampleTextureSet(coords, i, this._oldWaterData, this._newWaterData, this._oldOutflows, this._newOutflows, this._oldEdgeLinks, this._newEdgeLinks, this._oldCornerLinks, this._newCornerLinks, this._oldSkirts, this._newSkirts, this._oldContaminations, this._newContaminations);
			}
			RenderTexture.active = active;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000023D0 File Offset: 0x000005D0
		public void SampleTextureSet(Vector2Int coords, int columnIndex, Texture oldWaterDataTexture, Texture newWaterDataTexture, Texture oldOutflowsTexture, Texture newOutflowsTexture, Texture oldEdgeLinksTexture, Texture newEdgeLinksTexture, Texture oldCornerLinksTexture, Texture newCornerLinksTexture, Texture oldSkirtVisibilityTexture, Texture newSkirtVisibilityTexture, Texture oldContaminationsTexture, Texture newContaminationsTexture)
		{
			Vector4 vector = this.SampleVector4Texture(oldWaterDataTexture, columnIndex, coords);
			Vector4 vector2 = this.SampleVector4Texture(newWaterDataTexture, columnIndex, coords);
			Vector2 vector3 = this.SampleVector2Texture(oldOutflowsTexture, columnIndex, coords);
			Vector2 vector4 = this.SampleVector2Texture(newOutflowsTexture, columnIndex, coords);
			Vector4 input = this.SampleVector4Texture(oldEdgeLinksTexture, columnIndex, coords);
			Vector4 input2 = this.SampleVector4Texture(newEdgeLinksTexture, columnIndex, coords);
			float num = this.SampleByteTexture(oldContaminationsTexture, columnIndex, coords);
			float num2 = this.SampleByteTexture(newContaminationsTexture, columnIndex, coords);
			if (vector.x > 0f || vector2.x > 0f || WaterRenderingDebuggingPanel.HasAnyConnection(input) || WaterRenderingDebuggingPanel.HasAnyConnection(input2) || num > 0f || num2 > 0f || Math.Abs(vector3.x) > 0f || Math.Abs(vector3.y) > 0f || Math.Abs(vector4.x) > 0f || Math.Abs(vector4.y) > 0f)
			{
				Vector4 input3 = this.SampleVector4Texture(oldCornerLinksTexture, columnIndex, coords);
				Color32 input4 = this.SampleColorTexture(oldSkirtVisibilityTexture, columnIndex, coords);
				this._stringBuilder.AppendLine(string.Format("Column index: {0}", columnIndex));
				this._stringBuilder.AppendLine("Old:");
				this._stringBuilder.AppendLine(string.Format("Depth: {0:0.000000}, ", vector.x) + string.Format("Floor: {0:0.} Ceiling: {1:0.}", vector.y, vector.z));
				this._stringBuilder.AppendLine("Edge: " + WaterRenderingDebuggingPanel.FormatEdge(input));
				this._stringBuilder.AppendLine("Corner: " + WaterRenderingDebuggingPanel.FormatCorner(input3));
				this._stringBuilder.AppendLine("Skirts: " + WaterRenderingDebuggingPanel.FormatSkirts(input4));
				this._stringBuilder.AppendLine(string.Format("Contamination: {0:0.000}", num));
				this._stringBuilder.AppendLine("Outflows: " + WaterRenderingDebuggingPanel.FormatOutflows(vector3));
				Vector4 input5 = this.SampleVector4Texture(newCornerLinksTexture, columnIndex, coords);
				Color32 input6 = this.SampleColorTexture(newSkirtVisibilityTexture, columnIndex, coords);
				this._stringBuilder.AppendLine("New:");
				this._stringBuilder.AppendLine(string.Format("Depth: {0:0.000000}, ", vector2.x) + string.Format("Floor: {0:0.} Ceiling: {1:0.}", vector2.y, vector2.z));
				this._stringBuilder.AppendLine("Edge: " + WaterRenderingDebuggingPanel.FormatEdge(input2));
				this._stringBuilder.AppendLine("Corner: " + WaterRenderingDebuggingPanel.FormatCorner(input5));
				this._stringBuilder.AppendLine("Skirts: " + WaterRenderingDebuggingPanel.FormatSkirts(input6));
				this._stringBuilder.AppendLine(string.Format("Contamination: {0:0.000}", num2));
				this._stringBuilder.AppendLine("Outflows: " + WaterRenderingDebuggingPanel.FormatOutflows(vector4));
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000026DC File Offset: 0x000008DC
		public float SampleByteTexture(Texture sourceTexture, int layer, Vector2Int coordinates)
		{
			Graphics.CopyTexture(sourceTexture, layer, 0, coordinates.x, coordinates.y, 1, 1, this._byteSamplingRenderTexture, 0, 0, 0, 0);
			RenderTexture.active = this._byteSamplingRenderTexture;
			this._byteSamplingTexture.ReadPixels(new Rect(0f, 0f, 1f, 1f), 0, 0, false);
			return (float)this._byteSamplingTexture.GetRawTextureData<byte>()[0] / 255f;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002758 File Offset: 0x00000958
		public Vector2 SampleVector2Texture(Texture sourceTexture, int layer, Vector2Int coordinates)
		{
			Graphics.CopyTexture(sourceTexture, layer, 0, coordinates.x, coordinates.y, 1, 1, this._vector2SamplingRenderTexture, 0, 0, 0, 0);
			RenderTexture.active = this._vector2SamplingRenderTexture;
			this._vector2SamplingTexture.ReadPixels(new Rect(0f, 0f, 1f, 1f), 0, 0, false);
			return this._vector2SamplingTexture.GetRawTextureData<Vector2>()[0];
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000027D0 File Offset: 0x000009D0
		public Vector4 SampleVector4Texture(Texture sourceTexture, int layer, Vector2Int coordinates)
		{
			Graphics.CopyTexture(sourceTexture, layer, 0, coordinates.x, coordinates.y, 1, 1, this._vector4SamplingRenderTexture, 0, 0, 0, 0);
			RenderTexture.active = this._vector4SamplingRenderTexture;
			this._vector4SamplingTexture.ReadPixels(new Rect(0f, 0f, 1f, 1f), 0, 0, false);
			return this._vector4SamplingTexture.GetRawTextureData<Vector4>()[0];
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002848 File Offset: 0x00000A48
		public Color32 SampleColorTexture(Texture sourceTexture, int layer, Vector2Int coordinates)
		{
			Graphics.CopyTexture(sourceTexture, layer, 0, coordinates.x, coordinates.y, 1, 1, this._colorSamplingRenderTexture, 0, 0, 0, 0);
			RenderTexture.active = this._colorSamplingRenderTexture;
			this._colorSamplingTexture.ReadPixels(new Rect(0f, 0f, 1f, 1f), 0, 0, false);
			return this._colorSamplingTexture.GetRawTextureData<Color32>()[0];
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000028BD File Offset: 0x00000ABD
		public static bool HasAnyConnection(Vector4 input)
		{
			return input.x >= 0f || input.y >= 0f || input.z >= 0f || input.w >= 0f;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000028F8 File Offset: 0x00000AF8
		public static string FormatEdge(Vector4 input)
		{
			return string.Format("T: {0:0.}, L: {1:0.}, B: {2:0.}, R: {3:0.}", new object[]
			{
				input.x,
				input.y,
				input.z,
				input.w
			});
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002950 File Offset: 0x00000B50
		public static string FormatCorner(Vector4 input)
		{
			return string.Format("TL: {0:0.}, TR: {1:0.}, BL: {2:0.}, BR: {3:0.}", new object[]
			{
				input.x,
				input.y,
				input.z,
				input.w
			});
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000029A8 File Offset: 0x00000BA8
		public static string FormatSkirts(Color32 input)
		{
			return string.Format("T: {0}, L: {1}, B: {2}, R: {3}", new object[]
			{
				input.r,
				input.g,
				input.b,
				input.a
			});
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000029FD File Offset: 0x00000BFD
		public static string FormatOutflows(Vector2 input)
		{
			return string.Format("H: {0:0.000}, V: {1:0.000}", input.x, input.y);
		}

		// Token: 0x04000006 RID: 6
		public static readonly int TickProgress = Shader.PropertyToID("_TickProgress");

		// Token: 0x04000007 RID: 7
		public readonly DebuggingPanel _debuggingPanel;

		// Token: 0x04000008 RID: 8
		public readonly CursorDebugger _cursorDebugger;

		// Token: 0x04000009 RID: 9
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;

		// Token: 0x0400000A RID: 10
		public readonly StringBuilder _stringBuilder = new StringBuilder();

		// Token: 0x0400000B RID: 11
		public Texture _oldWaterData;

		// Token: 0x0400000C RID: 12
		public Texture _newWaterData;

		// Token: 0x0400000D RID: 13
		public Texture _oldOutflows;

		// Token: 0x0400000E RID: 14
		public Texture _newOutflows;

		// Token: 0x0400000F RID: 15
		public Texture _oldEdgeLinks;

		// Token: 0x04000010 RID: 16
		public Texture _newEdgeLinks;

		// Token: 0x04000011 RID: 17
		public Texture _oldCornerLinks;

		// Token: 0x04000012 RID: 18
		public Texture _newCornerLinks;

		// Token: 0x04000013 RID: 19
		public Texture _oldSkirts;

		// Token: 0x04000014 RID: 20
		public Texture _newSkirts;

		// Token: 0x04000015 RID: 21
		public Texture _oldContaminations;

		// Token: 0x04000016 RID: 22
		public Texture _newContaminations;

		// Token: 0x04000017 RID: 23
		public Texture2D _byteSamplingTexture;

		// Token: 0x04000018 RID: 24
		public RenderTexture _byteSamplingRenderTexture;

		// Token: 0x04000019 RID: 25
		public Texture2D _vector2SamplingTexture;

		// Token: 0x0400001A RID: 26
		public RenderTexture _vector2SamplingRenderTexture;

		// Token: 0x0400001B RID: 27
		public Texture2D _vector4SamplingTexture;

		// Token: 0x0400001C RID: 28
		public RenderTexture _vector4SamplingRenderTexture;

		// Token: 0x0400001D RID: 29
		public Texture2D _colorSamplingTexture;

		// Token: 0x0400001E RID: 30
		public RenderTexture _colorSamplingRenderTexture;
	}
}
