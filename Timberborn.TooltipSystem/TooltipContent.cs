using System;
using UnityEngine.UIElements;

namespace Timberborn.TooltipSystem
{
	// Token: 0x0200000A RID: 10
	public readonly struct TooltipContent
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002773 File Offset: 0x00000973
		public bool UpdatableContent { get; }

		// Token: 0x06000036 RID: 54 RVA: 0x0000277B File Offset: 0x0000097B
		public TooltipContent(Func<string> baseTextGetter, Func<VisualElement> visualElementGetter, bool instant, bool updatableContent = false, Func<string> keyBindingGetter = null)
		{
			this._baseTextGetter = baseTextGetter;
			this._visualElementGetter = visualElementGetter;
			this._instant = instant;
			this.UpdatableContent = updatableContent;
			this._keyBindingGetter = keyBindingGetter;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000027A2 File Offset: 0x000009A2
		public static TooltipContent Create(Func<string> baseTextGetter)
		{
			return new TooltipContent(baseTextGetter, null, false, false, null);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000027AE File Offset: 0x000009AE
		public static TooltipContent CreateWithKeyBinding(string baseText, Func<string> keyBindingGetter)
		{
			return new TooltipContent(() => baseText, null, false, false, keyBindingGetter);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000027D0 File Offset: 0x000009D0
		public static TooltipContent CreateUpdatable(Func<string> baseTextGetter)
		{
			return new TooltipContent(baseTextGetter, null, false, true, null);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000027DC File Offset: 0x000009DC
		public static TooltipContent Create(Func<VisualElement> visualElementGetter)
		{
			return new TooltipContent(null, visualElementGetter, false, false, null);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000027E8 File Offset: 0x000009E8
		public static TooltipContent CreateInstant(Func<VisualElement> visualElementGetter)
		{
			return new TooltipContent(null, visualElementGetter, true, false, null);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000027F4 File Offset: 0x000009F4
		public static TooltipContent CreateInstant(string baseText)
		{
			return new TooltipContent(() => baseText, null, true, false, null);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002816 File Offset: 0x00000A16
		public static TooltipContent CreateEmpty()
		{
			return new TooltipContent(null, null, false, false, null);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002822 File Offset: 0x00000A22
		public string BaseText
		{
			get
			{
				Func<string> baseTextGetter = this._baseTextGetter;
				if (baseTextGetter == null)
				{
					return null;
				}
				return baseTextGetter();
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002835 File Offset: 0x00000A35
		public VisualElement VisualElement
		{
			get
			{
				Func<VisualElement> visualElementGetter = this._visualElementGetter;
				if (visualElementGetter == null)
				{
					return null;
				}
				return visualElementGetter();
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002848 File Offset: 0x00000A48
		public float Delay
		{
			get
			{
				if (!this._instant)
				{
					return TooltipContent.DefaultDelay;
				}
				return 0f;
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000285D File Offset: 0x00000A5D
		public bool TryGetKeyBinding(out string keyBinding)
		{
			if (this._keyBindingGetter != null)
			{
				keyBinding = this._keyBindingGetter();
				return !string.IsNullOrWhiteSpace(keyBinding);
			}
			keyBinding = null;
			return false;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002883 File Offset: 0x00000A83
		public bool HasContent()
		{
			return !string.IsNullOrWhiteSpace(this.BaseText) || this.VisualElement != null;
		}

		// Token: 0x04000021 RID: 33
		public static readonly float DefaultDelay = 0.3f;

		// Token: 0x04000023 RID: 35
		public readonly Func<string> _baseTextGetter;

		// Token: 0x04000024 RID: 36
		public readonly Func<string> _keyBindingGetter;

		// Token: 0x04000025 RID: 37
		public readonly Func<VisualElement> _visualElementGetter;

		// Token: 0x04000026 RID: 38
		public readonly bool _instant;
	}
}
