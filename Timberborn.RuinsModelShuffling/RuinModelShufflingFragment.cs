using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Ruins;
using Timberborn.UndoSystem;
using UnityEngine.UIElements;

namespace Timberborn.RuinsModelShuffling
{
	// Token: 0x02000004 RID: 4
	public class RuinModelShufflingFragment : IEntityPanelFragment
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public RuinModelShufflingFragment(VisualElementLoader visualElementLoader, RuinReplacer ruinReplacer, IUndoRegistry undoRegistry)
		{
			this._visualElementLoader = visualElementLoader;
			this._ruinReplacer = ruinReplacer;
			this._undoRegistry = undoRegistry;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020DC File Offset: 0x000002DC
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("MapEditor/EntityPanel/RuinModelShufflingFragment");
			this._button = UQueryExtensions.Q<Button>(this._root, "Button", null);
			this._button.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.ShuffleModel), 0);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002140 File Offset: 0x00000340
		public void ShowFragment(BaseComponent entity)
		{
			this._ruin = entity.GetComponent<Ruin>();
			if (this._ruin)
			{
				this._root.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002167 File Offset: 0x00000367
		public void ClearFragment()
		{
			this._ruin = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000217C File Offset: 0x0000037C
		public void UpdateFragment()
		{
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000217E File Offset: 0x0000037E
		public void ShuffleModel(ClickEvent evt)
		{
			this._ruinReplacer.Shuffle(this._ruin);
			this._undoRegistry.CommitStack();
		}

		// Token: 0x04000006 RID: 6
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000007 RID: 7
		public readonly RuinReplacer _ruinReplacer;

		// Token: 0x04000008 RID: 8
		public readonly IUndoRegistry _undoRegistry;

		// Token: 0x04000009 RID: 9
		public Ruin _ruin;

		// Token: 0x0400000A RID: 10
		public Button _button;

		// Token: 0x0400000B RID: 11
		public VisualElement _root;
	}
}
