using Squirrel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Organic_Wizard
{
    public static class Updater
    {
        public static async Task CheckForUpdates(Action<UpdateStatus> OnUpdateChecked)
        {
            UpdateStatus status = new UpdateStatus();
            UpdateManager updateManager = null;
            ReleaseEntry release = null;
            UpdateInfo updateInfo = null;
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var updateTask = UpdateManager.GitHubUpdateManager(Properties.Settings.Default.GitRepo);

                updateManager = await updateTask;
                updateInfo = await updateManager.CheckForUpdate();
                int? updateCount = updateInfo.ReleasesToApply?.Count;

                var currentVersion = updateManager.CurrentlyInstalledVersion();
                if (updateCount.HasValue && updateCount.Value > 0)
                {
                    release = await updateManager.UpdateApp();
                }

                if (updateCount > 0 && release != null && (currentVersion == null || release.Version > currentVersion))
                {
                    status.InstalledNewVersion = true;
                    status.Release = release;
                }
                
                OnUpdateChecked?.Invoke(status);
            }
            catch (Exception ex)
            {
                status.Error = ex;
                OnUpdateChecked?.Invoke(status);
            }
            finally
            {
                if (updateManager != null)
                    updateManager.Dispose();
            }
        }

        public class UpdateStatus
        {
            public bool InstalledNewVersion { get; set; }
            public Exception Error { get; set; }
            public ReleaseEntry Release { get; set; }
        }
    }
}
