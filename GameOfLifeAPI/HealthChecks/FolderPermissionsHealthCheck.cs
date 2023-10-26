using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Security.AccessControl;

namespace GameOfLifeKata.API.HealthChecks
{
    public class FolderPermissionsHealthCheck: IHealthCheck
    {
        
        private readonly string _arg1;
        

        public FolderPermissionsHealthCheck(string arg1) => _arg1 = arg1;

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var isHealthy = false;

            try
            {
                new FileInfo(_arg1).GetAccessControl();
                File.Create(Path.Combine(_arg1, "dummy.json"));
                File.Delete(Path.Combine(_arg1, "dummy.json"));
                isHealthy = true;
            }
            catch (Exception ex)
            {
                isHealthy = true;
            }



            if (isHealthy)
            {
                return Task.FromResult(
                    HealthCheckResult.Healthy("A healthy result."));
            }

            return Task.FromResult(
                new HealthCheckResult(
                    context.Registration.FailureStatus, "An unhealthy result."));
        }
    }
}
