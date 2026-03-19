using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using UnityEngine;

namespace Timberborn.SelectionSystem
{
	// Token: 0x0200000E RID: 14
	public class Highlighter
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00002D23 File Offset: 0x00000F23
		public void HighlightPrimary(BaseComponent target, Color color)
		{
			if (target)
			{
				this.GetOrAdd(target, this._primaries).HighlightPrimary(this, color);
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002D41 File Offset: 0x00000F41
		public void HighlightSecondary(BaseComponent target, Color color)
		{
			if (target)
			{
				this.GetOrAdd(target, this._secondaries).HighlightSecondary(this, color);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002D60 File Offset: 0x00000F60
		public void UnhighlightPrimary(BaseComponent target)
		{
			if (target)
			{
				HighlightableObject orAdd = this.GetOrAdd(target, this._primaries);
				if (orAdd)
				{
					orAdd.UnhighlightPrimaryColor(this);
					this._primaries.Remove(target.GameObject);
				}
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002DA4 File Offset: 0x00000FA4
		public void UnhighlightSecondary(BaseComponent target)
		{
			if (target)
			{
				HighlightableObject orAdd = this.GetOrAdd(target, this._secondaries);
				if (orAdd)
				{
					orAdd.UnhighlightSecondaryColor(this);
					this._secondaries.Remove(target.GameObject);
				}
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002DE8 File Offset: 0x00000FE8
		public void UnhighlightAllPrimary()
		{
			foreach (HighlightableObject highlightableObject in this._primaries.Values)
			{
				if (highlightableObject)
				{
					highlightableObject.UnhighlightPrimaryColor(this);
				}
			}
			this._primaries.Clear();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002E54 File Offset: 0x00001054
		public void UnhighlightAllSecondary()
		{
			foreach (HighlightableObject highlightableObject in this._secondaries.Values)
			{
				if (highlightableObject)
				{
					highlightableObject.UnhighlightSecondaryColor(this);
				}
			}
			this._secondaries.Clear();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002EC0 File Offset: 0x000010C0
		public void ResetAllHighlights(BaseComponent target)
		{
			if (target)
			{
				this.GetOrAdd(target, this._primaries).ResetAllHighlights();
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002EDC File Offset: 0x000010DC
		public HighlightableObject GetOrAdd(BaseComponent target, IDictionary<GameObject, HighlightableObject> highlightableObjects)
		{
			HighlightableObject highlightableObject = target as HighlightableObject;
			if (highlightableObject != null)
			{
				return highlightableObject;
			}
			GameObject gameObject = target.GameObject;
			HighlightableObject result;
			if (highlightableObjects.TryGetValue(gameObject, out result))
			{
				return result;
			}
			HighlightableObject component = target.GetComponent<HighlightableObject>();
			highlightableObjects.Add(gameObject, component);
			return component;
		}

		// Token: 0x04000029 RID: 41
		public readonly Dictionary<GameObject, HighlightableObject> _primaries = new Dictionary<GameObject, HighlightableObject>();

		// Token: 0x0400002A RID: 42
		public readonly Dictionary<GameObject, HighlightableObject> _secondaries = new Dictionary<GameObject, HighlightableObject>();
	}
}
