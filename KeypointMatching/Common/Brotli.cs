using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeypointMatching.Common
{
	/// <summary>
	/// Предоставляет методы и свойства, используемые для сжатия и распаковки диапазонов байтов с использованием спецификации формата данных Brotli.
	/// </summary>
	public sealed class Brotli
	{
		/// <summary>
		/// Сжимает диапазон байтов.
		/// </summary>
		/// <param name="bytes">Диапазон байтов, который необходимо сжать.</param>
		/// <returns>Возвращает диапазон байтов, в котором хранится сжатый объект.</returns>
		public static byte[] CompressBytes(byte[] bytes)
		{
			using (var outputStream = new MemoryStream())
			{
				using (var compressionStream = new BrotliStream(outputStream, CompressionLevel.Fastest))
				{
					compressionStream.Write(bytes, 0, bytes.Length);
				}
				return outputStream.ToArray();
			}
		}

		/// <summary>
		/// Распаковывает диапазон байтов.
		/// </summary>
		/// <param name="bytes">Диапазон байтов, который необходимо распаковать.</param>
		/// <returns>Возвращает диапазон байтов, в котором хранится распакованный объект.</returns>
		public static byte[] DecompressBytes(byte[] bytes)
		{
			using (var inputStream = new MemoryStream(bytes))
			{
				using (var outputStream = new MemoryStream())
				{
					using (var compressionStream = new BrotliStream(inputStream, CompressionMode.Decompress))
					{
						compressionStream.CopyTo(outputStream);
					}
					return outputStream.ToArray();
				}
			}
		}
	}
}

