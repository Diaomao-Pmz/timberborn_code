using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CharacterModelSystem;

namespace Timberborn.BeaverContaminationSystem
{
	// Token: 0x02000007 RID: 7
	public class ContaminableAnimator : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002389 File Offset: 0x00000589
		public void Awake()
		{
			this._characterAnimator = base.GetComponent<CharacterAnimator>();
			base.GetComponent<Contaminable>().ContaminationChanged += this.OnContaminationChanged;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023B0 File Offset: 0x000005B0
		public void OnContaminationChanged(object sender, EventArgs e)
		{
			bool isContaminated = ((Contaminable)sender).IsContaminated;
			this._characterAnimator.SetBool(ContaminableAnimator.ContaminatedParameterName, isContaminated);
		}

		// Token: 0x0400000C RID: 12
		public static readonly string ContaminatedParameterName = "Contaminated";

		// Token: 0x0400000D RID: 13
		public CharacterAnimator _characterAnimator;
	}
}
