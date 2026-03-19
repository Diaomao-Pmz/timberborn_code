using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.Rendering;
using UnityEngine;

namespace Timberborn.SelectionSystem
{
	// Token: 0x0200000C RID: 12
	public class HighlightableObject : BaseComponent, IDeletableEntity
	{
		// Token: 0x0600004A RID: 74 RVA: 0x00002A10 File Offset: 0x00000C10
		public HighlightableObject(MaterialColorer materialColorer, HighlightRenderingService highlightRenderingService)
		{
			this._materialColorer = materialColorer;
			this._highlightRenderingService = highlightRenderingService;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002A3C File Offset: 0x00000C3C
		public void DeleteEntity()
		{
			if (this._isHighlighted)
			{
				this._highlightRenderingService.RemoveFromHighlight(base.GameObject);
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002A57 File Offset: 0x00000C57
		public void HighlightPrimary(Highlighter highlighter, Color color)
		{
			this.HighlightColor(this._primaryColors, highlighter, color);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002A67 File Offset: 0x00000C67
		public void HighlightSecondary(Highlighter highlighter, Color color)
		{
			this.HighlightColor(this._secondaryColors, highlighter, color);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002A77 File Offset: 0x00000C77
		public void UnhighlightPrimaryColor(Highlighter highlighter)
		{
			HighlightableObject.RemoveHighlighterColor(this._primaryColors, highlighter);
			this.UpdateColorAndHighlight();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002A8B File Offset: 0x00000C8B
		public void UnhighlightSecondaryColor(Highlighter highlighter)
		{
			HighlightableObject.RemoveHighlighterColor(this._secondaryColors, highlighter);
			this.UpdateColorAndHighlight();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002AA0 File Offset: 0x00000CA0
		public void UpdateColorAndHighlight()
		{
			Color emissionColor;
			if (this.HasHighlightColor(out emissionColor))
			{
				this._materialColorer.SetEmissionColor(this, emissionColor);
				this._highlightRenderingService.AddToHighlight(base.GameObject);
				this._isHighlighted = true;
				return;
			}
			this._materialColorer.ResetEmissionColor(this);
			this._highlightRenderingService.RemoveFromHighlight(base.GameObject);
			this._isHighlighted = false;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002B01 File Offset: 0x00000D01
		public void ResetAllHighlights()
		{
			this._primaryColors.Clear();
			this._secondaryColors.Clear();
			this.UpdateColorAndHighlight();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002B20 File Offset: 0x00000D20
		public void RefreshHighlight()
		{
			Color emissionColor;
			if (this._isHighlighted && this.HasHighlightColor(out emissionColor))
			{
				this._materialColorer.SetEmissionColor(this, emissionColor);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002B4C File Offset: 0x00000D4C
		public void HighlightColor(IList<HighlightableObject.HighlighterColor> highlighterColors, Highlighter highlighter, Color color)
		{
			HighlightableObject.HighlighterColor item;
			if (!HighlightableObject.TryGetHighlighterColor(highlighterColors, highlighter, out item) || item.Color != color)
			{
				highlighterColors.Remove(item);
				HighlightableObject.HighlighterColor item2 = new HighlightableObject.HighlighterColor(highlighter, color);
				highlighterColors.Add(item2);
				this.UpdateColorAndHighlight();
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002B94 File Offset: 0x00000D94
		public static void RemoveHighlighterColor(IList<HighlightableObject.HighlighterColor> highlighterColors, Highlighter highlighter)
		{
			for (int i = 0; i < highlighterColors.Count; i++)
			{
				if (highlighterColors[i].Highlighter == highlighter)
				{
					highlighterColors.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002BCC File Offset: 0x00000DCC
		public bool HasHighlightColor(out Color color)
		{
			if (this._primaryColors.Count > 0)
			{
				color = this._primaryColors.Last<HighlightableObject.HighlighterColor>().Color;
				return true;
			}
			if (this._secondaryColors.Count > 0)
			{
				color = this._secondaryColors.Last<HighlightableObject.HighlighterColor>().Color;
				return true;
			}
			color = default(Color);
			return false;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002C34 File Offset: 0x00000E34
		public static bool TryGetHighlighterColor(IList<HighlightableObject.HighlighterColor> highlighterColors, Highlighter highlighter, out HighlightableObject.HighlighterColor highlighterColor)
		{
			for (int i = 0; i < highlighterColors.Count; i++)
			{
				if (highlighterColors[i].Highlighter == highlighter)
				{
					highlighterColor = highlighterColors[i];
					return true;
				}
			}
			highlighterColor = default(HighlightableObject.HighlighterColor);
			return false;
		}

		// Token: 0x04000022 RID: 34
		public readonly MaterialColorer _materialColorer;

		// Token: 0x04000023 RID: 35
		public readonly HighlightRenderingService _highlightRenderingService;

		// Token: 0x04000024 RID: 36
		public readonly List<HighlightableObject.HighlighterColor> _primaryColors = new List<HighlightableObject.HighlighterColor>();

		// Token: 0x04000025 RID: 37
		public readonly List<HighlightableObject.HighlighterColor> _secondaryColors = new List<HighlightableObject.HighlighterColor>();

		// Token: 0x04000026 RID: 38
		public bool _isHighlighted;

		// Token: 0x0200000D RID: 13
		public readonly struct HighlighterColor : IEquatable<HighlightableObject.HighlighterColor>
		{
			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000057 RID: 87 RVA: 0x00002C7B File Offset: 0x00000E7B
			public Highlighter Highlighter { get; }

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x06000058 RID: 88 RVA: 0x00002C83 File Offset: 0x00000E83
			public Color Color { get; }

			// Token: 0x06000059 RID: 89 RVA: 0x00002C8B File Offset: 0x00000E8B
			public HighlighterColor(Highlighter highlighter, Color color)
			{
				this.Highlighter = highlighter;
				this.Color = color;
			}

			// Token: 0x0600005A RID: 90 RVA: 0x00002C9C File Offset: 0x00000E9C
			public bool Equals(HighlightableObject.HighlighterColor other)
			{
				return object.Equals(this.Highlighter, other.Highlighter) && this.Color.Equals(other.Color);
			}

			// Token: 0x0600005B RID: 91 RVA: 0x00002CD4 File Offset: 0x00000ED4
			public override bool Equals(object obj)
			{
				if (obj is HighlightableObject.HighlighterColor)
				{
					HighlightableObject.HighlighterColor other = (HighlightableObject.HighlighterColor)obj;
					return this.Equals(other);
				}
				return false;
			}

			// Token: 0x0600005C RID: 92 RVA: 0x00002CF9 File Offset: 0x00000EF9
			public override int GetHashCode()
			{
				return HashCode.Combine<Highlighter, Color>(this.Highlighter, this.Color);
			}

			// Token: 0x0600005D RID: 93 RVA: 0x00002D0C File Offset: 0x00000F0C
			public static bool operator ==(HighlightableObject.HighlighterColor left, HighlightableObject.HighlighterColor right)
			{
				return left.Equals(right);
			}

			// Token: 0x0600005E RID: 94 RVA: 0x00002D16 File Offset: 0x00000F16
			public static bool operator !=(HighlightableObject.HighlighterColor left, HighlightableObject.HighlighterColor right)
			{
				return !left.Equals(right);
			}
		}
	}
}
