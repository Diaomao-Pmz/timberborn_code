using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.TransformControl;
using UnityEngine;

namespace Timberborn.NaturalResourcesModelSystem
{
	// Token: 0x02000009 RID: 9
	public class NaturalResourceModelRandomizer : BaseComponent, IAwakableComponent, IPreInitializableEntity
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002471 File Offset: 0x00000671
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002479 File Offset: 0x00000679
		public float DiameterScale { get; private set; }

		// Token: 0x06000021 RID: 33 RVA: 0x00002482 File Offset: 0x00000682
		public NaturalResourceModelRandomizer(IFakeRandomNumberGeneratorFactory fakeRandomNumberGeneratorFactory)
		{
			this._fakeRandomNumberGeneratorFactory = fakeRandomNumberGeneratorFactory;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002491 File Offset: 0x00000691
		public void Awake()
		{
			this._naturalResourceModelRandomizerSpec = base.GetComponent<NaturalResourceModelRandomizerSpec>();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000249F File Offset: 0x0000069F
		public void PreInitializeEntity()
		{
			this._fakeRandomNumberGenerator = this._fakeRandomNumberGeneratorFactory.Create(base.GetComponent<EntityComponent>().EntityId, 972389643);
			this.Randomize();
			this.Apply();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024CE File Offset: 0x000006CE
		public void Randomize()
		{
			this.RandomizeHeightScale();
			this.RandomizeDiameterScale();
			this.RandomizeRotationAngle();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000024E2 File Offset: 0x000006E2
		public void RandomizeHeightScale()
		{
			this._heightScale = this._fakeRandomNumberGenerator.Range(this._naturalResourceModelRandomizerSpec.MinHeightScaleFactor, this._naturalResourceModelRandomizerSpec.MaxHeightScaleFactor, 0);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000250C File Offset: 0x0000070C
		public void RandomizeDiameterScale()
		{
			this.DiameterScale = (this._naturalResourceModelRandomizerSpec.ConstrainProportion ? this._heightScale : this._fakeRandomNumberGenerator.Range(this._naturalResourceModelRandomizerSpec.MinWidthScaleFactor, this._naturalResourceModelRandomizerSpec.MaxWidthScaleFactor, 1));
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000254C File Offset: 0x0000074C
		public void RandomizeRotationAngle()
		{
			RandomizeRotationMode randomizedRotation = this._naturalResourceModelRandomizerSpec.RandomizedRotation;
			float rotation;
			if (randomizedRotation != RandomizeRotationMode.By90Degree)
			{
				if (randomizedRotation != RandomizeRotationMode.BetweenMinAndMax)
				{
					throw new ArgumentOutOfRangeException();
				}
				rotation = this._fakeRandomNumberGenerator.Range(this._naturalResourceModelRandomizerSpec.MinRotation, this._naturalResourceModelRandomizerSpec.MaxRotation, 2);
			}
			else
			{
				rotation = NaturalResourceModelRandomizer.RotationsBy90Degree[(int)this._fakeRandomNumberGenerator.Byte(2) % NaturalResourceModelRandomizer.RotationsBy90Degree.Count];
			}
			this._rotation = rotation;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025C4 File Offset: 0x000007C4
		public void Apply()
		{
			TransformController component = base.GetComponent<TransformController>();
			component.AddScaleModifier().Set(new Vector3(this.DiameterScale, this._heightScale, this.DiameterScale));
			component.AddRotationModifier(10).Set(Quaternion.AngleAxis(this._rotation, Vector3.up));
		}

		// Token: 0x0400000F RID: 15
		public static readonly List<float> RotationsBy90Degree = new List<float>
		{
			0f,
			90f,
			180f,
			270f
		};

		// Token: 0x04000011 RID: 17
		public readonly IFakeRandomNumberGeneratorFactory _fakeRandomNumberGeneratorFactory;

		// Token: 0x04000012 RID: 18
		public IFakeRandomNumberGenerator _fakeRandomNumberGenerator;

		// Token: 0x04000013 RID: 19
		public NaturalResourceModelRandomizerSpec _naturalResourceModelRandomizerSpec;

		// Token: 0x04000014 RID: 20
		public float _heightScale;

		// Token: 0x04000015 RID: 21
		public float _rotation;
	}
}
