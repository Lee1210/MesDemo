using System;

using Autofac;

using Quartz;


namespace CpkDemo.Menjin
{
    class CpkJob:IJob
    {
        #region Implementation of IJob

        /// <summary>
        /// Called by the <see cref="T:Quartz.IScheduler"/> when a <see cref="T:Quartz.ITrigger"/>
        ///             fires that is associated with the <see cref="T:Quartz.IJob"/>.
        /// </summary>
        /// <remarks>
        /// The implementation may wish to set a  result object on the 
        ///             JobExecutionContext before this method exits.  The result itself
        ///             is meaningless to Quartz, but may be informative to 
        ///             <see cref="T:Quartz.IJobListener"/>s or 
        ///             <see cref="T:Quartz.ITriggerListener"/>s that are watching the job's 
        ///             execution.
        /// </remarks>
        /// <param name="context">The execution context.</param>
        public void Execute(IJobExecutionContext context)
        {
            Program.LogExcute();
            //using (var stream = new System.IO.StreamWriter("E:\\log.txt", true))
            //{
            //    stream.WriteLine(DateTime.Now.ToString("hh:mm:ss"));
            //}
        }

        #endregion
    }
}
