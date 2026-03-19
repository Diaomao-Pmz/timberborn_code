using System;
using Timberborn.Debugging;

namespace Timberborn.WaterSystemRendering
{
	// Token: 0x0200001E RID: 30
	public class WaterSystemRenderingDevModule : IDevModule
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x00005695 File Offset: 0x00003895
		public WaterSystemRenderingDevModule(IWaterMesh waterMesh, IWaterRenderer waterRenderer)
		{
			this._waterMesh = waterMesh;
			this._waterRenderer = waterRenderer;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000056C8 File Offset: 0x000038C8
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.Create("Toggle models: Water", new Action(this.ToggleModels))).AddMethod(DevMethod.Create("Toggle water logic: meshes", new Action(this.ToggleMeshes))).AddMethod(DevMethod.Create("Toggle water logic: textures", new Action(this.ToggleTextures))).AddMethod(DevMethod.Create("Toggle water logic: postprocess", new Action(this.TogglePostprocessing))).Build();
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000574B File Offset: 0x0000394B
		public void ToggleMeshes()
		{
			if (this._meshesActive)
			{
				this._waterRenderer.DisableMeshUpdate();
				this._meshesActive = false;
				return;
			}
			this._waterRenderer.EnableMeshUpdate();
			this._meshesActive = true;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000577A File Offset: 0x0000397A
		public void ToggleTextures()
		{
			if (this._texturesActive)
			{
				this._waterRenderer.DisableTextureUpdate();
				this._texturesActive = false;
				return;
			}
			this._waterRenderer.EnableTextureUpdate();
			this._texturesActive = true;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000057A9 File Offset: 0x000039A9
		public void TogglePostprocessing()
		{
			if (this._postprocessingActive)
			{
				this._waterRenderer.DisablePostprocessing();
				this._postprocessingActive = false;
				return;
			}
			this._waterRenderer.EnablePostprocessing();
			this._postprocessingActive = true;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000057D8 File Offset: 0x000039D8
		public void ToggleModels()
		{
			if (this._modelsActive)
			{
				this._waterMesh.Hide();
				this._modelsActive = false;
				return;
			}
			this._waterMesh.Show();
			this._modelsActive = true;
		}

		// Token: 0x040000C6 RID: 198
		public readonly IWaterMesh _waterMesh;

		// Token: 0x040000C7 RID: 199
		public readonly IWaterRenderer _waterRenderer;

		// Token: 0x040000C8 RID: 200
		public bool _modelsActive = true;

		// Token: 0x040000C9 RID: 201
		public bool _meshesActive = true;

		// Token: 0x040000CA RID: 202
		public bool _texturesActive = true;

		// Token: 0x040000CB RID: 203
		public bool _postprocessingActive = true;
	}
}
