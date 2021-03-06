using System;

namespace SEI.Desktop.Services
{
    public class SampleService : ISampleService
    {
        public string GetCurrentDate()
        {
            return DateTime.Now.ToLongDateString();
        }
    }
}
