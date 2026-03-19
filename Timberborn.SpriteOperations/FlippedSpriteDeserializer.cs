using System;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.SpriteOperations
{
	// Token: 0x02000008 RID: 8
	public class FlippedSpriteDeserializer : IDeserializer
	{
		// Token: 0x06000015 RID: 21 RVA: 0x0000224C File Offset: 0x0000044C
		public FlippedSpriteDeserializer(SpriteFlipper spriteFlipper)
		{
			this._spriteFlipper = spriteFlipper;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000210D File Offset: 0x0000030D
		public Type DeserializedType
		{
			get
			{
				return typeof(FlippedSprite);
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000225C File Offset: 0x0000045C
		public object Deserialize(object source)
		{
			Sprite asset = ((AssetRef<Sprite>)source).Asset;
			return new FlippedSprite(this._spriteFlipper.GetFlippedSprite(asset));
		}

		// Token: 0x04000009 RID: 9
		public readonly SpriteFlipper _spriteFlipper;
	}
}
