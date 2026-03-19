using System;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.SpriteOperations
{
	// Token: 0x02000010 RID: 16
	public class UISpriteDeserializer : IDeserializer
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002888 File Offset: 0x00000A88
		public UISpriteDeserializer(SpriteResizer spriteResizer)
		{
			this._spriteResizer = spriteResizer;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000041 RID: 65 RVA: 0x0000274A File Offset: 0x0000094A
		public Type DeserializedType
		{
			get
			{
				return typeof(UISprite);
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002898 File Offset: 0x00000A98
		public object Deserialize(object source)
		{
			Sprite asset = ((AssetRef<Sprite>)source).Asset;
			return new UISprite(this._spriteResizer.GetResizedSprite(asset, UISpriteDeserializer.UISpriteSize));
		}

		// Token: 0x04000016 RID: 22
		public static readonly int UISpriteSize = 24;

		// Token: 0x04000017 RID: 23
		public readonly SpriteResizer _spriteResizer;
	}
}
