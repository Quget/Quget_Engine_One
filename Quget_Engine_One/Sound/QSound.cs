using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Audio.OpenAL;
using OpenTK.Audio;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace Quget_Engine_One.Sound
{
    /// <summary>
    /// Used to play sound. This is expirimental... 
    /// Still uses ffmpeg to play sounds, might switch to OpenAL.
    /// </summary>
    class QSound
    {
        private static Dictionary<string, Process> stringProcDic = new Dictionary<string, Process>();
        private static int MaxThreadCount = 10;
        private static int threadCount = 0;
        public enum SoundType
        {
            Ambient,
            Effect,
            Music,
            Other,
            All,
        }
        //private static int ambient, effect, music,other;
        private static int[] volumes = new int[] { 100, 100, 100, 100 };
        public static void SetVolume(SoundType type, int volume)
        {
            if(type != SoundType.All)
                volumes[(int)type] = volume;
            else
            {
                for(int i = 0; i < volumes.Length; i++)
                {
                    volumes[i] = volume;
                }
            }
        }
        public static void PlayFFmpeg(string path,string soundID,SoundType type)
        {
            
            try
            {
                //FFMPEG process info
                ProcessStartInfo processInfo = new ProcessStartInfo();
                processInfo.FileName = "FFmpeg/ffplay.exe";
                processInfo.Arguments = path + " -nodisp -v 0 -autoexit -volume " + volumes[(int)type] + "";
                processInfo.UseShellExecute = false;
                processInfo.RedirectStandardOutput = true;


                Process process = Process.Start(processInfo);
                process.EnableRaisingEvents = true;
                process.Exited += Process_Exited;

                stringProcDic.Add(soundID, process);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void Dispose()
        {
            foreach(var process in stringProcDic)
            {
                process.Value.Kill();
            }
        }
        public static bool IsPlaying(string key)
        {
            return stringProcDic.ContainsKey(key);
        }

        private static void Process_Exited(object sender, EventArgs e)
        {
            Process process = (Process)sender;
            //process.Id
            List<string> toRemove = new List<string>();
            foreach (var stringProc in stringProcDic)
            {
                if(stringProc.Value.Id == process.Id)
                {
                    toRemove.Add(stringProc.Key);
                }
            }
            for(int i = 0; i < toRemove.Count; i++)
            {
                stringProcDic.Remove(toRemove[i]);
            }
            toRemove = null;
        }

        public static void PlayOpenTKT(string path)
        {
            Thread thread = new Thread(new ThreadStart(() => {
                if (threadCount < MaxThreadCount)
                {
                    PlayOpenTK(path);
                }
            }));
            thread.Start();
        }


        private static void PlayOpenTK(string path)
        {
            using (AudioContext context = new AudioContext())
            {
                int buffer = AL.GenBuffer();
                int source = AL.GenSource();
                int state;

                int channels = 0, bits_per_sample = 0, sample_rate = 0;
                byte[] sound_data = null;
                try
                {
                   sound_data = LoadWave(File.Open(path, FileMode.Open), out channels, out bits_per_sample, out sample_rate);
                }
                catch(Exception e)
                {
                    PlayOpenTK(path);
                    return;
                }
                
                AL.BufferData(buffer, GetSoundFormat(channels, bits_per_sample), sound_data, sound_data.Length, sample_rate);

                AL.Source(source, ALSourcei.Buffer, buffer);
                AL.SourcePlay(source);
                threadCount++;
                // Query the source to find out when it stops playing.
                do
                {
                    Thread.Sleep(250);
                    AL.GetSource(source, ALGetSourcei.SourceState, out state);
                }
                while ((ALSourceState)state == ALSourceState.Playing);

                AL.SourceStop(source);
                AL.DeleteSource(source);
                AL.DeleteBuffer(buffer);
                threadCount--;
            }
        }
        //OpenAL failure
        // Loads a wave/riff audio file.
        public static byte[] LoadWave(Stream stream, out int channels, out int bits, out int rate)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            using (BinaryReader reader = new BinaryReader(stream))
            {
                // RIFF header
                string signature = new string(reader.ReadChars(4));
                if (signature != "RIFF")
                    throw new NotSupportedException("Specified stream is not a wave file.");

                int riff_chunck_size = reader.ReadInt32();

                string format = new string(reader.ReadChars(4));
                if (format != "WAVE")
                    throw new NotSupportedException("Specified stream is not a wave file.");

                // WAVE header
                string format_signature = new string(reader.ReadChars(4));
                if (format_signature != "fmt ")
                    throw new NotSupportedException("Specified wave file is not supported.");

                int format_chunk_size = reader.ReadInt32();
                int audio_format = reader.ReadInt16();
                int num_channels = reader.ReadInt16();
                int sample_rate = reader.ReadInt32();
                int byte_rate = reader.ReadInt32();
                int block_align = reader.ReadInt16();
                int bits_per_sample = reader.ReadInt16();

                string data_signature = new string(reader.ReadChars(4));
                if (data_signature != "data")
                    throw new NotSupportedException("Specified wave file is not supported.");

                int data_chunk_size = reader.ReadInt32();

                channels = num_channels;
                bits = bits_per_sample;
                rate = sample_rate;
                return reader.ReadBytes((int)reader.BaseStream.Length);
            }
        }

        public static ALFormat GetSoundFormat(int channels, int bits)
        {
            switch (channels)
            {
                case 1: return bits == 8 ? ALFormat.Mono8 : ALFormat.Mono16;
                case 2: return bits == 8 ? ALFormat.Stereo8 : ALFormat.Stereo16;
                default: throw new NotSupportedException("The specified sound format is not supported.");
            }
        }
    }
}
