using System;

namespace Timberborn.Illumination
{
	// Token: 0x02000017 RID: 23
	public class IlluminatorToggle
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x000035D5 File Offset: 0x000017D5
		public IlluminatorToggle(Illuminator illuminator)
		{
			this._illuminator = illuminator;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000035E4 File Offset: 0x000017E4
		public void TurnOn()
		{
			if (!this._isOn)
			{
				this._illuminator.IncrementTurnedOnToggles();
				this._isOn = true;
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003600 File Offset: 0x00001800
		public void TurnOff()
		{
			if (this._isOn)
			{
				this._illuminator.DecrementTurnedOnToggles();
				this._isOn = false;
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000361C File Offset: 0x0000181C
		public void Toggle(bool value)
		{
			if (value)
			{
				this.TurnOn();
				return;
			}
			this.TurnOff();
		}

		// Token: 0x0400003A RID: 58
		public readonly Illuminator _illuminator;

		// Token: 0x0400003B RID: 59
		public bool _isOn;
	}
}
