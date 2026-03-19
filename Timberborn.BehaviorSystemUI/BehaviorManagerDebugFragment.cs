using System;
using System.Linq;
using System.Text;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using UnityEngine.UIElements;

namespace Timberborn.BehaviorSystemUI
{
	// Token: 0x02000004 RID: 4
	public class BehaviorManagerDebugFragment : IEntityPanelFragment
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public BehaviorManagerDebugFragment(DebugFragmentFactory debugFragmentFactory)
		{
			this._debugFragmentFactory = debugFragmentFactory;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020CD File Offset: 0x000002CD
		public VisualElement InitializeFragment()
		{
			this._root = this._debugFragmentFactory.Create("BehaviorManager");
			this._text = UQueryExtensions.Q<Label>(this._root, "Text", null);
			return this._root;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002102 File Offset: 0x00000302
		public void ShowFragment(BaseComponent entity)
		{
			this._behaviorManager = entity.GetComponent<BehaviorManager>();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002110 File Offset: 0x00000310
		public void ClearFragment()
		{
			this._behaviorManager = null;
			this.UpdateDescriptions();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000211F File Offset: 0x0000031F
		public void UpdateFragment()
		{
			this.UpdateDescriptions();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002128 File Offset: 0x00000328
		public void UpdateDescriptions()
		{
			if (this._behaviorManager && this._behaviorManager.Enabled)
			{
				StringBuilder stringBuilder = new StringBuilder();
				BehaviorInfo runningBehavior = this._behaviorManager.RunningBehavior;
				ExecutorInfo runningExecutor = this._behaviorManager.RunningExecutor;
				stringBuilder.AppendLine("Active behavior: " + runningBehavior.Name);
				stringBuilder.AppendLine(string.Format("Active executor: {0} {1:0.0}s", runningExecutor.Name, runningExecutor.ElapsedTime));
				stringBuilder.AppendLine("Behavior log:");
				foreach (string text in this._behaviorManager.TimestampedBehaviorLog.Reverse<string>())
				{
					stringBuilder.AppendLine(text ?? "");
				}
				this._text.text = stringBuilder.ToStringWithoutNewLineEnd();
				this._root.ToggleDisplayStyle(true);
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x04000006 RID: 6
		public readonly DebugFragmentFactory _debugFragmentFactory;

		// Token: 0x04000007 RID: 7
		public BehaviorManager _behaviorManager;

		// Token: 0x04000008 RID: 8
		public Label _text;

		// Token: 0x04000009 RID: 9
		public VisualElement _root;
	}
}
