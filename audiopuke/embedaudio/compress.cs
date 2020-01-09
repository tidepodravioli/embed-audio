/*
 * Created by SharpDevelop.
 * User: student
 * Date: 2020-01-07
 * Time: 14:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace SPI.EmbedAudio
{
		public static class Compress
		{
			public static void CopyTo(Stream src, Stream dest) {
			    byte[] bytes = new byte[4096];
			
			    int cnt;
			
			    while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0) {
			        dest.Write(bytes, 0, cnt);
			    }
			}
			
			public static byte[] Zip(string str, string tempFile) {
			    var bytes = Encoding.UTF8.GetBytes(str);
			
			    using (var msi = new MemoryStream(bytes))
			    using (var mso = new MemoryStream()) {
			        using (var gs = new GZipStream(mso, CompressionMode.Compress)) {
			            //msi.CopyTo(gs);
			            CopyTo(msi, gs);
			        }
			
			        return mso.ToArray();
			    }
			}
			
			public static MemoryStream Unzip(byte[] bytes) {
			    using (var msi = new MemoryStream(bytes))
			    using (var mso = new MemoryStream()) {
			        using (var gs = new GZipStream(msi, CompressionMode.Decompress)) {
			            //gs.CopyTo(mso);
			            CopyTo(gs, mso);
			        }
			
			    	return mso;
			    }
			}
	}
		
	public static class FileCompress
	{
		public static void CopyTo(Stream src, Stream dest) {
		    byte[] bytes = new byte[4096];
		
		    int cnt;
		
		    while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0) {
		        dest.Write(bytes, 0, cnt);
		    }
		}
		
		public static void ZipToFile(string inFile, string outFile)
		{
			FileStream input = new FileStream(inFile, FileMode.Open, FileAccess.Read);
			FileStream output = new FileStream(outFile, FileMode.Create, FileAccess.Write);
			
			int pl = 1;
			for(int i = 999999; i >= 1; i--)
			{
				if(input.Length % i == 0)
				{
					pl = i;
					break;
				}
			}
			
			for(int i = 0; i <= input.Length; i += pl)
			{
				byte[] buf = new byte[pl];
				input.Read(buf, i, pl);
				
				using (var msi = new MemoryStream(buf))
			    using (var mso = new MemoryStream()) {
			        using (var gs = new GZipStream(mso, CompressionMode.Compress)) {
			            //msi.CopyTo(gs);
			            CopyTo(msi, gs);
			        }
					output.Write(buf, i, pl);
			    }
			}
		}
	}
}
