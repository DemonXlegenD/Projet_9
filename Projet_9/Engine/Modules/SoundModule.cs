using NAudio.Wave;
using NGlobal;
using NEngine;
using System;
using System.Collections.Generic;
using System.IO;

namespace NModules
{
    public class SoundModule : Module
    {
        private Dictionary<string, AudioFileReader> audioFileReaders;
        private Dictionary<string, WaveOutEvent> waveOutDevices;
        private Dictionary<string, bool> loopingStates;

        public SoundModule()
        {
            audioFileReaders = new Dictionary<string, AudioFileReader>();
            waveOutDevices = new Dictionary<string, WaveOutEvent>();
            loopingStates = new Dictionary<string, bool>();

            string[] files2 = Directory.GetFiles("Assets\\Musiques\\Boss_Fight");
            foreach (string file in files2)
            {
                string id = file.Replace(".mp3", "").Replace("Assets\\Musiques\\Boss_Fight\\", "");
                var audioFileReader = new AudioFileReader(file);
                audioFileReaders.Add(id, audioFileReader);

                var waveOutDevice = new WaveOutEvent();
                waveOutDevice.Init(audioFileReader);
                waveOutDevices.Add(id, waveOutDevice);

                loopingStates.Add(id, false);
            }

            string[] files3 = Directory.GetFiles("Assets\\Musiques\\Fight");
            foreach (string file in files3)
            {
                string id = file.Replace(".mp3", "").Replace("Assets\\Musiques\\Fight\\", "");
                var audioFileReader = new AudioFileReader(file);
                audioFileReaders.Add(id, audioFileReader);

                var waveOutDevice = new WaveOutEvent();
                waveOutDevice.Init(audioFileReader);
                waveOutDevices.Add(id, waveOutDevice);

                loopingStates.Add(id, false);
            }

        }
        public SoundModule(Dictionary<string, string> musiqueEtChemins)
        {
            audioFileReaders = new Dictionary<string, AudioFileReader>();
            waveOutDevices = new Dictionary<string, WaveOutEvent>();
            loopingStates = new Dictionary<string, bool>();

            foreach (var kvp in musiqueEtChemins)
            {
                var audioFileReader = new AudioFileReader(kvp.Value);
                audioFileReaders.Add(kvp.Key, audioFileReader);

                var waveOutDevice = new WaveOutEvent();
                waveOutDevice.Init(audioFileReader);
                waveOutDevices.Add(kvp.Key, waveOutDevice);

                loopingStates.Add(kvp.Key, false);
            }
        }

        public void AddAudioFilePath(string name, string filePath)
        {
            var audioFileReader = new AudioFileReader(filePath);
            audioFileReaders.Add(name, audioFileReader);

            var waveOutDevice = new WaveOutEvent();
            waveOutDevice.Init(audioFileReader);
            waveOutDevices.Add(name, waveOutDevice);
        }

        //Pour les cris des pokemons mettre l'id du pokemon, les autres mettre le nom du fichier sans le .mp3
        public void Play(string nomMusique, bool loop = false)
        {
            if (waveOutDevices.ContainsKey(nomMusique))
            {
                // Vérifie si la musique est déjà en cours de lecture
                if (waveOutDevices[nomMusique].PlaybackState != PlaybackState.Playing)
                {
                    waveOutDevices[nomMusique].PlaybackStopped += OnPlaybackStopped;
                    waveOutDevices[nomMusique].Play();
                }

                //Met à jour l'état de boucle
                loopingStates[nomMusique] = loop;
            }
            else
            {
                Console.WriteLine("Musique non trouvée !");
            }
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            // Si la musique s'est terminée et la boucle est activée, on la remet en boucle
            var waveOutDevice = (WaveOutEvent)sender;
            foreach (var kvp in waveOutDevices)
            {
                if (kvp.Value == waveOutDevice && loopingStates[kvp.Key])
                {
                    kvp.Value.Play();
                    break;
                }
            }
        }

        public void Stop(string nomMusique)
        {
            if (waveOutDevices.ContainsKey(nomMusique))
            {
                waveOutDevices[nomMusique].PlaybackStopped -= OnPlaybackStopped;
                waveOutDevices[nomMusique].Stop();
            }
        }

        public void Dispose()
        {
            foreach (var waveOutDevice in waveOutDevices.Values)
            {
                waveOutDevice.Dispose();
            }

            foreach (var audioFileReader in audioFileReaders.Values)
            {
                audioFileReader.Dispose();
            }
          
        }
    }
    
}
