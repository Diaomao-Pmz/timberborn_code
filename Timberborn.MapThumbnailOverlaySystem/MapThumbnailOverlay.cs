using System;
using System.IO;
using Timberborn.MapEditorPersistence;
using Timberborn.MapRepositorySystem;
using Timberborn.MapThumbnail;
using Timberborn.SingletonSystem;
using Timberborn.ThumbnailCapturing;
using Timberborn.UndoSystem;
using UnityEngine;

namespace Timberborn.MapThumbnailOverlaySystem
{
	// Token: 0x02000004 RID: 4
	public class MapThumbnailOverlay : ILoadableSingleton, IUnloadableSingleton
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public Texture2D Overlay { get; private set; }

		// Token: 0x06000005 RID: 5 RVA: 0x000020CF File Offset: 0x000002CF
		public MapThumbnailOverlay(MapThumbnailConfiguration mapThumbnailConfiguration, IThumbnailRenderTextureProvider thumbnailRenderTextureProvider, MapEditorMapLoader mapEditorMapLoader, MapDeserializer mapDeserializer, MapThumbnailOverlaySerializer mapThumbnailOverlaySerializer, IUndoRegistry undoRegistry, EventBus eventBus)
		{
			this._mapThumbnailConfiguration = mapThumbnailConfiguration;
			this._thumbnailRenderTextureProvider = thumbnailRenderTextureProvider;
			this._mapEditorMapLoader = mapEditorMapLoader;
			this._mapDeserializer = mapDeserializer;
			this._mapThumbnailOverlaySerializer = mapThumbnailOverlaySerializer;
			this._undoRegistry = undoRegistry;
			this._eventBus = eventBus;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000210C File Offset: 0x0000030C
		public void Load()
		{
			if (this._mapEditorMapLoader.LoadedMap != null)
			{
				MapFileReference value = this._mapEditorMapLoader.LoadedMap.Value;
				byte[] array = this._mapDeserializer.ReadFromMapFile<byte[]>(value, this._mapThumbnailOverlaySerializer);
				if (array != null && array.Length > 0)
				{
					this.LoadFromBytes(array, false);
				}
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002166 File Offset: 0x00000366
		public void Unload()
		{
			this.ClearInternal(false);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002170 File Offset: 0x00000370
		public void LoadFromFile(string path)
		{
			byte[] fileData = File.ReadAllBytes(path);
			this.LoadFromBytes(fileData, true);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000218C File Offset: 0x0000038C
		public void Clear()
		{
			this.ClearInternal(true);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002198 File Offset: 0x00000398
		public void LoadFromBytes(byte[] fileData, bool registerUndo)
		{
			Texture2D overlay = this.Overlay;
			byte[] oldOverlay = (overlay != null) ? ImageConversion.EncodeToPNG(overlay) : null;
			this.ClearInternal(false);
			this.Overlay = new Texture2D(1, 1, 5, false)
			{
				filterMode = 1
			};
			if (ImageConversion.LoadImage(this.Overlay, fileData))
			{
				this.Resize();
				if (registerUndo)
				{
					this._undoRegistry.RegisterSingleUndoable(new MapThumbnailOverlay.ThumbnailOverlayUndoable(this, oldOverlay, ImageConversion.EncodeToPNG(this.Overlay)));
					return;
				}
			}
			else
			{
				this.ClearInternal(registerUndo);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002214 File Offset: 0x00000414
		public void ClearInternal(bool registerUndo)
		{
			if (this.Overlay)
			{
				if (registerUndo)
				{
					this._undoRegistry.RegisterSingleUndoable(new MapThumbnailOverlay.ThumbnailOverlayUndoable(this, ImageConversion.EncodeToPNG(this.Overlay), null));
				}
				Object.Destroy(this.Overlay);
				this.Overlay = null;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002260 File Offset: 0x00000460
		public void Resize()
		{
			ValueTuple<int, int> scaledOverlaySize = this.GetScaledOverlaySize();
			int item = scaledOverlaySize.Item1;
			int item2 = scaledOverlaySize.Item2;
			RenderTextureFormat format = this._thumbnailRenderTextureProvider.RenderTexture.format;
			RenderTexture temporary = RenderTexture.GetTemporary(item, item2, 0, format, 0);
			RenderTexture.active = temporary;
			Graphics.Blit(this.Overlay, temporary);
			this.Overlay.Reinitialize(item, item2, 5, false);
			this.Overlay.ReadPixels(new Rect(Vector2.zero, new Vector2((float)item, (float)item2)), 0, 0);
			this.Overlay.Apply();
			RenderTexture.active = null;
			RenderTexture.ReleaseTemporary(temporary);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022F8 File Offset: 0x000004F8
		public ValueTuple<int, int> GetScaledOverlaySize()
		{
			float width = (float)this._mapThumbnailConfiguration.Width;
			int height = this._mapThumbnailConfiguration.Height;
			int width2 = this.Overlay.width;
			int height2 = this.Overlay.height;
			float num = Mathf.Min(width / (float)width2, (float)height / (float)height2);
			return new ValueTuple<int, int>((int)((float)width2 * num), (int)((float)height2 * num));
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002352 File Offset: 0x00000552
		public void NotifyThumbnailChanged()
		{
			this._eventBus.Post(new MapThumbnailChangedEvent());
		}

		// Token: 0x04000007 RID: 7
		public readonly MapThumbnailConfiguration _mapThumbnailConfiguration;

		// Token: 0x04000008 RID: 8
		public readonly IThumbnailRenderTextureProvider _thumbnailRenderTextureProvider;

		// Token: 0x04000009 RID: 9
		public readonly MapEditorMapLoader _mapEditorMapLoader;

		// Token: 0x0400000A RID: 10
		public readonly MapDeserializer _mapDeserializer;

		// Token: 0x0400000B RID: 11
		public readonly MapThumbnailOverlaySerializer _mapThumbnailOverlaySerializer;

		// Token: 0x0400000C RID: 12
		public readonly IUndoRegistry _undoRegistry;

		// Token: 0x0400000D RID: 13
		public readonly EventBus _eventBus;

		// Token: 0x02000005 RID: 5
		public class ThumbnailOverlayUndoable : IUndoable
		{
			// Token: 0x0600000F RID: 15 RVA: 0x00002364 File Offset: 0x00000564
			public ThumbnailOverlayUndoable(MapThumbnailOverlay mapThumbnailOverlay, byte[] oldOverlay, byte[] newOverlay)
			{
				this._mapThumbnailOverlay = mapThumbnailOverlay;
				this._oldOverlay = oldOverlay;
				this._newOverlay = newOverlay;
			}

			// Token: 0x06000010 RID: 16 RVA: 0x00002381 File Offset: 0x00000581
			public void Undo()
			{
				if (this._oldOverlay == null)
				{
					this._mapThumbnailOverlay.ClearInternal(false);
				}
				else
				{
					this._mapThumbnailOverlay.LoadFromBytes(this._oldOverlay, false);
				}
				this._mapThumbnailOverlay.NotifyThumbnailChanged();
			}

			// Token: 0x06000011 RID: 17 RVA: 0x000023B6 File Offset: 0x000005B6
			public void Redo()
			{
				if (this._newOverlay == null)
				{
					this._mapThumbnailOverlay.ClearInternal(false);
				}
				else
				{
					this._mapThumbnailOverlay.LoadFromBytes(this._newOverlay, false);
				}
				this._mapThumbnailOverlay.NotifyThumbnailChanged();
			}

			// Token: 0x0400000E RID: 14
			public readonly MapThumbnailOverlay _mapThumbnailOverlay;

			// Token: 0x0400000F RID: 15
			public readonly byte[] _oldOverlay;

			// Token: 0x04000010 RID: 16
			public readonly byte[] _newOverlay;
		}
	}
}
