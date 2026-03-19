using System;
using Timberborn.Localization;
using Timberborn.Modding;
using Timberborn.SerializationSystem;
using Timberborn.SteamWorkshop;
using Timberborn.TextureOperations;

namespace Timberborn.SteamWorkshopModUploadingUI
{
	// Token: 0x0200000A RID: 10
	public class SteamWorkshopUploadableModFactory
	{
		// Token: 0x06000034 RID: 52 RVA: 0x000027CD File Offset: 0x000009CD
		public SteamWorkshopUploadableModFactory(SteamWorkshopItemSerializer steamWorkshopItemSerializer, SerializedObjectReaderWriter serializedObjectReaderWriter, TextureFactory textureFactory, ILoc loc)
		{
			this._steamWorkshopItemSerializer = steamWorkshopItemSerializer;
			this._serializedObjectReaderWriter = serializedObjectReaderWriter;
			this._textureFactory = textureFactory;
			this._loc = loc;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000027F4 File Offset: 0x000009F4
		public SteamWorkshopUploadableMod Create(Mod mod)
		{
			SteamWorkshopModDataFile steamWorkshopModDataFile = SteamWorkshopModDataFile.Create(this._steamWorkshopItemSerializer, this._serializedObjectReaderWriter, mod.ModDirectory.OriginPath);
			SteamWorkshopModThumbnail steamWorkshopModThumbnail = new SteamWorkshopModThumbnail(this._textureFactory, mod.ModDirectory.OriginPath);
			steamWorkshopModThumbnail.UpdateThumbnail();
			return new SteamWorkshopUploadableMod(this._loc, steamWorkshopModDataFile, mod, steamWorkshopModThumbnail);
		}

		// Token: 0x04000020 RID: 32
		public readonly SteamWorkshopItemSerializer _steamWorkshopItemSerializer;

		// Token: 0x04000021 RID: 33
		public readonly SerializedObjectReaderWriter _serializedObjectReaderWriter;

		// Token: 0x04000022 RID: 34
		public readonly TextureFactory _textureFactory;

		// Token: 0x04000023 RID: 35
		public readonly ILoc _loc;
	}
}
