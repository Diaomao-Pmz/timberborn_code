using System;
using Timberborn.BaseComponentSystem;
using Timberborn.StatusSystem;

namespace Timberborn.EnterableSystem
{
	// Token: 0x02000018 RID: 24
	public class EntererStatusIconHider : BaseComponent, IStartableComponent
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x0000361C File Offset: 0x0000181C
		public void Start()
		{
			StatusIconCycler componentInChildren = base.GetComponentInChildren<StatusIconCycler>(true);
			this._statusVisibilityToggle = componentInChildren.GetStatusVisibilityToggle();
			Enterer component = base.GetComponent<Enterer>();
			if (component.IsInside)
			{
				this.HideStatusIcons(component.CurrentBuilding);
			}
			component.EnteredEnterable += this.OnEnteredEnterable;
			component.ExitedEnterable += this.OnExitedEnterable;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000367C File Offset: 0x0000187C
		public void OnEnteredEnterable(object sender, EnteredEnterableEventArgs e)
		{
			this.HideStatusIcons(e.Enterable);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000368A File Offset: 0x0000188A
		public void OnExitedEnterable(object sender, EventArgs e)
		{
			this.ShowStatusIcons();
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003692 File Offset: 0x00001892
		public void HideStatusIcons(Enterable enterable)
		{
			if (enterable.GetComponent<IStatusHider>() != null)
			{
				this._statusVisibilityToggle.Hide();
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000036A7 File Offset: 0x000018A7
		public void ShowStatusIcons()
		{
			this._statusVisibilityToggle.Show();
		}

		// Token: 0x04000033 RID: 51
		public StatusVisibilityToggle _statusVisibilityToggle;
	}
}
