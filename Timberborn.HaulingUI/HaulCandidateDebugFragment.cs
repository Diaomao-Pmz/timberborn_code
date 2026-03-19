using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Hauling;
using UnityEngine.UIElements;

namespace Timberborn.HaulingUI
{
	// Token: 0x02000006 RID: 6
	public class HaulCandidateDebugFragment : IEntityPanelFragment
	{
		// Token: 0x06000009 RID: 9 RVA: 0x0000216B File Offset: 0x0000036B
		public HaulCandidateDebugFragment(DebugFragmentFactory debugFragmentFactory)
		{
			this._debugFragmentFactory = debugFragmentFactory;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002190 File Offset: 0x00000390
		public VisualElement InitializeFragment()
		{
			this._root = this._debugFragmentFactory.Create("HaulCandidate");
			this._text = UQueryExtensions.Q<Label>(this._root, "Text", null);
			return this._root;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021C5 File Offset: 0x000003C5
		public void ShowFragment(BaseComponent entity)
		{
			this._haulCandidate = entity.GetComponent<HaulCandidate>();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021D3 File Offset: 0x000003D3
		public void ClearFragment()
		{
			this._haulCandidate = null;
			this.UpdateContent();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021E2 File Offset: 0x000003E2
		public void UpdateFragment()
		{
			this.UpdateContent();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021EC File Offset: 0x000003EC
		public void UpdateContent()
		{
			this._description.Clear();
			if (this._haulCandidate && this._haulCandidate.Enabled)
			{
				this.UpdateDescription();
			}
			this._root.ToggleDisplayStyle(this._description.Length > 0);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002240 File Offset: 0x00000440
		public void UpdateDescription()
		{
			this._haulCandidate.GetWeightedBehaviors(this._weightedBehaviors);
			foreach (WeightedBehavior weightedBehavior in this._weightedBehaviors)
			{
				this._description.AppendLine(string.Format("{0:F2} {1}", weightedBehavior.Weight, HaulCandidateDebugFragment.GetBehaviorName(weightedBehavior)));
			}
			this._weightedBehaviors.Clear();
			this._text.text = this._description.ToStringWithoutNewLineEnd();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022E8 File Offset: 0x000004E8
		public static string GetBehaviorName(WeightedBehavior weightedBehavior)
		{
			string text = weightedBehavior.WorkplaceBehavior.ToString().Split('.', StringSplitOptions.None).Last<string>();
			return text.Remove(text.Length - 1);
		}

		// Token: 0x0400000A RID: 10
		public readonly DebugFragmentFactory _debugFragmentFactory;

		// Token: 0x0400000B RID: 11
		public readonly StringBuilder _description = new StringBuilder();

		// Token: 0x0400000C RID: 12
		public HaulCandidate _haulCandidate;

		// Token: 0x0400000D RID: 13
		public Label _text;

		// Token: 0x0400000E RID: 14
		public VisualElement _root;

		// Token: 0x0400000F RID: 15
		public readonly List<WeightedBehavior> _weightedBehaviors = new List<WeightedBehavior>();
	}
}
