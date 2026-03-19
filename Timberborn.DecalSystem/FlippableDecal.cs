using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.DuplicationSystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.DecalSystem
{
	// Token: 0x02000013 RID: 19
	public class FlippableDecal : BaseComponent, IAwakableComponent, IPersistentEntity, IDuplicable<FlippableDecal>, IDuplicable
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002EB7 File Offset: 0x000010B7
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00002EBF File Offset: 0x000010BF
		public bool IsFlipped { get; private set; }

		// Token: 0x06000071 RID: 113 RVA: 0x00002EC8 File Offset: 0x000010C8
		public void Awake()
		{
			FlippableDecalSpec component = base.GetComponent<FlippableDecalSpec>();
			this._decalTransform = base.GameObject.FindChildTransform(component.DecalName);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002EF3 File Offset: 0x000010F3
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(FlippableDecal.FlippableDecalKey).Set(FlippableDecal.IsFlippedKey, this.IsFlipped);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002F10 File Offset: 0x00001110
		[BackwardCompatible(2025, 11, 7, Compatibility.Save)]
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(FlippableDecal.FlippableDecalKey, out objectLoader))
			{
				this.SetFlip(objectLoader.Get(FlippableDecal.IsFlippedKey));
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002F40 File Offset: 0x00001140
		public void SetFlip(bool value)
		{
			this.IsFlipped = value;
			if ((this.IsFlipped && this._decalTransform.localScale.x > 0f) || (!this.IsFlipped && this._decalTransform.localScale.x < 0f))
			{
				this.FlipDecal();
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002F98 File Offset: 0x00001198
		public void DuplicateFrom(FlippableDecal source)
		{
			this.SetFlip(source.IsFlipped);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002FA8 File Offset: 0x000011A8
		public void FlipDecal()
		{
			Vector3 localScale = this._decalTransform.localScale;
			localScale.x *= -1f;
			this._decalTransform.localScale = localScale;
		}

		// Token: 0x04000029 RID: 41
		public static readonly ComponentKey FlippableDecalKey = new ComponentKey("FlippableDecal");

		// Token: 0x0400002A RID: 42
		public static readonly PropertyKey<bool> IsFlippedKey = new PropertyKey<bool>("IsFlipped");

		// Token: 0x0400002C RID: 44
		public Transform _decalTransform;
	}
}
