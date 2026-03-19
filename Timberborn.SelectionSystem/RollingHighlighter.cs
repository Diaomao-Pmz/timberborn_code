using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using UnityEngine;

namespace Timberborn.SelectionSystem
{
	// Token: 0x02000014 RID: 20
	public class RollingHighlighter
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00003239 File Offset: 0x00001439
		public RollingHighlighter(Highlighter highlighter)
		{
			this._highlighter = highlighter;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000325E File Offset: 0x0000145E
		public void HighlightPrimary(BaseComponent component, Color color)
		{
			this.Swap();
			this._current.Add(component);
			this.HighlightPrimary(color);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000327C File Offset: 0x0000147C
		public void HighlightPrimary(IEnumerable<BaseComponent> components, Color color)
		{
			this.Swap();
			foreach (BaseComponent item in components)
			{
				this._current.Add(item);
			}
			this.HighlightPrimary(color);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000032D8 File Offset: 0x000014D8
		public void UnhighlightAllPrimary()
		{
			this._highlighter.UnhighlightAllPrimary();
			this._current.Clear();
			this._previous.Clear();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000032FC File Offset: 0x000014FC
		public void Swap()
		{
			HashSet<BaseComponent> current = this._current;
			HashSet<BaseComponent> previous = this._previous;
			this._previous = current;
			this._current = previous;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003328 File Offset: 0x00001528
		public void HighlightPrimary(Color color)
		{
			foreach (BaseComponent baseComponent in this._current)
			{
				if (this._previous.Contains(baseComponent))
				{
					this._previous.Remove(baseComponent);
				}
				else
				{
					this._highlighter.HighlightPrimary(baseComponent, color);
				}
			}
			foreach (BaseComponent target in this._previous)
			{
				this._highlighter.UnhighlightPrimary(target);
			}
			this._previous.Clear();
		}

		// Token: 0x04000035 RID: 53
		public readonly Highlighter _highlighter;

		// Token: 0x04000036 RID: 54
		public HashSet<BaseComponent> _current = new HashSet<BaseComponent>();

		// Token: 0x04000037 RID: 55
		public HashSet<BaseComponent> _previous = new HashSet<BaseComponent>();
	}
}
