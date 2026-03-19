using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000050 RID: 80
	public class TimerModel : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x06000351 RID: 849 RVA: 0x000094A4 File Offset: 0x000076A4
		public void Awake()
		{
			this._timerModelSpec = base.GetComponent<TimerModelSpec>();
			this._timer = base.GetComponent<Timer>();
			this._progressObject = base.GameObject.FindChild(this._timerModelSpec.ProgressObjectName);
			this._barHeight = this._timerModelSpec.MaxHeight - this._timerModelSpec.MinHeight;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00009502 File Offset: 0x00007702
		public void OnEnterFinishedState()
		{
			this._timer.TimerTicked += this.OnTimerTicked;
			this.UpdateProgressObject();
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000387E File Offset: 0x00001A7E
		public void OnExitFinishedState()
		{
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00009521 File Offset: 0x00007721
		public void OnTimerTicked(object sender, EventArgs e)
		{
			this.UpdateProgressObject();
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000952C File Offset: 0x0000772C
		public void UpdateProgressObject()
		{
			bool flag;
			float num = Mathf.Clamp01(this._timer.GetProgress(out flag));
			float num2 = flag ? (1f - num) : num;
			Vector3 localScale = this._progressObject.transform.localScale;
			this._progressObject.transform.localScale = new Vector3(localScale.x, num2, localScale.z);
			float num3 = flag ? (this._timerModelSpec.MinHeight + this._barHeight * num) : this._timerModelSpec.MinHeight;
			Vector3 localPosition = this._progressObject.transform.localPosition;
			this._progressObject.transform.localPosition = new Vector3(localPosition.x, num3, localPosition.z);
		}

		// Token: 0x040001A0 RID: 416
		public TimerModelSpec _timerModelSpec;

		// Token: 0x040001A1 RID: 417
		public Timer _timer;

		// Token: 0x040001A2 RID: 418
		public GameObject _progressObject;

		// Token: 0x040001A3 RID: 419
		public float _barHeight;
	}
}
