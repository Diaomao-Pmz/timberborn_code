using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.CharacterModelSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.Forestry;
using Timberborn.TimeSystem;
using Timberborn.TransformControl;
using UnityEngine;

namespace Timberborn.ForestryEffects
{
	// Token: 0x0200000C RID: 12
	public class TreeShaker : BaseComponent, IAwakableComponent, IInitializableEntity, IUpdatableComponent
	{
		// Token: 0x06000028 RID: 40 RVA: 0x000024B1 File Offset: 0x000006B1
		public TreeShaker(NonlinearAnimationManager nonlinearAnimationManager, IRandomNumberGenerator randomNumberGenerator)
		{
			this._nonlinearAnimationManager = nonlinearAnimationManager;
			this._randomNumberGenerator = randomNumberGenerator;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000024D4 File Offset: 0x000006D4
		public void Awake()
		{
			this._rotationModifier = base.GetComponent<TransformController>().AddRotationModifier(20);
			TreeRemoveYieldStrategy component = base.GetComponent<TreeRemoveYieldStrategy>();
			component.CuttingStarted += this.StartShaking;
			component.CuttingStopped += this.FinishShaking;
			base.DisableComponent();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002523 File Offset: 0x00000723
		public void InitializeEntity()
		{
			base.GameObject.GetComponentsInChildren<MeshRenderer>(true, this._meshRenderers);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002537 File Offset: 0x00000737
		public void Update()
		{
			this.UpdateTimer();
			this._rotationModifier.Set(Quaternion.AngleAxis(this.GetCurrentAngle(), this._axis));
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000255B File Offset: 0x0000075B
		public void StartShaking(object sender, TreeCutter treeCutter)
		{
			base.EnableComponent();
			this._timer = this._randomNumberGenerator.Range(0f, TreeShaker.CycleTime);
			this._axis = treeCutter.GetComponent<CharacterModel>().Model.forward;
			this.UpdateShakingInMaterials(true);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000259B File Offset: 0x0000079B
		public void FinishShaking(object sender, EventArgs eventArgs)
		{
			base.DisableComponent();
			this._timer = 0f;
			this._rotationModifier.Reset();
			this.UpdateShakingInMaterials(false);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000025C0 File Offset: 0x000007C0
		public void UpdateTimer()
		{
			if (this._timer > TreeShaker.CycleTime)
			{
				this._timer = 0f;
			}
			this._timer += Time.deltaTime * this._nonlinearAnimationManager.SpeedMultiplier;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000025F8 File Offset: 0x000007F8
		public float GetCurrentAngle()
		{
			return Mathf.Sin(this._timer * TreeShaker.SpeedMultiplier + this.GetSpeedNoise()) * this.GetCyclicAmplitudeDistortion() / (TreeShaker.AmplitudeDivider + this.GetAmplitudeNoise());
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002626 File Offset: 0x00000826
		public float GetSpeedNoise()
		{
			return Mathf.PerlinNoise1D(this._timer * 2f) * TreeShaker.PerlinNoiseMultiplier;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002640 File Offset: 0x00000840
		public float GetCyclicAmplitudeDistortion()
		{
			float num = 0.5f * TreeShaker.CycleTime;
			return Mathf.Pow(Mathf.Abs(this._timer - num) / num, 2f) + 0.5f;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002678 File Offset: 0x00000878
		public float GetAmplitudeNoise()
		{
			return (Mathf.PerlinNoise1D(this._timer) - 0.5f) * TreeShaker.PerlinNoiseMultiplier;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002694 File Offset: 0x00000894
		public void UpdateShakingInMaterials(bool isShaking)
		{
			float num = isShaking ? 1f : 0f;
			foreach (MeshRenderer meshRenderer in this._meshRenderers)
			{
				meshRenderer.material.SetFloat(TreeShaker.IsShakingPropertyId, num);
			}
		}

		// Token: 0x04000011 RID: 17
		public static readonly int IsShakingPropertyId = Shader.PropertyToID("_IsShaking");

		// Token: 0x04000012 RID: 18
		public static readonly float DoublePi = 6.2831855f;

		// Token: 0x04000013 RID: 19
		public static readonly float SpeedMultiplier = 2.5f * TreeShaker.DoublePi;

		// Token: 0x04000014 RID: 20
		public static readonly float AmplitudeDivider = 3f;

		// Token: 0x04000015 RID: 21
		public static readonly float CycleTime = 2f;

		// Token: 0x04000016 RID: 22
		public static readonly float PerlinNoiseMultiplier = 0.35f;

		// Token: 0x04000017 RID: 23
		public readonly NonlinearAnimationManager _nonlinearAnimationManager;

		// Token: 0x04000018 RID: 24
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x04000019 RID: 25
		public RotationModifier _rotationModifier;

		// Token: 0x0400001A RID: 26
		public float _timer;

		// Token: 0x0400001B RID: 27
		public Vector3 _axis;

		// Token: 0x0400001C RID: 28
		public readonly List<MeshRenderer> _meshRenderers = new List<MeshRenderer>();
	}
}
