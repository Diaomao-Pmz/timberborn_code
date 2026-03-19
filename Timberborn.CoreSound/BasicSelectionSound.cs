using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.SelectionSystem;
using Timberborn.SoundSystem;

namespace Timberborn.CoreSound
{
	// Token: 0x02000007 RID: 7
	public class BasicSelectionSound : BaseComponent, IAwakableComponent, ISelectionListener
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public BasicSelectionSound(ISoundSystem soundSystem, IRandomNumberGenerator randomNumberGenerator)
		{
			this._soundSystem = soundSystem;
			this._randomNumberGenerator = randomNumberGenerator;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002114 File Offset: 0x00000314
		public void Awake()
		{
			this._basicSelectionSoundSpec = base.GetComponent<BasicSelectionSoundSpec>();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002122 File Offset: 0x00000322
		public void OnSelect()
		{
			this._soundSystem.PlaySound2D(base.GameObject, "UI.BasicSelection." + this.GetSoundName(), 10);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002147 File Offset: 0x00000347
		public void OnUnselect()
		{
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000214C File Offset: 0x0000034C
		public string GetSoundName()
		{
			if (!string.IsNullOrEmpty(this._basicSelectionSoundSpec.AlternativeSoundName) && this._randomNumberGenerator.Range(0f, 1f) < 0.1f)
			{
				return this._basicSelectionSoundSpec.AlternativeSoundName + ".AltSound";
			}
			return this._basicSelectionSoundSpec.SoundName;
		}

		// Token: 0x04000008 RID: 8
		public readonly ISoundSystem _soundSystem;

		// Token: 0x04000009 RID: 9
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x0400000A RID: 10
		public BasicSelectionSoundSpec _basicSelectionSoundSpec;
	}
}
