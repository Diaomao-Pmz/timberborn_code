using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.TransformControl;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.NaturalResources
{
	// Token: 0x02000007 RID: 7
	public class CoordinatesOffsetter : BaseComponent, IPersistentEntity
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002106 File Offset: 0x00000306
		public Vector2 CoordinatesOffset { get; private set; } = Vector2.zero;

		// Token: 0x06000009 RID: 9 RVA: 0x0000210F File Offset: 0x0000030F
		public CoordinatesOffsetter(IFakeRandomNumberGeneratorFactory fakeRandomNumberGeneratorFactory)
		{
			this._fakeRandomNumberGeneratorFactory = fakeRandomNumberGeneratorFactory;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000212C File Offset: 0x0000032C
		public void SetRandomOffset()
		{
			this._fakeRandomNumberGenerator = this._fakeRandomNumberGeneratorFactory.Create(base.GetComponent<EntityComponent>().EntityId, 208621589);
			this.CoordinatesOffset = new Vector2(this.RandomCoordinateOffset(3), this.RandomCoordinateOffset(0));
			base.GetComponent<TransformController>().AddPositionModifier().Set(CoordinateSystem.GridToWorld(this.CoordinatesOffset));
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002193 File Offset: 0x00000393
		public void Save(IEntitySaver entitySaver)
		{
			if (this.CoordinatesOffset != Vector2.zero)
			{
				entitySaver.GetComponent(CoordinatesOffsetter.CoordinatesOffsetterKey).Set(CoordinatesOffsetter.RandomKey, true);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021C0 File Offset: 0x000003C0
		[BackwardCompatible(2025, 4, 20, Compatibility.Map)]
		public void Load(IEntityLoader entityLoader)
		{
			ComponentKey key = new ComponentKey("CoordinatesOffseter");
			PropertyKey<Vector2> key2 = new PropertyKey<Vector2>("CoordinatesOffset");
			IObjectLoader objectLoader;
			IObjectLoader objectLoader2;
			if (entityLoader.TryGetComponent(CoordinatesOffsetter.CoordinatesOffsetterKey, out objectLoader))
			{
				if (objectLoader.Has<bool>(CoordinatesOffsetter.RandomKey) && objectLoader.Get(CoordinatesOffsetter.RandomKey))
				{
					this.SetRandomOffset();
					return;
				}
			}
			else if (entityLoader.TryGetComponent(key, out objectLoader2) && objectLoader2.Has<Vector2>(key2) && objectLoader2.Get(key2) != Vector2.zero)
			{
				this.SetRandomOffset();
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002242 File Offset: 0x00000442
		public float RandomCoordinateOffset(int byteIndex)
		{
			return this._fakeRandomNumberGenerator.Range(-0.5f, 0.5f, byteIndex) * CoordinatesOffsetter.MaxCoordinateOffset;
		}

		// Token: 0x04000008 RID: 8
		public static readonly float MaxCoordinateOffset = 0.5f;

		// Token: 0x04000009 RID: 9
		public static readonly ComponentKey CoordinatesOffsetterKey = new ComponentKey("CoordinatesOffsetter");

		// Token: 0x0400000A RID: 10
		public static readonly PropertyKey<bool> RandomKey = new PropertyKey<bool>("Random");

		// Token: 0x0400000C RID: 12
		public readonly IFakeRandomNumberGeneratorFactory _fakeRandomNumberGeneratorFactory;

		// Token: 0x0400000D RID: 13
		public IFakeRandomNumberGenerator _fakeRandomNumberGenerator;
	}
}
