using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using UnityEngine;

namespace Timberborn.Rendering
{
	// Token: 0x0200000C RID: 12
	public class EntityMaterials : BaseComponent, IAwakableComponent, IDeletableEntity
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00002882 File Offset: 0x00000A82
		public void Awake()
		{
			this.AddMaterials(base.GameObject);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002890 File Offset: 0x00000A90
		public void DeleteEntity()
		{
			foreach (EntityMaterials.ChildMaterial childMaterial in this._childMaterials)
			{
				Object.Destroy(childMaterial.Material);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000028E8 File Offset: 0x00000AE8
		public void AddMaterials(GameObject owner)
		{
			foreach (MeshRenderer meshRenderer in owner.GetComponentsInChildren<MeshRenderer>(true))
			{
				foreach (Material material in meshRenderer.materials)
				{
					this.AddMaterial(meshRenderer.transform, material);
				}
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000293C File Offset: 0x00000B3C
		public void AddMaterial(Transform owner, Material material)
		{
			this._childMaterials.Add(new EntityMaterials.ChildMaterial(owner, material));
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002950 File Offset: 0x00000B50
		public void DestroyMaterial(Material material)
		{
			for (int i = this._childMaterials.Count - 1; i >= 0; i--)
			{
				if (this._childMaterials[i].Material == material)
				{
					this._childMaterials.RemoveAt(i);
				}
			}
			Object.Destroy(material);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000029A4 File Offset: 0x00000BA4
		public void GetChildMaterials(Transform parent, List<Material> childMaterials)
		{
			for (int i = 0; i < this._childMaterials.Count; i++)
			{
				EntityMaterials.ChildMaterial childMaterial = this._childMaterials[i];
				if (childMaterial.Child.IsChildOf(parent))
				{
					childMaterials.Add(childMaterial.Material);
				}
			}
		}

		// Token: 0x04000019 RID: 25
		public readonly List<EntityMaterials.ChildMaterial> _childMaterials = new List<EntityMaterials.ChildMaterial>();

		// Token: 0x0200000D RID: 13
		public readonly struct ChildMaterial
		{
			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000035 RID: 53 RVA: 0x00002A03 File Offset: 0x00000C03
			public Transform Child { get; }

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000036 RID: 54 RVA: 0x00002A0B File Offset: 0x00000C0B
			public Material Material { get; }

			// Token: 0x06000037 RID: 55 RVA: 0x00002A13 File Offset: 0x00000C13
			public ChildMaterial(Transform child, Material material)
			{
				this.Child = child;
				this.Material = material;
			}
		}
	}
}
