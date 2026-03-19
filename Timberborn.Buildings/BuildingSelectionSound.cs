using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.CoreSound;
using Timberborn.SelectionSystem;
using Timberborn.SoundSystem;

namespace Timberborn.Buildings
{
	// Token: 0x02000014 RID: 20
	public class BuildingSelectionSound : BaseComponent, IAwakableComponent, ISelectionListener
	{
		// Token: 0x0600009D RID: 157 RVA: 0x0000346B File Offset: 0x0000166B
		public BuildingSelectionSound(ISoundSystem soundSystem)
		{
			this._soundSystem = soundSystem;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000347A File Offset: 0x0000167A
		public void Awake()
		{
			this._buildingSpec = base.GetComponent<BuildingSpec>();
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003494 File Offset: 0x00001694
		public void OnSelect()
		{
			string text = this._blockObject.IsFinished ? this._buildingSpec.SelectionSoundName : "Default";
			this._soundSystem.SetCustomMixer(base.GameObject, text, MixerNames.UIMixerNameKey);
			this._soundSystem.PlaySound2D(base.GameObject, "UI.Buildings.Selected." + text, 10);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000021B9 File Offset: 0x000003B9
		public void OnUnselect()
		{
		}

		// Token: 0x0400002F RID: 47
		public readonly ISoundSystem _soundSystem;

		// Token: 0x04000030 RID: 48
		public BuildingSpec _buildingSpec;

		// Token: 0x04000031 RID: 49
		public BlockObject _blockObject;
	}
}
