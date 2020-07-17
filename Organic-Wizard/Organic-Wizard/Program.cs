using Squirrel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Organic_Wizard
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            Task<UpdateManager> mgrTask = null;
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                mgrTask = UpdateManager.GitHubUpdateManager(@"https://github.com/clauderoy790/Organic-Wizard");
                //using (var mgr = UpdateManager.GitHubUpdateManager(@"https://github.com/clauderoy790/Organic-Wizard"))
                //{
                var updateManager = mgrTask.Result;
                var updates = await updateManager.CheckForUpdate();
                var dir = updates.PackageDirectory;
                MessageBox.Show($"Update: Appname:{ updateManager.ApplicationName}{Environment.NewLine}, root dir:{updateManager.RootAppDirectory}{Environment.NewLine}", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                var entry = await updateManager.UpdateApp();
                if (entry != null)
                {

                        MessageBox.Show($"Successfully updated to version: { entry.Version}! You may restart the app to get the newest features.", "Update Successful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //}
            }
            catch (Exception ex)
            {

                    MessageBox.Show($"An error occurred while trying to update, try again later.{Environment.NewLine}Error: " + ex.Message, "Uh oh!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                if (mgrTask != null && mgrTask.Result != null)
                    mgrTask.Result.Dispose();
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
