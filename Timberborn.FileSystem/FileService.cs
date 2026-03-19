using System;
using System.IO;
using System.Linq;
using Timberborn.PlatformUtilities;
using UnityEngine;

namespace Timberborn.FileSystem
{
	// Token: 0x02000006 RID: 6
	public class FileService : IFileService
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000022C1 File Offset: 0x000004C1
		public bool HasDocumentsPermissions { get; } = DocumentsPermissions.HasPermissions();

		// Token: 0x06000007 RID: 7 RVA: 0x000022C9 File Offset: 0x000004C9
		public bool FileExists(string fileName)
		{
			return File.Exists(fileName);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022D4 File Offset: 0x000004D4
		public Stream CreateFile(string fileName)
		{
			Stream result;
			try
			{
				result = File.Create(fileName);
			}
			catch (ArgumentNullException)
			{
				throw;
			}
			catch (ArgumentException e)
			{
				throw FileService.ThrowAsIOException(e, fileName);
			}
			catch (NotSupportedException e2)
			{
				throw FileService.ThrowAsIOException(e2, fileName);
			}
			return result;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002324 File Offset: 0x00000524
		public Stream OpenFile(string fileName)
		{
			Stream result;
			try
			{
				result = File.OpenRead(fileName);
			}
			catch (ArgumentNullException)
			{
				throw;
			}
			catch (ArgumentException e)
			{
				throw FileService.ThrowAsIOException(e, fileName);
			}
			catch (NotSupportedException e2)
			{
				throw FileService.ThrowAsIOException(e2, fileName);
			}
			return result;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002374 File Offset: 0x00000574
		public void DeleteFile(string fileName)
		{
			try
			{
				File.SetAttributes(fileName, FileAttributes.Normal);
			}
			catch (Exception ex)
			{
				Debug.LogWarning("Exception while setting file attributes to normal: " + ex.Message + "/n" + ex.StackTrace);
			}
			try
			{
				File.Delete(fileName);
			}
			catch (Exception e)
			{
				throw FileService.ThrowAsIOException(e, fileName);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000023E0 File Offset: 0x000005E0
		public FileInfo GetFileInfo(string fileName)
		{
			FileInfo result;
			try
			{
				result = new FileInfo(fileName);
			}
			catch (ArgumentNullException)
			{
				throw;
			}
			catch (ArgumentException e)
			{
				throw FileService.ThrowAsIOException(e, fileName);
			}
			return result;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002420 File Offset: 0x00000620
		public bool DirectoryExistsAndNotEmpty(string directoryName, string fileExtension)
		{
			return Directory.Exists(directoryName) && Directory.EnumerateFiles(directoryName).Any((string file) => fileExtension == null || Path.GetExtension(file) == fileExtension);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000245C File Offset: 0x0000065C
		public DirectoryCreationResult CreateDirectoryIfValid(string directoryPath)
		{
			DirectoryCreationResult result;
			try
			{
				if (this.DirectoryExistsAndNotEmpty(directoryPath, null))
				{
					result = DirectoryCreationResult.NameTaken;
				}
				else
				{
					this.CreateDirectory(directoryPath);
					result = DirectoryCreationResult.OK;
				}
			}
			catch (Exception ex)
			{
				Debug.LogError("Failed to create directory " + directoryPath + " due to " + ex.Message);
				result = DirectoryCreationResult.NameInvalid;
			}
			return result;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000024B4 File Offset: 0x000006B4
		public void CreateDirectory(string directoryName)
		{
			try
			{
				Directory.CreateDirectory(directoryName);
			}
			catch (Exception e)
			{
				throw FileService.ThrowAsIOException(e, directoryName);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000024E4 File Offset: 0x000006E4
		public void DeleteDirectory(string directoryName)
		{
			try
			{
				FileService.SetAttributesToNormal(new DirectoryInfo(directoryName));
			}
			catch (Exception ex)
			{
				Debug.LogWarning("Exception while setting attributes to normal: " + ex.Message + "/n" + ex.StackTrace);
			}
			try
			{
				Directory.Delete(directoryName, true);
			}
			catch (Exception e)
			{
				throw FileService.ThrowAsIOException(e, directoryName);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002550 File Offset: 0x00000750
		public string CombineIntoPath(string path1, string path2, string extension = "")
		{
			string result;
			try
			{
				result = Path.Combine(path1, path2 + extension);
			}
			catch (ArgumentNullException)
			{
				throw;
			}
			catch (ArgumentException e)
			{
				throw FileService.ThrowAsIOException(e, path1 + "/" + path2 + extension);
			}
			return result;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000025A0 File Offset: 0x000007A0
		public void WriteTextToFile(string path, string text)
		{
			try
			{
				this.CreateDirectory(Path.GetDirectoryName(path));
				File.WriteAllText(path, text);
			}
			catch (Exception e)
			{
				throw FileService.ThrowAsIOException(e, path);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000025DC File Offset: 0x000007DC
		public void CopyFile(string sourceFileName, string destinationFileName)
		{
			try
			{
				File.Copy(sourceFileName, destinationFileName);
			}
			catch (Exception e)
			{
				throw FileService.ThrowAsIOException(e, sourceFileName + " -> " + destinationFileName);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002614 File Offset: 0x00000814
		public static Exception ThrowAsIOException(Exception e, string name)
		{
			return new IOException(e.Message + " Name: " + name, e);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002630 File Offset: 0x00000830
		public static void SetAttributesToNormal(DirectoryInfo directory)
		{
			directory.Attributes = FileAttributes.Normal;
			DirectoryInfo[] directories = directory.GetDirectories();
			for (int i = 0; i < directories.Length; i++)
			{
				FileService.SetAttributesToNormal(directories[i]);
			}
			FileInfo[] files = directory.GetFiles();
			for (int i = 0; i < files.Length; i++)
			{
				files[i].Attributes = FileAttributes.Normal;
			}
		}
	}
}
