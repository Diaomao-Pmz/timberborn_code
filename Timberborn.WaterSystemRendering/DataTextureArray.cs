using System;
using UnityEngine;

namespace Timberborn.WaterSystemRendering
{
	// Token: 0x02000009 RID: 9
	public class DataTextureArray<T> : IDataTextureArray where T : struct
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021C5 File Offset: 0x000003C5
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000021CD File Offset: 0x000003CD
		public Texture2DArray OldArray { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000021D6 File Offset: 0x000003D6
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000021DE File Offset: 0x000003DE
		public Texture2DArray NewArray { get; private set; }

		// Token: 0x06000013 RID: 19 RVA: 0x000021E8 File Offset: 0x000003E8
		public DataTextureArray(TextureFormat textureFormat, Vector2Int size)
		{
			this._textureFormat = textureFormat;
			this._size = size;
			this._oldData = Array.Empty<T[]>();
			this._newData = Array.Empty<T[]>();
			this._bufferTexture = new Texture2D(this._size.x, this._size.y, this._textureFormat, false)
			{
				filterMode = 0,
				wrapMode = 1
			};
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002255 File Offset: 0x00000455
		public static DataTextureArray<T> Create(TextureFormat textureFormat, Vector2Int size)
		{
			DataTextureArray<T> dataTextureArray = new DataTextureArray<T>(textureFormat, size);
			dataTextureArray.Resize(1);
			return dataTextureArray;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002265 File Offset: 0x00000465
		public T[][] OldData
		{
			get
			{
				return this._oldData;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000226D File Offset: 0x0000046D
		public T[][] NewData
		{
			get
			{
				return this._newData;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002275 File Offset: 0x00000475
		public void Cleanup()
		{
			this.CleanupTextureArrays();
			this.CleanupBufferTexture();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002284 File Offset: 0x00000484
		public void SwapDataAndClear(int maxColumnIndex)
		{
			T[][] newData = this._newData;
			T[][] oldData = this._oldData;
			this._oldData = newData;
			this._newData = oldData;
			for (int i = 0; i < maxColumnIndex; i++)
			{
				Array.Clear(this._newData[i], 0, this._newData[i].Length);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022D4 File Offset: 0x000004D4
		public void SwapTextureArrays()
		{
			Texture2DArray newArray = this.NewArray;
			Texture2DArray oldArray = this.OldArray;
			this.OldArray = newArray;
			this.NewArray = oldArray;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002304 File Offset: 0x00000504
		public void UpdateTextureArrays(int columnIndex)
		{
			this._bufferTexture.SetPixelData<T>(this._oldData[columnIndex], 0, 0);
			this._bufferTexture.Apply(false, false);
			Graphics.CopyTexture(this._bufferTexture, 0, 0, this.OldArray, columnIndex, 0);
			this._bufferTexture.SetPixelData<T>(this._newData[columnIndex], 0, 0);
			this._bufferTexture.Apply(false, false);
			Graphics.CopyTexture(this._bufferTexture, 0, 0, this.NewArray, columnIndex, 0);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000237F File Offset: 0x0000057F
		public void Resize(int columnCount)
		{
			this.ResizeDataArrays(columnCount);
			this.CreateOrResizeTextureArrays(columnCount);
			this._columnCount = columnCount;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002398 File Offset: 0x00000598
		public void ResizeDataArrays(int columnCount)
		{
			int columnCount2 = this._columnCount;
			Array.Resize<T[]>(ref this._oldData, columnCount);
			Array.Resize<T[]>(ref this._newData, columnCount);
			int num = this._size.x * this._size.y;
			for (int i = columnCount2; i < columnCount; i++)
			{
				this._oldData[i] = new T[num];
				this._newData[i] = new T[num];
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002404 File Offset: 0x00000604
		public void CreateOrResizeTextureArrays(int columnCount)
		{
			if (this.OldArray == null || this.NewArray == null)
			{
				this.OldArray = DataTextureArray<T>.CreateTextureArray(this._size, columnCount, this._textureFormat);
				this.NewArray = DataTextureArray<T>.CreateTextureArray(this._size, columnCount, this._textureFormat);
				return;
			}
			Texture2DArray texture2DArray = DataTextureArray<T>.CreateTextureArray(this._size, columnCount, this._textureFormat);
			Texture2DArray texture2DArray2 = DataTextureArray<T>.CreateTextureArray(this._size, columnCount, this._textureFormat);
			DataTextureArray<T>.CopyTextureArray(this.OldArray, texture2DArray);
			DataTextureArray<T>.CopyTextureArray(this.NewArray, texture2DArray2);
			this.CleanupTextureArrays();
			this.OldArray = texture2DArray;
			this.NewArray = texture2DArray2;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024B0 File Offset: 0x000006B0
		public static Texture2DArray CreateTextureArray(Vector2Int size, int depth, TextureFormat textureFormat)
		{
			return new Texture2DArray(size.x, size.y, depth, textureFormat, false)
			{
				filterMode = 0,
				wrapMode = 1
			};
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024D8 File Offset: 0x000006D8
		public static void CopyTextureArray(Texture2DArray from, Texture2DArray to)
		{
			for (int i = 0; i < from.depth; i++)
			{
				Graphics.CopyTexture(from, i, 0, to, i, 0);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002501 File Offset: 0x00000701
		public void CleanupTextureArrays()
		{
			Object.Destroy(this.OldArray);
			Object.Destroy(this.NewArray);
			this.OldArray = null;
			this.NewArray = null;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002527 File Offset: 0x00000727
		public void CleanupBufferTexture()
		{
			Object.Destroy(this._bufferTexture);
			this._bufferTexture = null;
		}

		// Token: 0x0400000E RID: 14
		public T[][] _oldData;

		// Token: 0x0400000F RID: 15
		public T[][] _newData;

		// Token: 0x04000010 RID: 16
		public Texture2D _bufferTexture;

		// Token: 0x04000011 RID: 17
		public readonly TextureFormat _textureFormat;

		// Token: 0x04000012 RID: 18
		public readonly Vector2Int _size;

		// Token: 0x04000013 RID: 19
		public int _columnCount;
	}
}
