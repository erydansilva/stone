using Quartz;
using Quartz.Impl;

namespace TemperatureStone.Job
{
	public class JobScheduler
	{
		public static void Start()
		{
			IJobDetail temperatureJob = JobBuilder.Create<TemperatureJob>()
					.WithIdentity("job1")
					.Build();

			ITrigger trigger = TriggerBuilder.Create()
				.WithDailyTimeIntervalSchedule
					(s =>
						s.WithIntervalInSeconds(30)
					  .OnEveryDay()
					)	
				.ForJob(temperatureJob)
				.WithIdentity("trigger1")
				.StartNow()
				.WithCronSchedule("0 0/60 * * * ?") // Tempo: A cada 60 minutos o job executa
				.Build();

			ISchedulerFactory sf = new StdSchedulerFactory();
			IScheduler sc = sf.GetScheduler();
			sc.ScheduleJob(temperatureJob, trigger);
			sc.Start();
		}
	}
}