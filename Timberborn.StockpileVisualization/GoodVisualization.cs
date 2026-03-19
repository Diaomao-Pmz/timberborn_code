using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.EntitySystem;
using Timberborn.Goods;
using Timberborn.Rendering;
using UnityEngine;

namespace Timberborn.StockpileVisualization
{
	// Token: 0x02000010 RID: 16
	public class GoodVisualization : BaseComponent, IAwakableComponent, IDeletableEntity
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00002F7B File Offset: 0x0000117B
		public GoodVisualization(GoodIconVisualizer goodIconVisualizer, MaterialHeightCutoffSetter materialHeightCutoffSetter)
		{
			this._goodIconVisualizer = goodIconVisualizer;
			this._materialHeightCutoffSetter = materialHeightCutoffSetter;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002F91 File Offset: 0x00001191
		public void Awake()
		{
			this._buildingModel = base.GetComponent<BuildingModel>();
			this._entityMaterials = base.GetComponent<EntityMaterials>();
			this.CreateVisualizationObject();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002FB1 File Offset: 0x000011B1
		public void SetLocalPosition(Vector3 position)
		{
			this._visualization.transform.localPosition = position;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002FC4 File Offset: 0x000011C4
		public void SetPositionAndRotation(Vector3 position, Quaternion quaternion)
		{
			this._visualization.transform.SetPositionAndRotation(position, quaternion);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002FD8 File Offset: 0x000011D8
		public void SetMaterial(Material material, float heightCutoff)
		{
			this.SetNewMaterial(material);
			this.SetHeightCutoff(heightCutoff);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002FE8 File Offset: 0x000011E8
		public void SetMesh(Mesh mesh)
		{
			this._meshFilter.sharedMesh = mesh;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002FF6 File Offset: 0x000011F6
		public void SetIcon(GoodSpec goodSpec)
		{
			this.SetIcon(goodSpec, goodSpec.ContainerColor);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003005 File Offset: 0x00001205
		public void SetIcon(GoodSpec goodSpec, Color color)
		{
			if (this._meshRenderer.material)
			{
				this._goodIconVisualizer.ShowIcon(this._meshRenderer.material, goodSpec, color);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003031 File Offset: 0x00001231
		public void Clear()
		{
			this.ClearMaterial();
			this._meshFilter.sharedMesh = this._emptyMesh;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000304A File Offset: 0x0000124A
		public void DeleteEntity()
		{
			Object.Destroy(this._emptyMesh);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003058 File Offset: 0x00001258
		public void CreateVisualizationObject()
		{
			this._visualization = new GameObject("GoodVisualization");
			this._visualization.transform.SetParent(this._buildingModel.FinishedModel.transform, false);
			this._emptyMesh = new Mesh();
			this._meshFilter = this._visualization.AddComponent<MeshFilter>();
			this._meshRenderer = this._visualization.AddComponent<MeshRenderer>();
			this.Clear();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000030CC File Offset: 0x000012CC
		public void SetHeightCutoff(float cutoff)
		{
			float y = base.GetComponent<BlockObjectCenter>().WorldCenterGrounded.y;
			this._materialHeightCutoffSetter.SetCutoff(this._meshRenderer.material, y + cutoff);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003103 File Offset: 0x00001303
		public void SetNewMaterial(Material material)
		{
			this.ClearMaterial();
			this._meshRenderer.material = material;
			this._entityMaterials.AddMaterial(this._visualization.transform, this._meshRenderer.material);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003138 File Offset: 0x00001338
		public void ClearMaterial()
		{
			if (this._meshRenderer.material)
			{
				this._entityMaterials.DestroyMaterial(this._meshRenderer.material);
			}
		}

		// Token: 0x04000035 RID: 53
		public readonly GoodIconVisualizer _goodIconVisualizer;

		// Token: 0x04000036 RID: 54
		public readonly MaterialHeightCutoffSetter _materialHeightCutoffSetter;

		// Token: 0x04000037 RID: 55
		public BuildingModel _buildingModel;

		// Token: 0x04000038 RID: 56
		public EntityMaterials _entityMaterials;

		// Token: 0x04000039 RID: 57
		public MeshFilter _meshFilter;

		// Token: 0x0400003A RID: 58
		public MeshRenderer _meshRenderer;

		// Token: 0x0400003B RID: 59
		public GameObject _visualization;

		// Token: 0x0400003C RID: 60
		public Mesh _emptyMesh;
	}
}
