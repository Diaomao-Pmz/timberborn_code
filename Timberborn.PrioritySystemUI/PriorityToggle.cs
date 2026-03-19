using System;
using Timberborn.PrioritySystem;
using UnityEngine.UIElements;

namespace Timberborn.PrioritySystemUI
{
	// Token: 0x0200000B RID: 11
	public class PriorityToggle
	{
		// Token: 0x0600002E RID: 46 RVA: 0x000027D6 File Offset: 0x000009D6
		public PriorityToggle(Priority priority, Toggle toggle)
		{
			this._priority = priority;
			this._toggle = toggle;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000027EC File Offset: 0x000009EC
		public void Initialize()
		{
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._toggle, new EventCallback<ChangeEvent<bool>>(this.OnValueChanged));
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002808 File Offset: 0x00000A08
		public void UpdateState()
		{
			if (this._prioritizable != null)
			{
				bool flag = this._priority == this._prioritizable.Priority;
				this._toggle.SetValueWithoutNotify(flag);
				this.UpdateImage(flag);
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002844 File Offset: 0x00000A44
		public void Enable(IPrioritizable prioritizable)
		{
			this._prioritizable = prioritizable;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000284D File Offset: 0x00000A4D
		public void Disable()
		{
			this._prioritizable = null;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002856 File Offset: 0x00000A56
		public void OnValueChanged(ChangeEvent<bool> changeEvent)
		{
			if (changeEvent.newValue)
			{
				this._prioritizable.SetPriority(this._priority);
			}
			this.UpdateImage(changeEvent.newValue);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000287D File Offset: 0x00000A7D
		public void UpdateImage(bool state)
		{
			this._toggle.EnableInClassList(PriorityToggle.CheckedClass, state);
		}

		// Token: 0x04000014 RID: 20
		public static readonly string CheckedClass = "priority-toggle--checked";

		// Token: 0x04000015 RID: 21
		public readonly Priority _priority;

		// Token: 0x04000016 RID: 22
		public readonly Toggle _toggle;

		// Token: 0x04000017 RID: 23
		public IPrioritizable _prioritizable;
	}
}
