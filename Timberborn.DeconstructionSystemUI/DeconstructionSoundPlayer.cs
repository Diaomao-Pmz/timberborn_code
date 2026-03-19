using System;
using Timberborn.DeconstructionSystem;
using Timberborn.SingletonSystem;
using Timberborn.UISound;

namespace Timberborn.DeconstructionSystemUI
{
	// Token: 0x02000007 RID: 7
	public class DeconstructionSoundPlayer : ILoadableSingleton, IUpdatableSingleton
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002313 File Offset: 0x00000513
		public DeconstructionSoundPlayer(EventBus eventBus, UISoundController uiSoundController)
		{
			this._eventBus = eventBus;
			this._uiSoundController = uiSoundController;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002329 File Offset: 0x00000529
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002337 File Offset: 0x00000537
		public void UpdateSingleton()
		{
			if (this._shouldPlaySound)
			{
				this._shouldPlaySound = false;
				this._uiSoundController.PlaySound(DeconstructionSoundPlayer.DeconstructionSoundName);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002358 File Offset: 0x00000558
		[OnEvent]
		public void OnBuildingDeconstructed(BuildingDeconstructedEvent buildingDeconstructedEvent)
		{
			this._shouldPlaySound = true;
		}

		// Token: 0x04000012 RID: 18
		public static readonly string DeconstructionSoundName = "UI.Buildings.Deconstruction";

		// Token: 0x04000013 RID: 19
		public readonly EventBus _eventBus;

		// Token: 0x04000014 RID: 20
		public readonly UISoundController _uiSoundController;

		// Token: 0x04000015 RID: 21
		public bool _shouldPlaySound;
	}
}
