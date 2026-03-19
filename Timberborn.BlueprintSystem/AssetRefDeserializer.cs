using System;
using Timberborn.AssetSystem;
using Timberborn.SerializationSystem;
using UnityEngine;

namespace Timberborn.BlueprintSystem
{
	// Token: 0x0200000A RID: 10
	public class AssetRefDeserializer
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002521 File Offset: 0x00000721
		public AssetRefDeserializer(IAssetLoader assetLoader)
		{
			this._assetLoader = assetLoader;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002530 File Offset: 0x00000730
		public void EnableSafeMode()
		{
			this._safeMode = true;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000253C File Offset: 0x0000073C
		public bool TryDeserializeArray(SerializedObject serializedObject, string name, Type type, out Array assetArray)
		{
			if (AssetRefDeserializer.CanDeserialize(type))
			{
				AssetRefDeserializer.IAssetRefDeserializer assetRefDeserializer = this.CreateDeserializer(serializedObject, name, type);
				assetArray = assetRefDeserializer.DeserializeArray();
				return true;
			}
			assetArray = null;
			return false;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000256C File Offset: 0x0000076C
		public bool TryDeserialize(SerializedObject serializedObject, string name, Type type, out object asset)
		{
			if (AssetRefDeserializer.CanDeserialize(type))
			{
				AssetRefDeserializer.IAssetRefDeserializer assetRefDeserializer = this.CreateDeserializer(serializedObject, name, type);
				asset = assetRefDeserializer.Deserialize();
				return true;
			}
			asset = null;
			return false;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000259B File Offset: 0x0000079B
		public static bool CanDeserialize(Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(AssetRef<>);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000025BC File Offset: 0x000007BC
		public AssetRefDeserializer.IAssetRefDeserializer CreateDeserializer(SerializedObject serializedObject, string name, Type type)
		{
			return (AssetRefDeserializer.IAssetRefDeserializer)Activator.CreateInstance(typeof(AssetRefDeserializer.GenericDeserializer<>).MakeGenericType(new Type[]
			{
				type.GenericTypeArguments[0]
			}), new object[]
			{
				this._assetLoader,
				serializedObject,
				name,
				this._safeMode
			});
		}

		// Token: 0x0400000E RID: 14
		public readonly IAssetLoader _assetLoader;

		// Token: 0x0400000F RID: 15
		public bool _safeMode;

		// Token: 0x0200000B RID: 11
		public class GenericDeserializer<T> : AssetRefDeserializer.IAssetRefDeserializer where T : Object
		{
			// Token: 0x06000026 RID: 38 RVA: 0x00002618 File Offset: 0x00000818
			public GenericDeserializer(IAssetLoader assetLoader, SerializedObject serializedObject, string name, bool safeMode)
			{
				this._assetLoader = assetLoader;
				this._serializedObject = serializedObject;
				this._name = name;
				this._safeMode = safeMode;
			}

			// Token: 0x06000027 RID: 39 RVA: 0x00002640 File Offset: 0x00000840
			public Array DeserializeArray()
			{
				string[] array = this._serializedObject.Has(this._name) ? this._serializedObject.GetArray<string>(this._name) : Array.Empty<string>();
				AssetRef<T>[] array2 = new AssetRef<T>[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array2.SetValue(this.CreateAssetRef(array[i]), i);
				}
				return array2;
			}

			// Token: 0x06000028 RID: 40 RVA: 0x000026A4 File Offset: 0x000008A4
			public object Deserialize()
			{
				string path = this._serializedObject.Has(this._name) ? this._serializedObject.Get<string>(this._name) : null;
				return this.CreateAssetRef(path);
			}

			// Token: 0x06000029 RID: 41 RVA: 0x000026E0 File Offset: 0x000008E0
			public AssetRef<T> CreateAssetRef(string path)
			{
				if (string.IsNullOrEmpty(path))
				{
					return null;
				}
				return new AssetRef<T>(path, new Lazy<T>(() => this.LoadAsset(path)));
			}

			// Token: 0x0600002A RID: 42 RVA: 0x0000272C File Offset: 0x0000092C
			public T LoadAsset(string path)
			{
				if (!this._safeMode)
				{
					return this._assetLoader.Load<T>(path);
				}
				return this._assetLoader.LoadSafe<T>(path);
			}

			// Token: 0x04000010 RID: 16
			public readonly IAssetLoader _assetLoader;

			// Token: 0x04000011 RID: 17
			public readonly SerializedObject _serializedObject;

			// Token: 0x04000012 RID: 18
			public readonly string _name;

			// Token: 0x04000013 RID: 19
			public readonly bool _safeMode;
		}

		// Token: 0x0200000D RID: 13
		public interface IAssetRefDeserializer
		{
			// Token: 0x0600002D RID: 45
			Array DeserializeArray();

			// Token: 0x0600002E RID: 46
			object Deserialize();
		}
	}
}
