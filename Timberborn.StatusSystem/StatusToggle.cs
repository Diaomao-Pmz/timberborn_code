using System;

namespace Timberborn.StatusSystem
{
	// Token: 0x02000028 RID: 40
	public class StatusToggle
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060000FC RID: 252 RVA: 0x000049B0 File Offset: 0x00002BB0
		// (remove) Token: 0x060000FD RID: 253 RVA: 0x000049E8 File Offset: 0x00002BE8
		public event EventHandler<EventArgs> StatusToggled;

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00004A1D File Offset: 0x00002C1D
		public StatusSpecification StatusSpecification { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00004A25 File Offset: 0x00002C25
		public bool IsPriorityStatus { get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00004A2D File Offset: 0x00002C2D
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00004A35 File Offset: 0x00002C35
		public bool IsActive { get; private set; }

		// Token: 0x06000102 RID: 258 RVA: 0x00004A3E File Offset: 0x00002C3E
		public StatusToggle(StatusSpecification statusSpecification, bool isPriorityStatus)
		{
			this.StatusSpecification = statusSpecification;
			this.IsPriorityStatus = isPriorityStatus;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004A54 File Offset: 0x00002C54
		public static StatusToggle CreateNormalStatus(string spriteName, string description)
		{
			return new StatusToggle(StatusSpecification.Create(spriteName, description), false);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004A63 File Offset: 0x00002C63
		public static StatusToggle CreatePriorityStatusWithFloatingIcon(string spriteName, string description, float delayInHours = 0f)
		{
			return new StatusToggle(StatusSpecification.CreateWithIcon(spriteName, description, delayInHours), true);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004A73 File Offset: 0x00002C73
		public static StatusToggle CreatePriorityStatusWithAlertAndFloatingIcon(string spriteName, string statusDescription, string alertDescription, float delayInHours = 0f)
		{
			return new StatusToggle(StatusSpecification.CreateWithAlertAndIcon(spriteName, statusDescription, alertDescription, delayInHours), true);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004A84 File Offset: 0x00002C84
		public static StatusToggle CreateNormalStatusWithFloatingIcon(string spriteName, string description, float delayInHours = 0f)
		{
			return new StatusToggle(StatusSpecification.CreateWithIcon(spriteName, description, delayInHours), false);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004A94 File Offset: 0x00002C94
		public static StatusToggle CreateNormalStatusWithAlert(string spriteName, string statusDescription, string alertDescription, float delayInHours = 0f)
		{
			return new StatusToggle(StatusSpecification.CreateWithAlert(spriteName, statusDescription, alertDescription, delayInHours), false);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004AA5 File Offset: 0x00002CA5
		public static StatusToggle CreateNormalStatusWithAlertAndFloatingIcon(string spriteName, string statusDescription, string alertDescription, float delayInHours = 0f)
		{
			return new StatusToggle(StatusSpecification.CreateWithAlertAndIcon(spriteName, statusDescription, alertDescription, delayInHours), false);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004AB6 File Offset: 0x00002CB6
		public void Activate()
		{
			this.Toggle(true);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004ABF File Offset: 0x00002CBF
		public void Deactivate()
		{
			this.Toggle(false);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004AC8 File Offset: 0x00002CC8
		public void Toggle(bool isActive)
		{
			if (this.IsActive != isActive)
			{
				this.IsActive = isActive;
				EventHandler<EventArgs> statusToggled = this.StatusToggled;
				if (statusToggled == null)
				{
					return;
				}
				statusToggled(this, EventArgs.Empty);
			}
		}
	}
}
