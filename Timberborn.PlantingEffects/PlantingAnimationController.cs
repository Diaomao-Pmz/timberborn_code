using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CharacterModelSystem;
using Timberborn.Planting;

namespace Timberborn.PlantingEffects
{
	// Token: 0x02000007 RID: 7
	public class PlantingAnimationController : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public void Awake()
		{
			this._characterAnimator = base.GetComponent<CharacterAnimator>();
			PlantExecutor component = base.GetComponent<PlantExecutor>();
			component.PlantingStarted += this.OnPlantingStarted;
			component.PlantingFinished += this.OnPlantingFinished;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002135 File Offset: 0x00000335
		public void OnPlantingStarted(object sender, EventArgs e)
		{
			this._characterAnimator.SetBool(PlantingAnimationController.PlantingAnimation, true);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002148 File Offset: 0x00000348
		public void OnPlantingFinished(object sender, EventArgs e)
		{
			this._characterAnimator.SetBool(PlantingAnimationController.PlantingAnimation, false);
		}

		// Token: 0x04000008 RID: 8
		public static readonly string PlantingAnimation = "Planting";

		// Token: 0x04000009 RID: 9
		public CharacterAnimator _characterAnimator;
	}
}
