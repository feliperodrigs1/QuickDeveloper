using Newtonsoft.Json;

namespace Application.Models
{
    public class CustomReturn<T>
    {
        public Guid TransactionId { get; set; }

        public bool Failure { get; set; }

        public T Data { get; set; }

        public List<string> Errors { get; set; }

        public string Code { get; set; }

        [JsonIgnore]
        public Exception SetException
        {
            set
            {
                setMessageErros(value);
                Failure = true;
            }
        }

        public DateTime Date { get; set; }

        public CustomReturn()
        {
            TransactionId = Guid.NewGuid();
            Date = DateTime.Now;
        }

        public CustomReturn(Exception ex)
        {
            TransactionId = Guid.NewGuid();
            Date = DateTime.Now;
            SetException = ex;
        }

        private void setMessageErros(Exception ex)
        {
            if (ex != null)
            {
                if (Errors == null)
                {
                    Errors = new List<string>();
                }

                getInnerException(ex);
            }
        }

        private void getInnerException(Exception ex)
        {
            Errors.Add(ex.Message);
            if (ex.InnerException != null)
            {
                getInnerException(ex.InnerException);
            }
        }
    }
}
