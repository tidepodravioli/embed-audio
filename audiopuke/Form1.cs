using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using TagLib;
using CSCore;
using CSCore.DMO;
using CSCore.Codecs;
using CSCore.CoreAudioAPI;
using CSCore.SoundOut;
using CSCore.Streams;
using System.IO;
using SPI.EmbedAudio;

namespace audiopuke
{
    public partial class Form1 : Form
    {
        private readonly playerCom  player = new playerCom();
        private bool _stopSliderUpdate;
        private readonly ObservableCollection<MMDevice> _devices = new ObservableCollection<MMDevice>();

        bool mousedown = false;
        bool embedded = false;
        XmlNodeList em3tracks;
        int current = 0;
        int count = 0;

        public Form1()
        {
            InitializeComponent();
        }
        
        private class playerCom
        {
            private ISoundOut _soundOut;
            private IWaveSource _waveSource;

            public event EventHandler<PlaybackStoppedEventArgs> PlaybackStopped;
            public PlaybackState PlaybackState
            {
                get
                {
                    if (_soundOut != null)
                        return _soundOut.PlaybackState;
                    return PlaybackState.Stopped;
                }
            }
            public void Open(string filename, MMDevice device)
            {
                CleanupPlayback();

                _waveSource =
                    CodecFactory.Instance.GetCodec(filename)
                        .ToSampleSource()
                        .ToMono()
                        .ToWaveSource();
                _soundOut = new WasapiOut() { Latency = 100, Device = device };
                _soundOut.Initialize(_waveSource);
                if (PlaybackStopped != null) _soundOut.Stopped += PlaybackStopped;
            }

            private void CleanupPlayback()
            {
                if (_soundOut != null)
                {
                    _soundOut.Dispose();
                    _soundOut = null;
                }
                if (_waveSource != null)
                {
                    _waveSource.Dispose();
                    _waveSource = null;
                }
            }
            public TimeSpan Position
            {
                get
                {
                    if (_waveSource != null)
                        return _waveSource.GetPosition();
                    return TimeSpan.Zero;
                }
                set
                {
                    if (_waveSource != null)
                        _waveSource.SetPosition(value);
                }
            }

            public TimeSpan Length
            {
                get
                {
                    if (_waveSource != null)
                        return _waveSource.GetLength();
                    return TimeSpan.Zero;
                }
            }
            public int Volume
            {
                get
                {
                    if (_soundOut != null)
                        return Math.Min(100, Math.Max((int)(_soundOut.Volume * 100), 0));
                    return 100;
                }
                set
                {
                    if (_soundOut != null)
                    {
                        _soundOut.Volume = Math.Min(1.0f, Math.Max(value / 100f, 0f));
                    }
                }
            }
            public void Play()
            {
                if (_soundOut != null)
                    _soundOut.Play();
            }
            public void Pause()
            {
                if (_soundOut != null)
                    _soundOut.Pause();
            }
            public void Stop()
            {
                if (_soundOut != null)
                    _soundOut.Stop();
            }

            public void playFromStream(Stream stream, MMDevice device)
            {
                _waveSource  = GetSoundSource(stream);
                    _soundOut = new WasapiOut() { Latency = 100, Device = device };
                    _soundOut.Initialize(_waveSource );
                    if (PlaybackStopped != null) _soundOut.Stopped += PlaybackStopped;
            }

            private IWaveSource GetSoundSource(Stream stream)
            {
                // Instead of using the CodecFactory as helper, you specify the decoder directly:
                return new CSCore.Codecs.MP3.DmoMp3Decoder(stream);

            }
        }
        public class SimpleFile
        {
            public SimpleFile(string Name, Stream Stream)
            {
                this.Name = Name;
                this.Stream = Stream;
            }
            public string Name { get; set; }
            public Stream Stream { get; set; }
        }
        public class SimpleFileAbstraction : TagLib.File.IFileAbstraction
        {
            private SimpleFile file;

            public SimpleFileAbstraction(SimpleFile file)
            {
                this.file = file;
            }

            public string Name
            {
                get { return file.Name; }
            }

            public System.IO.Stream ReadStream
            {
                get { return file.Stream; }
            }

            public System.IO.Stream WriteStream
            {
                get { return file.Stream; }
            }

            public void CloseStream(System.IO.Stream stream)
            {
                stream.Position = 0;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (var mmdeviceEnumerator = new MMDeviceEnumerator())
            {
                using (
                    var mmdeviceCollection = mmdeviceEnumerator.EnumAudioEndpoints(DataFlow.Render, DeviceState.Active))
                {
                    foreach (var device in mmdeviceCollection)
                    {
                        _devices.Add(device);
                    }
                }
            }

            comboBox1.DataSource = _devices;
            comboBox1.DisplayMember = "FriendlyName";
            comboBox1.ValueMember = "DeviceID";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = CodecFactory.SupportedFilesFilterEn + ";*.em3"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (openFileDialog.FileName.EndsWith(".em3"))
                    {
                        XmlDocument em3 = new XmlDocument();
                        em3.Load(openFileDialog.FileName);
                        XmlNode attr = em3.SelectSingleNode("/em3/header");
                        count = Convert.ToInt32(attr.Attributes.GetNamedItem("count").Value);
                        XmlNodeList tracks = em3.SelectNodes("//em3/tracks/track");
                        em3tracks = tracks;

                        string base64 = tracks[0].InnerText;
                        byte[] dat = Convert.FromBase64String(base64);
                        
                        Stream stream = new MemoryStream(dat);
                        TagLib.Tag emu = FileTagReader(stream, Application.StartupPath + "//temp.mp3");
                        tracklbl.Text = emu.Title;
                        artistlbl.Text = emu.FirstAlbumArtist;
                        albumlbl.Text = emu.Album;
                        yearlbl.Text = emu.Year.ToString();
                        if (emu.Pictures.Count() != 0)
                        {
                            MemoryStream ms = new MemoryStream(emu.Pictures[0].Data.Data);
                            pictureBox1.Image = Image.FromStream(ms);
                        }
                        
                        player.playFromStream(stream, (MMDevice)comboBox1.SelectedItem);
                        trackBarVolume.Value = player.Volume;
                        button3.PerformClick();
                    }
                    else
                    {
                        var tag = TagLib.File.Create(openFileDialog.FileName);
                        tracklbl.Text = tag.Tag.Title;
                        artistlbl.Text = tag.Tag.FirstAlbumArtist;
                        albumlbl.Text = tag.Tag.Album;
                        yearlbl.Text = tag.Tag.Year.ToString();
                        if (tag.Tag.Pictures.Count() > 0)
                        {
                            MemoryStream ms = new MemoryStream(tag.Tag.Pictures[0].Data.Data);
                            pictureBox1.Image = Image.FromStream(ms);
                        }
                        tag.Dispose();

                        player.Open(openFileDialog.FileName, (MMDevice)comboBox1.SelectedItem);
                        trackBarVolume.Value = player.Volume;

                        button3.Enabled = true;
                        button2.Enabled = button4.Enabled = false;
                        button3.PerformClick();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not open file: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (player.PlaybackState != PlaybackState.Playing)
            {
                player.Play();
                button3.Enabled = false;
                button2.Enabled = button4.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (player.PlaybackState == PlaybackState.Playing)
            {
                player.Pause();
                button3.Enabled = true;
                button2.Enabled = button4.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (player.PlaybackState != PlaybackState.Stopped)
            {
                player.Stop();
                button3.Enabled = true;
                button2.Enabled = button4.Enabled = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan position = player.Position;
            TimeSpan length = player.Length;
            if (position > length)
                length = position;

            lblPosition.Text = String.Format(@"{0:mm\:ss} / {1:mm\:ss}", position, length);

            if (!_stopSliderUpdate &&
                length != TimeSpan.Zero && position != TimeSpan.Zero)
            {
                double perc = position.TotalMilliseconds / length.TotalMilliseconds * trackBar1.Maximum;
                trackBar1.Value = (int)perc;
            }

            if(trackBar1.Value == trackBar1.Maximum && mousedown == false)
            {
                trackBar1.Value = 0;
                button5.PerformClick();
            }
        }

        private void trackBar1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _stopSliderUpdate = true;
                mousedown = true;
            }
                
                
        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _stopSliderUpdate = false;
                mousedown = false;
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            if (_stopSliderUpdate)
            {
                double perc = trackBar1.Value / (double)trackBar1.Maximum;
                TimeSpan position = TimeSpan.FromMilliseconds(player.Length.TotalMilliseconds * perc);
                player.Position = position;
            }
        }
        private void trackbarVolume_ValueChanged(object sender, EventArgs e)
        {
            player.Volume = trackBarVolume.Value;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            current++;

            if(current < count) // current starts from 0, count starts from 1
            {
                player.Stop();
                string base64 = em3tracks[current].InnerText;
                byte[] dat = Convert.FromBase64String(base64);
                Stream str = new MemoryStream(dat);
                TagLib.Tag em3 = FileTagReader(str, Application.StartupPath + "//temp.mp3");
                tracklbl.Text = em3.Title;
                artistlbl.Text = em3.FirstAlbumArtist;
                albumlbl.Text = em3.Album;
                yearlbl.Text = em3.Year.ToString();
                if (em3.Pictures.Count() != 0)
                {
                    MemoryStream ms = new MemoryStream(em3.Pictures[0].Data.Data);
                    pictureBox1.Image = Image.FromStream(ms);
                }
                player.playFromStream(str, (MMDevice)comboBox1.SelectedItem);
                player.Play();
            }
            else
            {
                current--;
            }
        }
        public static TagLib.Tag FileTagReader(Stream stream, string fileName)
        {
            //Create a simple file and simple file abstraction
            var simpleFile = new SimpleFile(fileName, stream);
            var simpleFileAbstraction = new SimpleFileAbstraction(simpleFile);
            /////////////////////

            //Create a taglib file from the simple file abstraction
            var mp3File = TagLib.File.Create(simpleFileAbstraction);

            //Get all the tags
            TagLib.Tag tags = mp3File.Tag;

            //Save and close
            mp3File.Save();
            mp3File.Dispose();

            //Return the tags
            return tags;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            current--;

            if (current > -1) // current starts from 0, count starts from 1
            {
                player.Stop();
                string base64 = em3tracks[current].InnerText;
                byte[] dat = Convert.FromBase64String(base64);
                Stream str = new MemoryStream(dat);
                TagLib.Tag em3 = FileTagReader(str, Application.StartupPath + "//temp.mp3");
                tracklbl.Text = em3.Title;
                artistlbl.Text = em3.FirstAlbumArtist;
                albumlbl.Text = em3.Album;
                yearlbl.Text = em3.Year.ToString();
                if(em3.Pictures.Count() != 0)
                {
                    MemoryStream ms = new MemoryStream(em3.Pictures[0].Data.Data);
                    pictureBox1.Image = Image.FromStream(ms);
                }
                player.playFromStream(str, (MMDevice)comboBox1.SelectedItem);
                player.Play();
            }
            else
            {
                current++;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            player.Stop();
        }
		void YearlblClick(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog{
				Filter = "EM3|*.em3"
			};
			dlg.ShowDialog();
			
			SaveFileDialog sdlg = new SaveFileDialog{
				Filter = "EME3|*.eme3"
			};
			sdlg.ShowDialog();
			
			FileCompress.ZipToFile(dlg.FileName, sdlg.FileName);
			MessageBox.Show("Done.");
		}
    }
}
