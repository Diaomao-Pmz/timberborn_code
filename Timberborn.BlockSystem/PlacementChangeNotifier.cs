using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000055 RID: 85
	public class PlacementChangeNotifier : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000216 RID: 534 RVA: 0x00006C8C File Offset: 0x00004E8C
		public void Awake()
		{
			base.GetComponents<IPrePlacementChangeListener>(this._prePlacementChangeListeners);
			base.GetComponents<IPostPlacementChangeListener>(this._postPlacementChangeListeners);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00006CA8 File Offset: 0x00004EA8
		public void NotifyPreChangeListeners()
		{
			for (int i = 0; i < this._prePlacementChangeListeners.Count; i++)
			{
				this._prePlacementChangeListeners[i].OnPrePlacementChanged();
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00006CDC File Offset: 0x00004EDC
		public void NotifyPostChangeListeners()
		{
			for (int i = 0; i < this._postPlacementChangeListeners.Count; i++)
			{
				this._postPlacementChangeListeners[i].OnPostPlacementChanged();
			}
		}

		// Token: 0x040000FA RID: 250
		public readonly List<IPrePlacementChangeListener> _prePlacementChangeListeners = new List<IPrePlacementChangeListener>();

		// Token: 0x040000FB RID: 251
		public readonly List<IPostPlacementChangeListener> _postPlacementChangeListeners = new List<IPostPlacementChangeListener>();
	}
}
