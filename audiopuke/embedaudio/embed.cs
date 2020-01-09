/*
 * Created by SharpDevelop.
 * User: student
 * Date: 2020-01-07
 * Time: 15:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Drawing;
using System.Xml;
using TagLib;
using SPI.EmbedAudio.MediaData;

namespace SPI.EmbedAudio
{
	/// <summary>
	/// Provides an interface for reading and writing to Embedded Audio files, both compressed and uncompressed.
	/// </summary>
	public class EMFile
	{
		private XmlNode attr;
		private XmlNodeList tracks;
		
		public int Count = -1;
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="FileName">Location of em3 or ec3 file.</param>
		/// <param name="Compressed">Decompresses the file if true, otherwise false.</param>
		public EMFile(string FileName, bool Compressed = false)
		{
			if(Compressed)
			{
				MemoryStream file = Compress.Unzip(System.IO.File.ReadAllBytes(FileName));
				
				XmlDocument em3 = new XmlDocument();
				em3.Load(file);
				attr = em3.SelectSingleNode("/em3/header");
                Count = Convert.ToInt32(attr.Attributes.GetNamedItem("Count").Value);
                tracks = em3.SelectNodes("//em3/tracks/track");
			}
			else
			{
				XmlDocument em3 = new XmlDocument();
				em3.Load(FileName);
				attr = em3.SelectSingleNode("/em3/header");
                Count = Convert.ToInt32(attr.Attributes.GetNamedItem("Count").Value);
                tracks = em3.SelectNodes("//em3/tracks/track");
                
			}
		}
		/// <summary>
		/// Reads and returns the track data of the given index in the EMFile.
		/// </summary>
		/// <param name="index">Index of the track in the file.</param>
		/// <param name="dummyLocation">Dummy location to write id3tag file to.</param>
		/// <returns>Returns the track data of the given index of the file as an EMTrack. </returns>
		public EMTrack GetTrack(int index, string dummyLocation)
		{
			string base64 = tracks[index].InnerText;
			byte[] dat = Convert.FromBase64String(base64);
			
	        Stream stream = new MemoryStream(dat);
	        EMHeader header = new EMHeader();
	        
	        TagLib.Tag emu = id3Tag.FileTagReader(stream, dummyLocation);
	        header.Title = emu.Title;
	        header.Artist = emu.FirstAlbumArtist;
	        header.Album = emu.Album;
	        header.Year = emu.Year;
	        
	        if (emu.Pictures.Length != 0)
	        {
	            MemoryStream ms = new MemoryStream(emu.Pictures[0].Data.Data);
	            header.AlbumArt = Image.FromStream(ms);
	        }
	        else header.AlbumArt = null;
	        
	        EMTrack y = new EMTrack();
	        y.Track = (MemoryStream)stream;
	        y.Header = header;
	        
	        return y;
		}
		
		/// <summary>
		/// Takes the stream of the given index and writes it to a file.
		/// </summary>
		/// <param name="index">Index of the track in the file.</param>
		/// <param name="FileName">Location to save the file to.</param>
		public void WriteTrackToFile(int index, string FileName)
		{
			EMTrack y = GetTrack(index, FileName);
			
			FileStream k = new FileStream(FileName, FileMode.Create);
			byte[] data = new byte[y.Track.Length];
			y.Track.Read(data, 0, (int)y.Track.Length);
			k.Write(data, 0, data.Length);
			k.Dispose();
		}
	}
	
	public class EMTrack
	{
		public MemoryStream Track {get; set;}
		public EMHeader Header {get; set;}
		
		
	}
	
	public class EMHeader
	{
		public virtual string Title {get; set;}
		public virtual string Artist {get; set;}
		public virtual string Album {get; set;}
		public virtual uint Year {get; set;}
		public virtual Image AlbumArt {get; set;}
	}
}
