using System.Diagnostics;
using System.Net;

namespace Common.Models
{
    public class ResponseBase<T> : ICloneable
    {
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public decimal ProcessTimeSeg { get; set; }
        public bool Success { get; set; }

        private Stopwatch? _timer;

        public ResponseBase(bool startTimer = false)
        {
            Code = HttpStatusCode.OK;
            Success = true;
            ProcessTimeSeg = 0;
            if (startTimer) StartTimer();
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public void StartTimer()
        {
            _timer = Stopwatch.StartNew();
        }

        public ResponseBase<T> StopTimer()
        {
            if (_timer != null)
            {
                _timer.Stop();
                var num = 0m;
                var isDecimal = decimal.TryParse(_timer.Elapsed.TotalSeconds.ToString(), out num);
                num = isDecimal ? num : 0m;

                ProcessTimeSeg = num;
            }
            return this;
        }
    }
}
