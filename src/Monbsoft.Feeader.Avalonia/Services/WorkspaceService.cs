using Monbsoft.Feeader.Avalonia.Infrastructure;
using Monbsoft.Feeader.Avalonia.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Monbsoft.Feeader.Avalonia.Services
{
    public static class WorkspaceService
    {
        private static string s_cacheWorkspaceFilePath = Path.Combine(Constants.CachePath, Constants.WorkspaceFileName);

        static WorkspaceService()
        {
            if (!Directory.Exists(Constants.Cache))
                Directory.CreateDirectory(Constants.Cache);
        }
        
        public static async Task<Workspace> LoadAsync()
        {
           Workspace? workspace = null;

            try
            {
                if (File.Exists(s_cacheWorkspaceFilePath))
                {
                    using (var stream = File.OpenRead(s_cacheWorkspaceFilePath))
                    {
                        workspace = await JsonSerializer.DeserializeAsync<Workspace>(stream);
                    }
                }
            }
            catch (NotSupportedException ex)
            {
                Trace.TraceWarning("Failed to load workspace: {0}", ex.Message);
                workspace = null;
            }
            return workspace ?? new Workspace();
        }
        public static async Task SaveAsync(Workspace workspace)
        {
            try
            {
                if (File.Exists(s_cacheWorkspaceFilePath))
                    File.Delete(s_cacheWorkspaceFilePath);

                using (var stream = File.OpenWrite(s_cacheWorkspaceFilePath))
                {
                    await JsonSerializer.SerializeAsync(stream, workspace);
                }
            }
            catch (NotSupportedException ex)
            {
                Trace.TraceWarning("Failed to save workspace: {0}", ex.Message);
            }
        }
    }
}
