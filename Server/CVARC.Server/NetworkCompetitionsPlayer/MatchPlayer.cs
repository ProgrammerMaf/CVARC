﻿using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using CommonTypes;
using CVARC.Network;

namespace NetworkCompetitionsPlayer
{
    class MatchPlayer
    {
        private readonly HelloPackage package;
        private readonly Player player;
        private readonly Player player2;
        private const string ReplayDirectory = "RawReplays";
        private readonly Random random = new Random();

        public MatchPlayer(HelloPackage package, Player player, Player player2)
        {
            this.package = package;
            this.player = player;
            this.player2 = player2;
        }

        public string Play()
        {
            DisposeResource();
            var process = RunCompetition();
            process.WaitForExit(60000);
            var replay = File.ReadAllText(Directory.GetFiles(ReplayDirectory).Single());
            DisposeResource();
            return replay;
        }

        private Process RunCompetition()
        {
            var mapSeed = package.MapSeed != 0 ? package.MapSeed : random.Next();
            const bool allowExitFromMatch = false;
            const bool needSaveReplay = true;
            const bool realTime = false;
            var process = Process.Start(new ProcessStartInfo("CVARC.Network.exe")
            {
                Arguments = string.Format("{0} {1} {2} {3} {4} {5} {6}", package.LevelName, mapSeed, package.Opponent, package.Side, realTime, needSaveReplay, allowExitFromMatch),
            });
            RunClients();
            return process;
        }

        private void RunClients()
        {
            UnZipAndRunClient(player);
            Thread.Sleep(100);
            if (player2 != null)
                UnZipAndRunClient(player2);
        }

        private void UnZipAndRunClient(Player playerClient)
        {
            Directory.CreateDirectory(playerClient.Name);
            var zipFilePath = Path.Combine(playerClient.Name, playerClient.Name) + ".zip";
            File.WriteAllBytes(zipFilePath, playerClient.Zip);
            ZipFile.ExtractToDirectory(zipFilePath, playerClient.Name);
            Process.Start(new ProcessStartInfo("run.bat")
            {
                Arguments = "noRunServer",
                WorkingDirectory = playerClient.Name
            });
        }

        private void DisposeResource()
        {
            var proceses = Process.GetProcesses()
                                  .Where(x => x.MainWindowTitle == "rtsClient" || x.MainWindowTitle == "rtsServer" || x.MainWindowTitle == "TutorialForm")
                                  .OrderBy(x => x.MainWindowTitle);
            foreach (var process in proceses)
            {
                var currentProcess = process;
                SafeAction(currentProcess.Kill);
                process.WaitForExit(1000);
            }
            SafeAction(() => Directory.Delete(player.Name, true));
            if (player2 != null)
                SafeAction(() => Directory.Delete(player2.Name, true));
            SafeAction(() => Directory.Delete(ReplayDirectory, true));
        }

        private void SafeAction(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }    
        }
    }
}
